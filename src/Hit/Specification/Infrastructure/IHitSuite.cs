using Hit.Specification.User;
using System.Threading.Tasks;

namespace Hit.Specification.Infrastructure
{
    public interface IHitSuite<World> : IHitType<World>
    {
        string Name { get; }
        string Description { get; }
        Task<IHitSuiteTestResults> RunTestsAsync();
        ITestImpl<World> GetTest(string name);
    }
}
