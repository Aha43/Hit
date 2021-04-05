using Hit.Infrastructure.Visitors;
using Hit.Specification.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit.Infrastructure
{
    internal class TestRuns<World>
    {
        private TestRun<World>[] _runs;

        internal TestRuns(TestHierarchy<World> hierarchy)
        {
            var leafs = hierarchy.Leafs;
            _runs = new TestRun<World>[leafs.Count()];
            var i = 0;
            foreach (var leaf in leafs)
            {
                _runs[i++] = new TestRun<World>(hierarchy, leaf);
            }
        }

        internal void Visit(AbstractTestNodeVisitor<World> visitor)
        {
            foreach (var run in _runs)
            {
                run.Visit(visitor);
            }
        }

        internal async Task VisitAsync(AbstractTestNodeVisitorAsync<World> visitor)
        {
            foreach (var run in _runs)
            {
                await run.VisitAsync(visitor).ConfigureAwait(false);
            }
        }

        private readonly static NotRunTestNodeVisitor<World> _notRunTestNodeVisitor = new NotRunTestNodeVisitor<World>();

        internal async Task RunTestsAsync(IWorldProvider<World> worldProvider)
        {
            for (var i = 0; i < _runs.Length; i++)
            {
                await _runs[i].RunTestsAsync(worldProvider);
            }
        }

        internal IEnumerable<TestResultNode> CreateTestResults()
        {
            var n = _runs.Length;
            var retVal = new TestResultNode[n];
            for (var i = 0; i < n; i++)
            {
                retVal[i] = _runs[i].GetTestResult();
            }
            return retVal;
        }

    }

}
