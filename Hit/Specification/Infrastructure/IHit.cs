using Hit.Specification.User;
using System.Collections.Generic;

namespace Hit.Specification.Infrastructure
{
    public interface IHit<World> : IHitType<World>
    {
        void Initialize();
        IEnumerable<ITestResultNode> RunTests();
    }
}
