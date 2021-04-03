using Hit.Attributes;
using Hit.Infrastructure.User;
using Items.Domain.Param;
using Items.Specification;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests
{
    [UseAs(test: "ReadItemAfterCreate", followingTest: "CreateItem")]
    [UseAs(test: "ReadItemAfterUpdate", followingTest: "UpdateItem")]
    public class ReadItemTestImplementation : TestImplementationBase<ItemCrudWorld>
    {
        private IItemsRepository _repository;

        public ReadItemTestImplementation(IItemsRepository repository) => _repository = repository;

        public override async Task ActAsync(ItemCrudWorld world)
        {
            // arange
            var param = new ReadItemParam
            {
                Id = world.Id
            };

            // act
            var read = await _repository.ReadAsync(param, CancellationToken.None);

            // assert
            read.ShouldNotBe(null);
            read.Id.ShouldBe(world.Id);
            read.Name.ShouldBe(world.Name);
        }

    }

}
