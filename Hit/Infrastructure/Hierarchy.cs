using System;
using System.Collections.Generic;
using System.Linq;

namespace Hit.Infrastructure
{
    internal class Hierarchy<World>
    {
        private readonly IList<TestNode<World>> _rootNodes = new List<TestNode<World>>();

        private readonly IDictionary<string, List<TestNode<World>>> _childNodes = new Dictionary<string, List<TestNode<World>>>();

        internal void Add(Type testType)
        {
            var node = new TestNode<World>(testType);

            var parentNames = testType.ParentNames<World>();
            if (parentNames.Any())
            {
                foreach (var name in parentNames)
                {
                    GetSiblingList(name).Add(node);
                }
            }
            else
            {
                _rootNodes.Add(node);
            }
        }

        internal void Dfs(AbstractTestNodeVisitor<World> visitor)
        {
            foreach (var root in _rootNodes)
            {
                Dfs(root, visitor);
            }
        }

        internal void Dfs(TestNode<World> node, AbstractTestNodeVisitor<World> visitor)
        {
            visitor.Visit(node);

            if (_childNodes.TryGetValue(node.TestName, out List<TestNode<World>> children))
            {
                foreach (var child in children)
                {
                    visitor.Visit(child);
                }
            }
        }

        private List<TestNode<World>> GetSiblingList(string parentName)
        {
            if (_childNodes.TryGetValue(parentName, out List<TestNode<World>> retVal))
            {
                return retVal;
            }

            retVal = new List<TestNode<World>>();
            _childNodes[parentName] = retVal;

            return retVal;
        }

    }

}
