using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hit.Specification.Infrastructure
{
    public interface IHitSuites<World>
    {
        IEnumerable<IHitSuite<World>> Suites { get; }
        IHitSuite<World> GetNamedSuite(string name);
        Task<IEnumerable<IHitSuiteTestResults>> RunTestsAsync();
    }
}
