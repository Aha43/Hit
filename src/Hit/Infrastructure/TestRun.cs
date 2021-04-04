using Hit.Infrastructure.Visitors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Infrastructure
{
    internal class TestRun<World>
    {
        private readonly TestNode<World>[] _testNodes;

        internal TestRun(TestHierarchy<World> hierarchy, TestNode<World> last)
        {
            var stack = new Stack<TestNode<World>>();

            var current = last;
            while (current != null)
            {
                stack.Push(current);
                current = hierarchy.GetParent(current);
            }

            _testNodes = stack.ToArray();
        }

        internal void Visit(AbstractTestNodeVisitor<World> visitor)
        {
            var n = _testNodes.Length;
            if (n == 0) return;
            visitor.Visit(_testNodes[0], null);
            for (var i = 1; i < _testNodes.Length; i++)
            {
                visitor.Visit(_testNodes[i], _testNodes[i - 1]);
            }
        }

        internal async Task VisitAsync(AbstractTestNodeVisitorAsync<World> visitor)
        {
            var n = _testNodes.Length;
            if (n == 0) return;
            await visitor.VisitAsync(_testNodes[0], null).ConfigureAwait(false);
            for (var i = 1; i < _testNodes.Length; i++)
            {
                await visitor.VisitAsync(_testNodes[i], _testNodes[i - 1]).ConfigureAwait(false);
            }
        }

        internal TestResultNode GetTestResult()
        {
            int n = _testNodes.Length;
            if (n == 0) return null;
            var retVal = new TestResultNode(_testNodes[0].TestResult as TestResult);
            var parent = retVal;
            for (var i = 1; i < n; i++)
            {
                var current = new TestResultNode(_testNodes[i].TestResult as TestResult);
                parent.AddChild(current);
                parent = current;
            }
            return retVal;
        }

    }

}
