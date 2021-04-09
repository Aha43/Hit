using Hit.Specification.Infrastructure;
using System.Threading.Tasks;

namespace Hit.Specification.User
{
    public interface ITestLogic<World> : IHitType<World>
    {
        void Test(ITestContext<World> context);
        Task TestAsync(ITestContext<World> context);
    }
}
