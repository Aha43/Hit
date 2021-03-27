using Hit.Specification.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hit.Specification.Infrastructure
{
    public interface IHitSuite<World> : IHitType<World>
    {
        void Initialize();
        Task<IEnumerable<ITestResultNode>> RunTestsAsync();
    }
}
