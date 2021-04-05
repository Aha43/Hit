using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System;
using System.Threading.Tasks;

namespace Hit.Infrastructure.Visitors
{
    internal class RunTestNodeVisitorAsync<World> : AbstractTestNodeVisitorAsync<World>
    {
        private readonly World _world;

        public RunTestNodeVisitorAsync(World world) => _world = world;

        public override async Task VisitAsync(TestNode<World> node, TestNode<World> parent)
        {
            var testResult = node.TestResult as TestResult;

            if (parent != null && parent.TestResult.Status != TestStatus.Success)
            {
                testResult.NotReached();
                return;
            }

            var test = node.Test;

            if (await TestAsync(test, node.TestOptions, testResult, TestFailureSource.Test).ConfigureAwait(false))
            { 
                testResult.Success();
            }
        }

        private async Task<bool> TestAsync(ITestImpl<World> test, ITestOptions options, TestResult testResult, TestFailureSource source)
        {
            var ex = await TestAsync(test, options).ConfigureAwait(false);
            if (ex == null) return true;
            testResult.Failed(ex, source);
            return false;
        }

        private async Task<Exception> TestAsync(ITestImpl<World> actor, ITestOptions options)
        {
            if (actor == default) return null;
            try
            {
                await actor.TestAsync(_world, options).ConfigureAwait(false);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

    }

}
