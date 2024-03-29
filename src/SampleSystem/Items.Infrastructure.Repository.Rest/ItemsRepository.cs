﻿using Items.Domain.Model;
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
    public class ItemsRepository : IItemsRepository
    {
        private readonly ApiUri _apiUri;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public ItemsRepository(ApiUri apiUri) => _apiUri = apiUri;

        public async Task<Item> CreateAsync(CreateItemParam param, CancellationToken cancellationToken) => await PerformAsync(param, "CreateItem", cancellationToken).ConfigureAwait(false);
        public async Task<Item> ReadAsync(ReadItemParam param, CancellationToken cancellationToken) => await PerformAsync(param, "ReadItem", cancellationToken).ConfigureAwait(false);
        public async Task<Item> UpdateAsync(UpdateItemParam param, CancellationToken cancellationToken) => await PerformAsync(param, "UpdateItem", cancellationToken).ConfigureAwait(false);
        public async Task<Item> DeleteAsync(DeleteItemParam param, CancellationToken cancellationToken) => await PerformAsync(param, "DeleteItem", cancellationToken).ConfigureAwait(false);
        
        private async Task<Item> PerformAsync(object param, string method, CancellationToken cancellationToken)
        {
            var paramJson = JsonSerializer.Serialize(param);
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_apiUri.Uri);
            var response = await httpClient.PostAsync("/api/" + method, new StringContent(paramJson, Encoding.UTF8, "application/json"), cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NoContent:
                        return null;
                    default:
                        var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                        var retVal = JsonSerializer.Deserialize<Item>(content, _jsonOptions);
                        return retVal;
                }
            }

            throw new Exception(response.StatusCode.ToString());
        }

    }

}
