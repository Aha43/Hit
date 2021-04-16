using Hit.Specification.Infrastructure;
using System.Threading.Tasks;

namespace Hit.Specification.User
{
    public interface IUnitTestEventHandler<World> : IHitType<World>
    {
        Task UnitTestStarts(ITestContext<World> context);
        Task UnitTestFailed(ITestContext<World> context);
        Task UnitTestEnded(ITestContext<World> context);
    }
}
