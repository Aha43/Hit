using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System.Threading.Tasks;

namespace Hit.Specification.User
{
    public interface ITestRunEventHandler<World> : IHitType<World>
    {
        Task RunStarts(ITestContext<World> context);
        Task RunFailed(ITestContext<World> context);
        Task RunEnded(ITestContext<World> context);
    }
}
