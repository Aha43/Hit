using Hit.Specification.User;
using System.Threading.Tasks;

namespace Hit.Specification.Infrastructure
{
    public interface ITestRunEventHandler<World> : IHitType<World>
    {
        Task RunStarts(ITestContext<World> context);
        Task RunFailed(ITestContext<World> context);
        Task RunEnded(ITestContext<World> context);
    }
}
