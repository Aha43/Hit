using System.Threading.Tasks;

namespace Hit.Infrastructure.Visitors
{
    internal abstract class AbstractTestNodeVisitorAsync<World>
    {
        public abstract Task VisitAsync(TestNode<World> node, TestNode<World> parent);
    }
}
