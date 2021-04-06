using System.Collections.Generic;

namespace Hit.Specification.Infrastructure
{
    public interface IHitSuiteTestResults
    {
        string Name { get; }
        string Description { get; }
        IEnumerable<ITestResultNode> Results { get; }
        bool Success();
    }
}
