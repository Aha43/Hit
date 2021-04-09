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
    [UseAs(test: "UpdateItem", followingTest: "ReadItemAfterCreate")]
    public class UpdateItemTestLogic : TestLogicBase<ItemCrudWorld>
    {
        private readonly IItemsRepository _repository;

        public UpdateItemTestLogic(IItemsRepository repository) => _repository = repository;

        public override async Task TestAsync(ITestContext<ItemCrudWorld> testContext)
        {
            // arrange
            var newName = "Dragonfly";

            var param = new UpdateItemParam
            {
                Id = testContext.World.Id,
                Name = newName
            };

            // act
            var updated = await _repository.UpdateAsync(param, CancellationToken.None).ConfigureAwait(false);

            // assert
            updated.ShouldNotBe(null);
            updated.Id.ShouldBe(testContext.World.Id);
            updated.Name.ShouldBe(newName);

            // change world state
            testContext.World.Name = newName;
        }

    }
}
