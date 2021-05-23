using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using Items.Specification;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests
{
    public class CrudUnitTestEventHandler : UnitTestEventHandlerAdapter<ItemCrudWorld>
    {
        private IItemsRepository _repo;

        public CrudUnitTestEventHandler(IItemsRepository repo)
        {
            _repo = repo; // just to show injection works :)
        }

        public override async Task UnitTestFailed(ITestContext<ItemCrudWorld> context)
        {
            await DoSomethingAsync();
        }

        private static Task DoSomethingAsync()
        {
            return Task.CompletedTask;
        }

    }

}
