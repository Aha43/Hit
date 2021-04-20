using System.Threading.Tasks;

namespace Hit.Specification.Infrastructure
{
    public interface IUnitTestsSet<World>
    {
        IUnitTests<World> GetNamedUnitTests(string name);
        Task<IUnitTestResult> RunUnitTestAsync(string unitTestsName, string unitTest);
    }
}
