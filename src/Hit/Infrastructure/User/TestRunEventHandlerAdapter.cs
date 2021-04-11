using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System.Threading.Tasks;

namespace Hit.Infrastructure.User
{
    public abstract class TestRunEventHandlerAdapter<World> : ITestRunEventHandler<World>
    {
        public virtual Task RunEnded(ITestContext<World> context)
        {
            return Task.CompletedTask;
        }

        public virtual Task RunFailed(ITestContext<World> context)
        {
            return Task.CompletedTask;
        }

        public virtual Task RunStarts(ITestContext<World> context)
        {
            return Task.CompletedTask;
        }

    }

}
