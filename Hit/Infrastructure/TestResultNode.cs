using Hit.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hit.Infrastructure
{
    internal class TestResultNode : ITestResultNode
    {
        private readonly TestResultNode _parent;

        private readonly List<ITestResultNode> _children = new List<ITestResultNode>();

        private readonly TestResult _testResult;

        internal TestResultNode(TestResult testResult)
        {
            _testResult = new TestResult(testResult);
        }

        internal TestResultNode(TestResult testResult, TestResultNode parent)
        {
            _testResult = new TestResult(testResult);
            _parent = parent;
        }

        public ITestResultNode Parent => _parent;

        public IEnumerable<ITestResultNode> Children => _children.AsReadOnly();

        public bool IsRoot => _parent == default;

        public bool IsLeaf => !_children.Any();

        public bool IsInternal => !IsRoot && !IsLeaf;

    }

}
