using Items.Domain.Model;
using Items.Domain.Param;
using Items.Specification;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Items.Infrastructure.Repository.Rest
{
    public class ItemRepository : IItemsRepository
    {
        private readonly ApiUri _apiUri;

        public ItemRepository(ApiUri apiUri) => _apiUri = apiUri;

        public async Task<Item> CreateAsync(CreateItemParam param, CancellationToken cancellationToken) => await PerformAsync(param, "CreateItem", cancellationToken);
        public async Task<Item> ReadAsync(ReadItemParam param, CancellationToken cancellationToken) => await PerformAsync(param, "ReadItem", cancellationToken);
        public async Task<Item> UpdateAsync(UpdateItemParam param, CancellationToken cancellationToken) => await PerformAsync(param, "UpdateItem", cancellationToken);
        public async Task<Item> DeleteAsync(DeleteItemParam param, CancellationToken cancellationToken) => await PerformAsync(param, "DeleteItem", cancellationToken);
        
        private async Task<Item> PerformAsync(object param, string method, CancellationToken cancellationToken)
        {
            var paramJson = JsonSerializer.Serialize(param);
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(_apiUri + "/api/" + method, new StringContent(paramJson, Encoding.UTF8, "application/json"), cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NoContent:
                        return null;
                    default:
                        var content = await response.Content.ReadAsStringAsync(cancellationToken);
                        return JsonSerializer.Deserialize<Item>(content);
                }
            }

            throw new Exception(response.StatusCode.ToString());
        }

    }

}
