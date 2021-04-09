using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using Items.Domain.Param;
using Items.Specification;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests.TestLogic
{
    [UseAs(test: "DeleteItem", followingTest: "ReadItemAfterUpdate")]
    public class DeleteItemTestLogic : TestLogicBase<ItemCrudWorld>
    {
        private readonly IItemsRepository _repository;

        public DeleteItemTestLogic(IItemsRepository repository) => _repository = repository;

        public override async Task TestAsync(ITestContext<ItemCrudWorld> testContext)
        {
            // arrange
            var param = new DeleteItemParam
            {
                Id = testContext.World.Id
            };

            // act
            var deleted = await _repository.DeleteAsync(param, CancellationToken.None).ConfigureAwait(false);

            // assert
            deleted.ShouldNotBe(null);
            deleted.Id.ShouldBe(testContext.World.Id);
            deleted.Name.ShouldBe(testContext.World.Name);
        }

    }

}
