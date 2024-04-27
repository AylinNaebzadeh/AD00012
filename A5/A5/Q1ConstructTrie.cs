using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q1ConstructTrie : Processor
    {
        public Q1ConstructTrie(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, String[], String[]>) Solve);

 public string[] Solve(long n, string[] patterns)
        {
            // Dictionary<char, int> edges = new Dictionary<char, int>();
            List<Dictionary<char, int>> trie = new List<Dictionary<char, int>>();
            Dictionary<char, int> root = new Dictionary<char, int>();
            trie.Add(root);

            
            foreach (string pattern in patterns)
            {
                Dictionary<char, int> currentNode = root;
                for (int j = 0; j < pattern.Length; j++)
                {
                    char currentSymbol = pattern[j];
                    List<char> childs = currentNode.Keys.ToList();
                    if (childs != null && childs.Contains(currentSymbol))
                    {
                        currentNode = trie[currentNode[currentSymbol]];
                    }
                    else
                    {
                        Dictionary<char, int> newNode = new Dictionary<char, int>();
                        trie.Add(newNode);
                        currentNode.Add(currentSymbol, trie.Count - 1);
                        currentNode = newNode;
                    }
                }
            }
            List<string> result = new List<string>();
            
            for (int i = 0; i < trie.Count; i++)
            {
                Dictionary<char, int> node = trie[i];
                foreach (var entry in node)
                {
                    result.Add($"{i}->{entry.Value}:{entry.Key}");
                }
            }
            return result.ToArray();
        }
    }
}
