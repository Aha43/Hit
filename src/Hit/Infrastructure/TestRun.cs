using Hit.Infrastructure.Visitors;
using Hit.Specification.Infrastructure;
using System.Collections.Generic;
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
                stack.Push(new TestNode<World>(current));
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

        private readonly static NotRunTestNodeVisitor<World> _notRunTestNodeVisitor = new NotRunTestNodeVisitor<World>();

        internal async Task RunTestsAsync(TestContext<World> context, ITestRunEventHandler<World> testRunEventHandler)
        {
            Visit(_notRunTestNodeVisitor);
            var testVisitor = new RunTestNodeVisitorAsync<World>(context);

            if (testRunEventHandler != null)
            {
                await testRunEventHandler.RunStarts(context);
            }

            await VisitAsync(testVisitor).ConfigureAwait(false);
            
            if (testRunEventHandler != null)
            {
                var handlersContext = ContextForHandler(context);
                if (handlersContext.TestResult != null && handlersContext.TestResult.Status == TestStatus.Failed)
                {
                    await testRunEventHandler.RunFailed(handlersContext);
                }
                else
                {
                    await testRunEventHandler.RunEnded(handlersContext);
                }
            }
        }

        private TestContext<World> ContextForHandler(TestContext<World> context)
        {
            var retVal = new TestContext<World>
            {
                World = context.World,
                EnvironmentType = context.EnvironmentType,
                SuiteName = context.SuiteName,
                TestRunName = context.TestRunName
            };

            foreach (var node in _testNodes)
            {
                if (node.TestResult.Status == TestStatus.Failed)
                {
                    retVal.ParentTestName = node.ParentTestName;
                    retVal.TestName = node.TestName;
                    retVal.TestResult = node.TestResult;
                    retVal.Options = node.TestOptions;
                }

                return retVal;
            }

            return retVal;
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
                parent.Next = current;
                parent = current;
            }
            return retVal;
        }

    }

}
