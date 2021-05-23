using Items.Domain.Model;
using Items.Domain.Param;
using System.Threading;
using System.Threading.Tasks;

namespace Items.Specification
{
    public interface IItemsRepository
    {
        Task<Item> CreateAsync(CreateItemParam param, CancellationToken cancellationToken);
        Task<Item> ReadAsync(ReadItemParam param, CancellationToken cancellationToken);
        Task<Item> UpdateAsync(UpdateItemParam param, CancellationToken cancellationToken);
        Task<Item> DeleteAsync(DeleteItemParam param, CancellationToken cancellationToken);
    }
}
