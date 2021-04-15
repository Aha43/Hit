using System.Threading.Tasks;

namespace Hit.Specification.Infrastructure
{
    public interface IHitSuites<World>
    {
        IHitSuite<World> GetNamedSuite(string name);
        Task<IUnitTestResult> RunUnitTestAsync(string suiteName, string unitTest);
    }
}
