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
    [UseAs(test: "DeleteItem", followingTest: "ReadItemAfterUpdate")]
    public class DeleteItemTestImpl : TestImplBase<ItemCrudWorld>
    {
        private readonly IItemsRepository _repository;

        public DeleteItemTestImpl(IItemsRepository repository) => _repository = repository;

        public override async Task TestAsync(ItemCrudWorld world, ITestOptions options)
        {
            // arrange
            var param = new DeleteItemParam
            {
                Id = world.Id
            };

            // act
            var deleted = await _repository.DeleteAsync(param, CancellationToken.None);

            // assert
            deleted.ShouldNotBe(null);
            deleted.Id.ShouldBe(world.Id);
            deleted.Name.ShouldBe(world.Name);
        }

    }

}
