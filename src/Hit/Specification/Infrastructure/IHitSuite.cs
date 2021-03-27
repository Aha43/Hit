using Hit.Specification.User;
using System.Collections.Generic;

namespace Hit.Specification.Infrastructure
{
    public interface IHitSuite<World> : IHitType<World>
    {
        void Initialize();
        IEnumerable<ITestResultNode> RunTests();
    }
}
