using Hit.Specification.Infrastructure;
using Hit.Specification.User;
using System;

namespace Hit.Infrastructure.Visitors
{
    internal class RunTestNodeVisitor<World> : AbstractTestNodeVisitor<World>
    {
        private readonly World _world;

        public RunTestNodeVisitor(World world) => _world = world;

        public override void Visit(TestNode<World> node, TestNode<World> parent)
        {
            var testResult = node.TestResult as TestResult;

            if (parent != null && parent.TestResult.Status != TestStatus.Success)
            {
                testResult.NotReached();
                return;
            }

            var test = node.Test;

            if (Act(test.PreTestActor, testResult, TestFailureSource.Pre))
            {
                if (Act(test, testResult, TestFailureSource.Test))
                {
                    if (Act(test.PostTestActor, testResult, TestFailureSource.Post))
                    {
                        testResult.Success();
                    }
                }
            }
        }

        private bool Act(IWorldActor<World> actor, TestResult testResult, TestFailureSource source)
        {
            var ex = Act(actor);
            if (ex == null) return true;
            testResult.Failed(ex, source);
            return false;
        }

        private Exception Act(IWorldActor<World> actor)
        {
            if (actor == default) return null;
            try
            {
                actor.Act(_world);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

    }

}
