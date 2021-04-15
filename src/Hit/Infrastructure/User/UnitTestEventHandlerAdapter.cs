using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System.Threading.Tasks;

namespace Hit.Infrastructure.User
{
    public abstract class UnitTestEventHandlerAdapter<World> : IUnitTestEventHandler<World>
    {
        public virtual Task UnitTestEnded(ITestContext<World> context)
        {
            return Task.CompletedTask;
        }

        public virtual Task UnitTestRunFailed(ITestContext<World> context)
        {
            return Task.CompletedTask;
        }

        public virtual Task UnitTestStarts(ITestContext<World> context)
        {
            return Task.CompletedTask;
        }

    }

}
