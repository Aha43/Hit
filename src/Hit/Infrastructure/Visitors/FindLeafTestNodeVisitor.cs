using System.Collections.Generic;
using System.Linq;

namespace Hit.Infrastructure.Visitors
{
    internal class FindLeafTestNodeVisitor<World> : AbstractTestNodeVisitor<World>
    {
        private readonly List<TestNode<World>> _leafs = new List<TestNode<World>>();

        private readonly TestHierarchy<World> _hierarchy;

        public FindLeafTestNodeVisitor(TestHierarchy<World> hierarchy)
        {
            _hierarchy = hierarchy;
        }

        public IEnumerable<TestNode<World>> Leafs => _leafs.AsReadOnly();

        public override void Visit(TestNode<World> node, TestNode<World> parent)
        {
            if (_hierarchy.GetChildren(node).Count() == 0)
            {
                _leafs.Add(node);
            }
        }

    }

}
