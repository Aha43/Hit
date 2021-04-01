using System.Collections.Generic;

namespace Hit.Infrastructure
{
    internal class ParentChildren<World>
    {
        private readonly List<TestNode<World>> _children = new List<TestNode<World>>();

        internal TestNode<World> Parent { get; }

        internal IEnumerable<TestNode<World>> Children => _children.AsReadOnly();

        internal ParentChildren(TestNode<World> parent)
        {
            Parent = parent;
        }

        internal void AddChild(TestNode<World> child)
        {
            // more checks?
            _children.Add(child);
        }

    }

}
