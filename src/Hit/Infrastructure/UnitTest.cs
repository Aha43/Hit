using Hit.Infrastructure.Visitors;
using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Infrastructure
{
    public class UnitTest<World>
    {
        private readonly TestNode<World>[] _testNodes;

        internal string Name { get; private set; }

        internal UnitTest(TestHierarchy<World> hierarchy, TestNode<World> last)
        {
            Name = last.UnitTest;

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

        internal async Task RunUnitTestAsync(TestContext<World> context, IUnitTestEventHandler<World> unitTestEventHandler)
        {
            context.UnitTest = Name;

            Visit(_notRunTestNodeVisitor);
            var testVisitor = new RunTestNodeVisitorAsync<World>(context);

            if (unitTestEventHandler != null)
            {
                await unitTestEventHandler.UnitTestStarts(context);
            }

            await VisitAsync(testVisitor).ConfigureAwait(false);
            
            if (unitTestEventHandler != null)
            {
                var handlersContext = ContextForHandler(context);
                if (handlersContext.TestResult != null && handlersContext.TestResult.Status == TestStatus.Failed)
                {
                    await unitTestEventHandler.UnitTestFailed(handlersContext);
                }
                else
                {
                    await unitTestEventHandler.UnitTestEnded(handlersContext);
                }
            }
        }

        private TestContext<World> ContextForHandler(TestContext<World> context)
        {
            var retVal = new TestContext<World>
            {
                World = context.World,
                EnvironmentType = context.EnvironmentType,
                UnitTestsName = context.UnitTestsName,
                UnitTest = context.UnitTest
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

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Name).Append(": ");
            for (var i = 0; i < _testNodes.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(" -> ");
                }
                sb.Append(_testNodes[i].TestName);
            }

            return sb.ToString();
        }

    }

}
