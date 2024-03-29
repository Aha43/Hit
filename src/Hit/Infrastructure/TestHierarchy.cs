﻿using Hit.Infrastructure.Exceptions;
using Hit.Infrastructure.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hit.Infrastructure
{
    internal class TestHierarchy<World>
    {
        private readonly List<TestNode<World>> _rootNodes = new List<TestNode<World>>();

        private readonly List<TestNode<World>> _leafNodes = new List<TestNode<World>>();

        private readonly Dictionary<string, TestNode<World>> _allNodes = new Dictionary<string, TestNode<World>>();

        private readonly Dictionary<string, List<TestNode<World>>> _childNodes = new Dictionary<string, List<TestNode<World>>>();

        private readonly Dictionary<string, TestNode<World>> _unitTestNodes = new Dictionary<string, TestNode<World>>();

        internal void AddTestImplType(Type testImplementation)
        {
            var testNodes = testImplementation.CreateTestNodes<World>();
            foreach (var node in testNodes)
            {
                if (_allNodes.ContainsKey(node.TestName))
                {
                    throw new TestNameCollisionException(node.TestName);
                }

                if (!string.IsNullOrWhiteSpace(node.ParentTestName))
                {
                    GetChildrenList(node.ParentTestName).Add(node);
                }
                else
                {
                    _rootNodes.Add(node);
                }

                if (!string.IsNullOrWhiteSpace(node.UnitTest))
                {
                    _unitTestNodes.Add(node.UnitTest, node);
                }

                _allNodes.Add(node.TestName, node);
            }
        }

        internal void AllTestImplTypesAdded()
        {
            var leafVisitor = new FindLeafTestNodeVisitor<World>(this);
            Dfs(leafVisitor);
            _leafNodes.Clear();
            _leafNodes.AddRange(leafVisitor.Leafs);

            Validate();
        }

        private void Validate()
        {
            // checks if all parent named exists
            foreach (var e in _childNodes)
            {
                if (!_allNodes.ContainsKey(e.Key))
                {
                    throw new MissingTestException(e.Key, e.Value.First().TestName);
                }
            }
        }

        private static readonly IEnumerable<TestNode<World>> EmptyTestNodeList = new TestNode<World>[] { };

        internal TestNode<World> GetNode(string name) => 
            _allNodes.TryGetValue(name, out TestNode<World> node) ? node : null;

        internal TestNode<World> GetParent(TestNode<World> node) => 
            node.ParentTestName != null && _allNodes.TryGetValue(node.ParentTestName, out TestNode<World> parent) ? parent : null;

        internal IEnumerable<TestNode<World>> GetChildren(TestNode<World> parent) => 
            _childNodes.TryGetValue(parent.TestName, out List<TestNode<World>> children) ? children.AsReadOnly() : EmptyTestNodeList;

        internal UnitTest<World> GetUnitTest(string system, string layer, string unitTest)
        {
            if (_unitTestNodes.TryGetValue(unitTest, out TestNode<World> lastNode))
            {
                return new UnitTest<World>(this, lastNode);
            }

            throw new UnitTestNotFoundException(system, layer, unitTest);
        }

        internal IEnumerable<string> UnitTestNames => _unitTestNodes.Keys.ToArray();

        internal void Dfs(AbstractTestNodeVisitor<World> visitor)
        {
            foreach (var root in _rootNodes)
            {
                Dfs(root, null, visitor);
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
            await visitor.VisitAsync(node, parent).ConfigureAwait(false);

            foreach (var child in GetChildren(node))
            {
                await DfsAsync(child, node, visitor).ConfigureAwait(false);
            }
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

        internal string[] UnitTestsNames
        {
            get
            {
                return _unitTestNodes.Select(e => e.Key).ToArray();
            }
        }

    }

}
