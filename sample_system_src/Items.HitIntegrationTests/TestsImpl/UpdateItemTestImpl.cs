using Hit.Attributes;
using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using Items.Domain.Param;
using Items.Specification;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests.TestsImpl
{
    [UseAs(test: "UpdateItem", followingTest: "ReadItemAfterCreate")]
    public class UpdateItemTestImpl : TestImplementationBase<ItemCrudWorld>
    {
        private readonly IItemsRepository _repository;

        public UpdateItemTestImpl(IItemsRepository repository) => _repository = repository;

        public override async Task TestAsync(ItemCrudWorld world, ITestOptions options)
        {
            // arrange
            var param = new UpdateItemParam
            {
                Id = world.Id,
                Name = "DragonFly"
            };

            // act
            var updated = await _repository.UpdateAsync(param, CancellationToken.None);

            // assert
            updated.ShouldNotBe(null);
            updated.Id.ShouldBe(world.Id);
            updated.Name.ShouldBe("DragonFly");

            // change world state
            world.Name = "DragonFly";
        }

    }
}
