using Hit.Infrastructure.User;
using Hit.Specification.Infrastructure;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests
{
    public class CrudTestRunEventHandler : TestRunEventHandlerAdapter<ItemCrudWorld>
    {
        public override async Task RunFailed(ITestContext<ItemCrudWorld> context)
        {
            await DoSomethingAsync();
        }

        private static Task DoSomethingAsync()
        {
            return Task.CompletedTask;
        }

    }

}
