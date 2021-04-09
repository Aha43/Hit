using Hit.Specification.Infrastructure;
using System.Collections.Generic;

namespace Hit.Infrastructure
{
    internal class HitSuiteTestResults : IHitSuiteTestResults
    {
        internal HitSuiteTestResults(string name, string description, IEnumerable<ITestResultNode> results)
        {
            Name = name;
            Description = description;
            Results = results;
        }

        internal HitSuiteTestResults(string name, string description, ITestResultNode results)
        {
            Name = name;
            Description = description;
            Results = new ITestResultNode[] { results };
        }

        public string Name { get; }
        public string Description { get; }
        public IEnumerable<ITestResultNode> Results { get; }

        public bool Success()
        {
            foreach (var node in Results)
            {
                if (!Success(node)) return false;
            }

            return true;
        }

        private bool Success(ITestResultNode node)
        {
            if (node.TestResult.Status != TestStatus.Success)
            {
                return false;
            }

            return node.Next == null || Success(node.Next);
        }

    }

}
