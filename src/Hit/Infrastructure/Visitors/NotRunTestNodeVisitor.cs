namespace Hit.Infrastructure.Visitors
{
    internal class NotRunTestNodeVisitor<World> : AbstractTestNodeVisitor<World>
    {
        public override void Visit(TestNode<World> node, TestNode<World> parent)
        {
            var testResult = node.TestResult as TestResult;

            testResult.NotRun();
        }

    }

}
