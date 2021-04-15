using Hit.Specification.User;
using System.Threading.Tasks;

namespace Hit.Specification.Infrastructure
{
    public interface IHitSuite<World> : IHitType<World>
    {
        string Name { get; }
        string Description { get; }
        string EnvironmentType { get; }
        Task<IUnitTestResult> RunUnitTestAsync(string name);
        ITestLogic<World> GetTest(string name);
    }
}
