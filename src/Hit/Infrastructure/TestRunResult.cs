using Hit.Specification.Infrastructure;
using System.Collections.Generic;

namespace Hit.Infrastructure
{
    internal class TestRunResult : ITestRunResult
    {
        internal TestRunResult(string suiteName, string suiteDescription, string runName, ITestResultNode results)
        {
            SuiteName = suiteName;
            SuiteDescription = suiteDescription;
            RunName = runName;
            Results = new ITestResultNode[] { results };
        }

        public string SuiteName { get; }
        public string SuiteDescription { get; }
        public string RunName { get; }
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
