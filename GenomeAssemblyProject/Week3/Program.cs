using System;
using System.Collections.Generic;
using System.Linq;

namespace A14
{
    class Program
    {
        #region Q1
        private static bool BFS(int source, int target, int[,] graph, int[] parents)
        {
            int size = graph.GetLength(0);
            bool[] visited = new bool[size];
            Queue<int> bfs = new Queue<int>();
            bfs.Enqueue(source);
            visited[source] = true;

            while (bfs.Count > 0)
            {
                int to_be_checked = bfs.Dequeue();

                if (target == to_be_checked)
                {
                    break;
                }

                for (int i = 0; i < size; i++)
                {
                    if (!visited[i] && graph[to_be_checked, i] > 0)
                    {
                        visited[i] = true;
                        parents[i] = to_be_checked;
                        bfs.Enqueue(i);
                    }
                }
            }
            return visited[target];
        }

        private static int calculateMaxFlow(int[, ] graph, int source, int target)
        {
            int size = graph.GetLength(0);
            int[] parent = new int[size];
            for (int i = 0; i < size; i++)
            {
                parent[i] = -1;
            }
            int res = 0;

            while (BFS(source, target, graph, parent))
            {
                int currentFlow = int.MaxValue;
                int currentNode = target;
                while (currentNode != source)
                {
                    currentFlow = Math.Min(currentFlow, graph[parent[currentNode], currentNode]);
                    currentNode = parent[currentNode];
                }

                currentNode = target;
                while (currentNode != source)
                {
                    graph[parent[currentNode], currentNode] -= currentFlow;
                    graph[currentNode, parent[currentNode]] += currentFlow;
                    currentNode = parent[currentNode];
                }

                res += currentFlow;
            }
            return res;
        }
        #endregion
        #region Q2
        private static bool optimalKmer(int k, List<string> reads)
        {
            HashSet<string> kmers = new HashSet<string>();
            foreach (var read in reads)
            {
                for (int i = 0; i < read.Length - k + 1; i++)
                {
                    kmers.Add(read.Substring(i, k));
                }
            }

            HashSet<string> prefix = new HashSet<string>();
            HashSet<string> suffix = new HashSet<string>();

            foreach (var kmer in kmers)
            {
                prefix.Add(kmer.Substring(0, kmer.Length - 1));
                suffix.Add(kmer.Substring(1));
            }

            foreach (var pre in prefix)
            {
                if (!suffix.Contains(pre))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        
        static void Main(string[] args)
        {
            #region Q1Driver
            // V E
            int[] first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToInt32);
            int V = first_line[0] + 2;
            int[,] graph = new int[V, V];
            int[, ] edges = new int[first_line[1], 4];

            int totalLower = 0;

            for (int i = 0; i < first_line[1]; i++)
            {
                var edge = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToInt32);
                int u = edge[0];
                int v = edge[1];
                int l = edge[2];
                int c = edge[3];

                edges[i, 0] = u;
                edges[i, 1] = v;
                edges[i, 2] = l;
                edges[i, 3] = c;

                graph[u, v] += (c - l);
                graph[0, v] += l;
                graph[u, V - 1] += l;
                totalLower += l;
            }

            int maxFlow = calculateMaxFlow(graph, 0, V - 1);
            if (maxFlow == totalLower) 
            {
                Console.WriteLine("YES");
                for (int i = 0; i < first_line[1]; ++i) 
                {
                    if (graph[edges[i, 1], edges[i, 0]] > edges[i, 3] - edges[i, 2]) 
                    {
                        Console.WriteLine(edges[i, 3]);
                        graph[edges[i, 1], edges[i, 0]] -= edges[i, 3] - edges[i, 2];
                    } 
                    else 
                    {
                        Console.WriteLine(graph[edges[i, 1], edges[i, 0]] + edges[i, 2]);
                        graph[edges[i, 1], edges[i, 0]] = 0;
                    }
                }
            } 
            else 
            {
                Console.WriteLine("NO");
            }
            #endregion
            #region Q2Driver
            List<string> reads = new List<string>();
            string s = "";
            while (!string.IsNullOrEmpty(s = Console.ReadLine()))
            {
                reads.Add(s);
            }

            for (int i = reads[0].Length; i > 1; i--)
            {
                if (optimalKmer(i, reads))
                {
                    Console.WriteLine(i);
                    break;
                }
            }
            #endregion
            #region Q3Driver

            #endregion
        }
    }
}
