using Hit.Specification.Infrastructure;
using System.Threading.Tasks;

namespace Items.HitIntegrationTests
{
    public class CrudTestRunEventHandler : ITestRunEventHandler<ItemCrudWorld>
    {
        public Task RunEnded(ITestContext<ItemCrudWorld> context)
        {
            return Task.CompletedTask;
        }

        public Task RunFailed(ITestContext<ItemCrudWorld> context)
        {
            return Task.CompletedTask;
        }

        public Task RunStarts(ITestContext<ItemCrudWorld> context)
        {
            return Task.CompletedTask;
        }

    }

}
