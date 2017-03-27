using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace src.Models
{
    public class Node: IEnumerable<Node>
    {
        public Node this[int index] { get { return children[index]; } }
        public int Count { get { return children.Count; } }

        public string Name { get; private set; }
        public string Data { get; set; } = string.Empty;

        public Node Parent
        {
            get { return parent; }
            private set
            {
                if (parent != null) parent.children.Remove(this);
                parent = value;
            }
        }

        public Node Active { get; private set; } = null;

        Node parent;

        List<Node> children = new List<Node>();

        Node(string name)
        {
            Name = name;
        }

        public void Add(Node node)
        {
            node.Parent = this;
            children.Add(node);
        }
        public void Insert(int index, Node node)
        {
            node.Parent = this;
            children.Insert(index, node);
        }

        public void Remove(Node node)
        {
            node.Parent = null;
            children.Remove(node);
        }

        public static Node Create(string name, string data = "")
        {
            return new Node(name) { Data = data };
        }

        public string GetPath()
        {
            List<string> pathList = new List<string>();
            Node node = this;
            pathList.Add(node.Name);
            while (node.Parent != null)
            {
                pathList.Add(node.Parent.children.IndexOf(node).ToString());
                node = node.Parent;
            }
            pathList.Reverse();
            return string.Join("-", pathList);
        }

        public bool IsExists(string path)
        {
            Node node;
            return TryGetNode(path, out node);
        }

        internal void SetActive(string path)
        {
            Node node;
            if (TryGetNode(path, out node)) Active = node;
            else throw new InvalidOperationException(string.Format("Wrong path ({0)}", path));
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return children.GetEnumerator();
        }

        bool TryGetNode(string path, out Node node)
        {
            node = this;

            if (string.IsNullOrEmpty(path)) return false;

            var pathList = path.Split('-').ToList();
            string nameNode = pathList.Last();
            pathList.RemoveAt(pathList.Count - 1);
            foreach (string indexNode in pathList)
            {
                int index;
                if (int.TryParse(indexNode, out index))
                {
                    if (0 <= index && index < node.Count)
                    {
                        node = node[index];
                    }
                    else return false;
                }
                else return false;
            }
            return node.Name == nameNode;
        }

    }
}
