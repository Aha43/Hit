using System;
using System.Collections.Generic;
using System.Text;

namespace Hit.Specification
{
    public interface IHit<World> : IHitType<World>
    {
        void Initialize();
        IEnumerable<ITestResult> RunTests();
    }
}
