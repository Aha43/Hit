using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System;
using System.Threading.Tasks;

namespace Hit.Infrastructure.Visitors
{
    internal class RunTestNodeVisitor<World> : AbstractTestNodeVisitorAsync<World>
    {
        private readonly World _world;

        public RunTestNodeVisitor(World world) => _world = world;

        public override async Task VisitAsync(TestNode<World> node, TestNode<World> parent)
        {
            var testResult = node.TestResult as TestResult;

            if (parent != null && parent.TestResult.Status != TestStatus.Success)
            {
                testResult.NotReached();
                return;
            }

            var test = node.Test;

            if (await ActAsync(test.PreTestActor, testResult, TestFailureSource.Pre).ConfigureAwait(false))
            {
                if (await ActAsync(test, testResult, TestFailureSource.Test).ConfigureAwait(false))
                {
                    if (await ActAsync (test.PostTestActor, testResult, TestFailureSource.Post).ConfigureAwait(false))
                    {
                        testResult.Success();
                    }
                }
            }
        }

        private async Task<bool> ActAsync(IWorldActor<World> actor, TestResult testResult, TestFailureSource source)
        {
            var ex = await ActAsync(actor).ConfigureAwait(false);
            if (ex == null) return true;
            testResult.Failed(ex, source);
            return false;
        }

        private async Task<Exception> ActAsync(IWorldActor<World> actor)
        {
            if (actor == default) return null;
            try
            {
                await actor.ActAsync(_world).ConfigureAwait(false);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

    }

}
