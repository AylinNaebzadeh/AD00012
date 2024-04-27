using System;
using System.Collections.Generic;

class Q3
{
        /* useful link https://www.geeksforgeeks.org/de-bruijn-sequence-set-1/*/
        private static void DFS(string node, int k, List<int> edges)
        {
            HashSet<string> visited = new HashSet<string>();
            edges = new List<int>();
            
            for (int i = 0; i < k; i++)
            {
                string str = node + (i % 2);
                if (!visited.Contains(str))
                {
                    visited.Add(str);
                    DFS(str.Substring(1), k, edges);
                    edges.Add(i);
                }
            }
        }

        public static string deBruijn(int n)
        {
            List<int> edges = new List<int>();
            string start = "";
            for (int i = 0; i < n - 1; i++)
            {
                start += '0';
            }
            DFS(start, 2, edges);

            string res = "";

            int len = (int)Math.Pow(2, n);
            for (int i = 0; i < len; i++)
            {
                res += (edges[i] % 2);
            }

            res += start;
            return res;
        }
}