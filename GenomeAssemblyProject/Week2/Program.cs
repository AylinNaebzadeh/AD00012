using System;
using System.Collections.Generic;
using System.Linq;


namespace A13
{
    class Program
    {
        #region Q1
        private static List<int> findingEulerianCycle (List<int>[] adj)
        {

            if (adj.Length == 0)
            {
                return new List<int>();
            }
            Dictionary<int, int> nodeCount = new Dictionary<int, int>();
            for (int i = 0; i < adj.Length; i++)
            {
                nodeCount[i] = adj[i].Count;
            }

            List<int> path = new List<int>();
            List<int> cycle = new List<int>();
            path.Add(0);
            int deadend = 0;
            while (path.Count > 0)
            {
                if (nodeCount[deadend] != 0)
                {
                    path.Add(deadend);
                    int next = adj[deadend].Last();
                    nodeCount[deadend] -= 1;
                    adj[deadend].RemoveAt(adj[deadend].Count - 1);
                    deadend = next;
                }
                else
                {
                    cycle.Add(deadend);
                    deadend = path.Last();
                    path.RemoveAt(path.Count - 1);
                }
            }
            return cycle;
        }

        private static bool checkForDegrees (Dictionary<int, int> inEdges, Dictionary<int, int> outEdges)
        {
            foreach (var key in inEdges.Keys)
            {
                if (inEdges[key] != inEdges[key])
                {
                    return false;
                }
            }
            return true;
        }
        #endregion Q1

        #region Q2
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
        #endregion Q2
        static void Main(string[] args)
        {
            #region DriveQ1
            // n-->v, m-->e
            // int[] first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToInt32);
            // List<int>[] adj = new List<int>[first_line[0]];
            // for (int i = 0; i < first_line[0]; i++)
            // {
            //     adj[i] = new List<int>();
            // }
            // Dictionary<int, int> inEdges = new Dictionary<int, int>();
            // Dictionary<int, int> outEdges = new Dictionary<int, int>();
            // for (int i = 0; i < first_line[0]; i++)
            // {
            //     inEdges[i] = 0;
            //     outEdges[i] = 0;
            // }
            // int[] edge;
            // for (int i = 0; i < first_line[1]; i++)
            // {
            //     edge = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToInt32);
            //     int u = edge[0] - 1;
            //     int v = edge[1] - 1;
            //     adj[u].Add(v);
            //     inEdges[v]++;
            //     outEdges[u]++;
            // }


            // var res = findingEulerianCycle(adj);
            // bool flag = false;
            // foreach (var item in inEdges.Keys)
            // {
            //     if(inEdges[item] != outEdges[item])
            //         flag = true;
            // }
            // if (flag)
            // {
            //     System.Console.WriteLine(0);
            // }
            // else
            // {
            //     System.Console.WriteLine(1);
            //     for (int i = res.Count - 1; i > 0; i--)
            //     {
            //         System.Console.Write(res[i] + 1);
            //         Console.Write(" ");
            //     }
            // }
            #endregion
            #region DriveQ2
            int input = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine(deBruijn(input));
            #endregion
        }
    }
}
