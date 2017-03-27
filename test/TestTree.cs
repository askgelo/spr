using Xunit;
using src;
using System.Linq;
using System.Collections.Generic;
using src.Models;
using System;

namespace Tests
{
    public class TestTree
    {
        Node tree;

        public TestTree()
        {
            tree = Node.Create("root");
            tree.Add(Node.Create("node1", "data1"));
        }

        [Fact]
        public void CreateNode()
        {
            var node = Node.Create("outer node");

            Assert.True(node.Count() == 0);
            Assert.True(node.Name == "outer node");
            Assert.True(node.Data == string.Empty);
            Assert.True(node.Parent == null);
        }
        [Fact]
        public void AddNode() 
        {
            Assert.True(tree.Count() == 1);
            Assert.True(tree[0].Name == "node1");
            Assert.True(tree[0].Data == "data1");
            Assert.True(tree[0].Parent == tree);
        }
        [Fact]
        public void RemoveNode()
        {
            tree.Remove(tree[0]);

            Assert.True(tree.Count() == 0);
        }
        [Fact]
        public void InsertNode()
        {
            tree.Insert(0, Node.Create("node0"));

            Assert.True(tree.Count() == 2);
            Assert.True(tree[0].Name == "node0");
            Assert.True(tree[0].Parent == tree);
        }

        [Fact]
        public void GetPath()
        {
            var firstChild = tree[0];
            var nodeTwo1 = Node.Create("Node 1 two ganeration");
            firstChild.Add(nodeTwo1);
            var nodeTwo2 = Node.Create("Node 2 two ganeration");
            nodeTwo2.Add(Node.Create("Temp 1"));
            nodeTwo2.Add(Node.Create("Temp 2"));
            var nodeThree = Node.Create("Node three ganeration");
            nodeTwo2.Add(nodeThree);
            firstChild.Add(nodeTwo2);

            string key = nodeThree.GetPath();

            Assert.Equal("0-1-2-Node three ganeration", key);
        }

        [Fact]
        public void IsExists()
        {
            var firstChild = tree[0];
            var nodeTwo1 = Node.Create("Node 1 two ganeration");
            firstChild.Add(nodeTwo1);
            var nodeTwo2 = Node.Create("Node 2 two ganeration");
            nodeTwo2.Add(Node.Create("Temp 1"));
            nodeTwo2.Add(Node.Create("Temp 2"));
            var nodeThree = Node.Create("Node three ganeration");
            nodeTwo2.Add(nodeThree);
            firstChild.Add(nodeTwo2);
            string key = nodeThree.GetPath();

            bool exists = tree.IsExists(key);

            Assert.True(exists, string.Format("wrong path ({0})", key));
        }

        [Fact]
        public void IsExistsInvalid()
        {
            var firstChild = tree[0];

            bool exists = tree.IsExists("0-1-2-Node1");

            Assert.False(exists);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData(" - ")]
        public void IsExistsCorrect(string key)
        {
            bool exists = tree.IsExists(key);

            Assert.False(exists, string.Format("error invalid key ({0})", key));
        }
    }
}
