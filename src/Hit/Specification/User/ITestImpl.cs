using Hit.Specification.Infrastructure;
using System.Threading.Tasks;

namespace Hit.Specification.User
{
    public interface ITestImpl<World> : IHitType<World>
    {
        void Test(World world, ITestOptions options);
        Task TestAsync(World world, ITestOptions options);
    }
}
