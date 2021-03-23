using Hit.Specification;
using System;

namespace Hit.Infrastructure
{
    internal class RunTestNodeVisitor<World> : AbstractTestNodeVisitor<World>
    {
        private readonly NotReachedTestNodeVisitor<World> _notReachedTestNodeVisitor = new NotReachedTestNodeVisitor<World>();

        private readonly World _world;

        private readonly Hierarchy<World> _hierarchy;

        public RunTestNodeVisitor(
            World world,
            Hierarchy<World> hierarchy)
        {
            _world = world;
            _hierarchy = hierarchy;
        }
        
        public override void Visit(TestNode<World> node)
        {
            var test = node.Test;

            var testResult = node.TestResult as TestResult;

            var ex = Act(test.PreTestActor);
            if (ex != null)
            {
                testResult.Failure = new TestFailure
                {
                    Exception = ex,
                    Source = TestFailureSource.Pre
                };
            }

            ex = Act(test);
            if (ex != null)
            {
                testResult.Failure = new TestFailure
                {
                    Exception = ex,
                    Source = TestFailureSource.Test
                };
            }

            ex = Act(test.PostTestActor);
            if (ex != null)
            {
                testResult.Failure = new TestFailure
                {
                    Exception = ex,
                    Source = TestFailureSource.Post
                };
            }
        }

        private Exception Act(IWorldActor<World> actor)
        {
            if (actor != default)
            {
                try
                {
                    actor.Act(_world);
                }
                catch (Exception ex)
                {
                    return ex;
                }
            }

            return null;
        }

    }

}
