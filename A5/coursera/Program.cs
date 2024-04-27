using System;
using System.Collections.Generic;
using System.Linq;
namespace A55
{
    class Node
    {
        public Dictionary<char, Node> children = new Dictionary<char, Node>();
        public bool isLeaf = false;
    }


    class Program
    {
        #region Q1
        public static string[] Solve(long n, string[] patterns)
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
                    result.Add(i.ToString() + "->" + (entry.Value).ToString() + ":" + (entry.Key).ToString());
                }
            }
            return result.ToArray();
        }
        #endregion
        #region Q2
        #endregion
        #region Q3
        public static long[] Solve(string text, long n, string[] patterns)
        {
            List<long> result = new List<long>();
            Node root = new Node();
            foreach (var pattern in patterns)
            {
                Node currentNode = root;
                for (int i = 0; i < pattern.Length; i++)
                {
                    char currentSymbol = pattern[i];
                    // The new if for duplicate patterns
                    if (!currentNode.children.ContainsKey(currentSymbol))
                    {
                        currentNode.children[currentSymbol] = new Node();
                    }
                    if (pattern.Length - 1 == i)
                    {
                        currentNode.children[currentSymbol].isLeaf = true;
                    }
                    

                    else
                    {
                        currentNode = currentNode.children[currentSymbol];
                    }
                }
            }
            // This part is to check prefixTrieMatching
            for (int i = 0; i < text.Length; i++)
            {
                int position = i;
                Node currentNode = root;
                while (position < text.Length)
                {
                    char currentSymbol = text[position];
                    if (!currentNode.children.ContainsKey(currentSymbol))
                    {
                        break;
                    }
                    currentNode = currentNode.children[currentSymbol];
                    if (currentNode.isLeaf)
                    {
                        result.Add(i);
                        break;
                    }
                    position++;
                }
            }
            if (result.Count == 0)
            {
                return new long[]{};
            }
            result.Sort();
            return result.ToArray();
        }
        #endregion
        static void Main(string[] args)
        {
            // int noOfpatterns = Convert.ToInt32(Console.ReadLine());
            // string[] patterns = new string[noOfpatterns];
            // for (int i = 0; i < noOfpatterns; i++)
            // {
            //     patterns[i] = Console.ReadLine();
            // }
            // var res = Solve(noOfpatterns, patterns);
            // for (int i = 0; i < res.Length; i++)
            // {
            //     System.Console.WriteLine(res[i]);
            // }
            // *************************************************************
            string t = Console.ReadLine();
            int noOfpatterns = Convert.ToInt32(Console.ReadLine());
            string[] patterns = new string[noOfpatterns];

            for (int i = 0; i < noOfpatterns; i++)
            {
                patterns[i] = Console.ReadLine();
            }

            var res = string.Join(" ", Solve(t, noOfpatterns, patterns));

            System.Console.WriteLine(res);

        }
    }
}
