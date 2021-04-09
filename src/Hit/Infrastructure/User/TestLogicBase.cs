using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System.Threading.Tasks;

namespace Hit.Infrastructure.User
{
    public abstract class TestLogicBase<World> : ITestLogic<World>
    {
        public virtual void Test(ITestContext<World> context) { }

        public virtual async Task TestAsync(ITestContext<World> context)
        {
            await Task.Run(() => Test(context)).ConfigureAwait(false);
        }

    }

}
