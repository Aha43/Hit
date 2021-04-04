using System.Collections.Generic;

namespace Hit.Specification.Infrastructure
{
    internal class HitSuiteTestResults : IHitSuiteTestResults
    {
        internal HitSuiteTestResults(string name, string description, IEnumerable<ITestResultNode> results)
        {
            Name = name;
            Description = description;
            Results = results;
        }

        public string Name { get; }
        public string Description { get; }
        public IEnumerable<ITestResultNode> Results { get; }
    }
}
