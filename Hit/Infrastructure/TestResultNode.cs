using Hit.Specification.Infrastructure;
using System.Collections.Generic;

namespace Hit.Infrastructure
{
    internal class TestResultNode : ITestResultNode
    {
        private readonly List<ITestResultNode> _children = new List<ITestResultNode>();

        private readonly TestResult _testResult;

        internal TestResultNode(TestResult testResult)
        {
            _testResult = new TestResult(testResult);
        }

        internal void AddChild(TestResultNode child) => _children.Add(child);

        public IEnumerable<ITestResultNode> Children => _children.AsReadOnly();

        public ITestResult TestResult => _testResult;

    }

}
