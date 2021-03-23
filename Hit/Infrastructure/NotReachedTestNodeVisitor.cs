namespace Hit.Infrastructure
{
    internal class NotReachedTestNodeVisitor<World> : AbstractTestNodeVisitor<World>
    {
        public override void Visit(TestNode<World> node)
        {
            var testResult = node.TestResult as TestResult;

            testResult.Failure = default;
            testResult.Status = Specification.TestStatus.NotReached;
        }

    }

}
