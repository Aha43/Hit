namespace Hit.Infrastructure.Visitors
{
    internal abstract class AbstractTestNodeVisitor<World>
    {
        public abstract void Visit(TestNode<World> node, TestNode<World> parent);
    }
}
