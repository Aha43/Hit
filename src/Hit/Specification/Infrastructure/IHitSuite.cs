using Hit.Specification.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hit.Specification.Infrastructure
{
    public interface IHitSuite<World> : IHitType<World>
    {
        Task<IEnumerable<ITestResultNode>> RunTestsAsync();
        ITestImplementation<World> GetTest(string name);
    }
}
