using System.Collections.Generic;

namespace Hit.Specification.Infrastructure
{
    public interface IHitSuiteTestResults
    {
        public string Name { get; }
        public string Description { get; }
        public IEnumerable<ITestResultNode> Results { get; }
    }
}
