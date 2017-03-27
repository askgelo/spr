using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using src.Models;

namespace src.Services
{
    public class TreeService : ITreeService
    {
        const string rootName = "Категории";

        Storage storage;
        Dictionary<string, Node> files = new Dictionary<string, Node>();

        public TreeService(Storage storage)
        {
            this.storage = storage;
        }

        public Node GetTree(string dwp, string filename = null)
        {
            Node tree;
            if (!files.ContainsKey(dwp))
            {
                var data = storage.GetData(filename);
                if (data == null)
                {
                    if (filename != null) throw new InvalidOperationException(string.Format("file ({0}) not exists", filename));
                    else tree = Node.Create(rootName);
                    tree.Add(Node.Create("as1!"));
                    var treeNode = Node.Create("as2!");
                    treeNode.Add(Node.Create("sub as 1"));
                    treeNode.Add(Node.Create("sub as 2"));
                    var subNode = Node.Create("sub as 3");
                    subNode.Add(Node.Create("sub sub as 1 long name on some words"));
                    treeNode.Add(subNode);
                    treeNode.Add(Node.Create("sub as 4"));
                    tree.Add(treeNode);
                    tree.Add(Node.Create("as3!"));
                    tree.Add(Node.Create(string.Concat(Enumerable.Repeat("Very long name...", 10))));
                    for (int i = 0; i < 20; i++)
                    {
                        tree.Add(Node.Create(i.ToString()));
                    }
                }
                else
                {
                    string json = Crypt.GetDecrypted(dwp, data);
                    tree = JsonConvert.DeserializeObject<Node>(json);
                }
                files.Add(dwp, tree);
            }
            else tree = files[dwp];

            return tree;
        }

        public void SaveTree(string dwp)
        {
            if (!files.ContainsKey(dwp))
                throw new InvalidOperationException(string.Format("tree with dwp {0} not exists", dwp));

            string json = JsonConvert.SerializeObject(files[dwp]);
            var data = Crypt.GetEncrypted(dwp, json);
            storage.SaveData(data);
        }
    }
}
