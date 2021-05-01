using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hit.Specification.Infrastructure
{
    public interface IUnitTestsSpace<World>
    {
        IEnumerable<string> SystemNames();
        IEnumerable<string> LayerNames(string system);
        IUnitTests<World> GetUnitTests(string system);
        IUnitTests<World> GetUnitTests(string system, string layer);
        Task<IUnitTestResult> RunUnitTestAsync(string system, string unitTest);
        Task<IUnitTestResult> RunUnitTestAsync(string system, string layer, string unitTest);
    }
}
