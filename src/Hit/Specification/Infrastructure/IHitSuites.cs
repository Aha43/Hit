using System.Threading.Tasks;

namespace Hit.Specification.Infrastructure
{
    public interface IHitSuites<World>
    {
        IHitSuite<World> GetNamedSuite(string name);
        Task<ITestRunResult> RunTestRunAsync(string suiteName, string runName);
    }
}
