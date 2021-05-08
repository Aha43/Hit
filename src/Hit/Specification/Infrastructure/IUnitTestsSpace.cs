using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hit.Specification.Infrastructure
{
    public interface IUnitTestsSpace<World>
    {
        int Dimension { get; }
        int SystemCount { get; }
        int LayerCount { get; }
        IEnumerable<string> SystemNames { get; }
        IEnumerable<string> LayerNames { get; }
        IEnumerable<(string system, string layer, string unitTests)> UnitTestCoordinates { get; }
        IUnitTests<World> GetUnitTests();
        IUnitTests<World> GetUnitTests(string system);
        IUnitTests<World> GetUnitTests(string system, string layer);
        Task<IUnitTestResult> RunUnitTestAsync(string system, string unitTest);
        Task<IUnitTestResult> RunUnitTestAsync(string system, string layer, string unitTest);
    }
}
