using Items.Domain.Model;
using Items.Domain.Param;
using Items.Specification;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Items.Infrastructure.Repository.InMemory
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly Dictionary<string, Item> _items = new();

        public Task<Item> CreateAsync(CreateItemParam param, CancellationToken cancellationToken)
        {
            var retVal = new Item
            {
                Id = Guid.NewGuid().ToString(),
                Name = param.Name
            };

            _items[retVal.Id] = retVal;

            return Task.FromResult(retVal);
        }

        public Task<Item> ReadAsync(ReadItemParam param, CancellationToken cancellationToken)
        {
            if (_items.TryGetValue(param.Id, out Item item))
            {
                return Task.FromResult(item);
            }

            return Task.FromResult(null as Item);
        }

        public Task<Item> UpdateAsync(UpdateItemParam param, CancellationToken cancellationToken)
        {
            if (_items.TryGetValue(param.Id, out Item item))
            {
                item.Name = param.Name;
                return Task.FromResult(item);
            }

            return Task.FromResult(null as Item);
        }

        public Task<Item> DeleteAsync(DeleteItemParam param, CancellationToken cancellationToken)
        {
            if (_items.TryGetValue(param.Id, out Item item))
            {
                _items.Remove(param.Id);
                return Task.FromResult(item);
            }

            return Task.FromResult(null as Item);
        }

    }

}
