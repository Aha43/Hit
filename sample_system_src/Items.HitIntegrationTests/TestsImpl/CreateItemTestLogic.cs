using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using Items.Domain.Param;
using Items.Specification;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests.TestsImpl
{
    [UseAs(test: "CreateItem")]
    public class CreateItemTestLogic : TestLogicBase<ItemCrudWorld>
    {
        private readonly IItemsRepository _repository;

        public CreateItemTestLogic(IItemsRepository repository) => _repository = repository;

        public override async Task TestAsync(ITestContext<ItemCrudWorld> testContext)
        {
            // arrange
            var param = new CreateItemParam
            {
                Name = "Dragon"
            };

            // act
            var created = await _repository.CreateAsync(param, CancellationToken.None).ConfigureAwait(false);

            // assert
            created.ShouldNotBe(null);
            created.Name.ShouldBe("Dragon");

            // change world state
            testContext.World.Id = created.Id;
            testContext.World.Name = created.Name;
        }

    }

}
