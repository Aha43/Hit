using Hit.Infrastructure.Attributes;
using Hit.Infrastructure.User;
using Hit.Infrastructure.User.Utility;
using Hit.Specification.Infrastructure;
using Items.Domain.Param;
using Items.Specification;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests.TestLogic
{
    [UseAs(test: "CreateItem")]
    public class CreateItemTestLogic : TestLogicBase<ItemCrudWorld>
    {
        private readonly IItemsRepository _repository;

        private readonly TestLogMessageBuilder<ItemCrudWorld> _msgBuilder;

        public CreateItemTestLogic(IItemsRepository repository)
        {
            _repository = repository;

            _msgBuilder = new TestLogMessageBuilder<ItemCrudWorld>(this)
                .UsingService(typeof(IItemsRepository), repository);
        }

        public override async Task TestAsync(ITestContext<ItemCrudWorld> testContext)
        {
            _msgBuilder.ClearTransient().InContext(testContext);

            var param = new CreateItemParam
            {
                Name = "Dragon"
            };

            var created = await _repository.CreateAsync(param, CancellationToken.None).ConfigureAwait(false);

            created.ShouldNotBe(null);
            created.Name.ShouldBe("Dragon");

            testContext.World.Id = created.Id;
            testContext.World.Name = created.Name;

            testContext.Log(_msgBuilder.ToString());
        }

    }

}
