using Hit.Infrastructure.Visitors;
using Hit.Specification.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hit.Infrastructure
{
    internal class Hierarchy<World>
    {
        private readonly List<TestNode<World>> _rootNodes = new List<TestNode<World>>();

        private readonly List<TestNode<World>> _leafNodes = new List<TestNode<World>>();

        private readonly Dictionary<string, TestNode<World>> _allNodes = new Dictionary<string, TestNode<World>>();

        private readonly Dictionary<string, List<TestNode<World>>> _childNodes = new Dictionary<string, List<TestNode<World>>>();

        internal void Add(Type testType)
        {
            var parentNames = testType.ParentNames<World>();
            if (parentNames.Any())
            {
                foreach (var parentName in parentNames)
                {
                    var node = new TestNode<World>(testType, parentName);
                    GetChildrenList(parentName).Add(node);
                    _allNodes.Add(node.TestName, node);
                }
            }
            else
            {
                var node = new TestNode<World>(testType, null);
                _rootNodes.Add(node);
                _allNodes.Add(node.TestName, node);
            }
        }

        internal void Completed()
        {
            var leafVisitor = new FindLeafTestNodeVisitor<World>(this);
            Dfs(leafVisitor);
            _leafNodes.Clear();
            _leafNodes.AddRange(leafVisitor.Leafs);
        }

        internal IEnumerable<TestNode<World>> Roots => _rootNodes.AsReadOnly();

        internal IEnumerable<TestNode<World>> Leafs => _leafNodes.AsReadOnly();

        private static readonly IEnumerable<TestNode<World>> EmptyTestNodeList = new TestNode<World>[] { };

        internal TestNode<World> GetNode(string name)
        {
            if (_allNodes.TryGetValue(name, out TestNode<World> node))
            {
                return node;
            }

            return null;
        }

        internal TestNode<World> GetParent(TestNode<World> node)
        {
            if (node.ParentTestName !=null && _allNodes.TryGetValue(node.ParentTestName, out TestNode<World> parent))
            {
                return parent;
            }

            return null;
        }

        internal IEnumerable<TestNode<World>> GetChildren(TestNode<World> parent)
        {
            if (_childNodes.TryGetValue(parent.TestName, out List<TestNode<World>> children))
            {
                return children.AsReadOnly();
            }

            return EmptyTestNodeList;
        }

        internal void Dfs(AbstractTestNodeVisitor<World> visitor)
        {
            foreach (var root in _rootNodes)
            {
                Dfs(root, null, visitor);
            }
        }

        internal async Task DfsAsync(AbstractTestNodeVisitorAsync<World> visitor)
        {
            foreach (var root in _rootNodes)
            {
                await DfsAsync(root, null, visitor).ConfigureAwait(false);
            }
        }

        internal void Dfs(TestNode<World> node, TestNode<World> parent, AbstractTestNodeVisitor<World> visitor)
        {
            visitor.Visit(node, parent);

            foreach (var child in GetChildren(node))
            {
                Dfs(child, node, visitor);
            }
        }

        internal async Task DfsAsync(TestNode<World> node, TestNode<World> parent, AbstractTestNodeVisitorAsync<World> visitor)
        {
            await visitor.VisitAsync(node, parent);

            foreach (var child in GetChildren(node))
            {
                await DfsAsync(child, node, visitor).ConfigureAwait(false);
            }
        }

        internal IEnumerable<ITestResultNode> CreateTestResultForrest()
        {
            var forrest = new List<ITestResultNode>();

            foreach (var root in _rootNodes)
            {
                var tree = CreateTestResultTree(root);
                forrest.Add(tree);
            }

            return forrest.AsReadOnly();
        }

        private TestResultNode CreateTestResultTree(TestNode<World> root)
        {
            var retVal = new TestResultNode(root.TestResult as TestResult);

            foreach (var child in GetChildren(root))
            {
                var tree = CreateTestResultTree(child);
                retVal.AddChild(tree);
            }

            return retVal;
        }

        private List<TestNode<World>> GetChildrenList(string parentName)
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
