using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using Items.Specification;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests
{
    public class CrudTestRunEventHandler : UnitTestEventHandlerAdapter<ItemCrudWorld>
    {
        private IItemsRepository _repo;

        public CrudTestRunEventHandler(IItemsRepository repo)
        {
            _repo = repo; // just to show injection works :)
        }

        public override async Task UnitTestRunFailed(ITestContext<ItemCrudWorld> context)
        {
            await DoSomethingAsync();
        }

        private static Task DoSomethingAsync()
        {
            return Task.CompletedTask;
        }

    }

}
