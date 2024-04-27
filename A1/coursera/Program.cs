using System;
using System.Collections.Generic;

namespace A112
{
    class Node
    {
        public List<long> adj = new List<long>();
        public List<long> reversed_adj = new List<long>();
    }
    class Program
    {
        #region Q1
        // https://www.baeldung.com/cs/check-if-two-nodes-are-connected

        // public static bool DFS(List<List<long>> graph, long source, long dest, bool[] visited)
        // {
        //     if (source == dest)
        //     {
        //         return true;
        //     }
        //     if (visited[source])
        //     {
        //         return false;
        //     }
        //     visited[source] = true;
        //     foreach (var next in graph[(int)source])
        //     {
        //         if (DFS(graph, next, dest, visited))
        //         {
        //             return true;
        //         }
        //     }
        //     return false;
        // }
        // public static long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        // {
        //     bool[] visited = new bool[nodeCount];
        //     List<List<long>> graph = new List<List<long>>();
        //     for (long i = 0; i < nodeCount; i++)
        //     {
        //         graph.Add(new List<long>());
        //     }
        //     for (int i = 0; i < edges.Length; i++)
        //     {
        //         long source = edges[i][0];
        //         long destination = edges[i][1];
        //         graph[(int)source - 1].Add(destination - 1);
        //         graph[(int)destination - 1].Add(source - 1);
        //     }
        //     if (DFS(graph, StartNode-=1, EndNode-=1, visited))
        //     {
        //         return 1;
        //     }
        //     return 0;
        // }
        #endregion
        #region Q2
        // private static void DFS(long to_be_explored, List<List<long>> edges, bool[] visited)
        // {
        //     if (visited[to_be_explored])
        //     {
        //         return;
        //     }
        //     visited[to_be_explored] = true;
        //     for (long i = 0; i < edges[(int)to_be_explored].Count; i++)
        //     {
        //         DFS(edges[(int)to_be_explored][(int)i], edges, visited);
        //     }
        // }
        // public static long Solve(long nodeCount, long[][] edges)
        // {
        //     bool[] visited = new bool[nodeCount];
        //     List<List<long>> graph = new List<List<long>>();
        //     for (long i = 0; i < nodeCount; i++)
        //     {
        //         graph.Add(new List<long>());
        //     }
        //     for (int i = 0; i < edges.Length; i++)
        //     {
                
        //         long source = edges[i][0];
        //         long destination = edges[i][1];
        //         graph[(int)source - 1].Add(destination - 1);
        //         graph[(int)destination - 1].Add(source - 1);
        //     }
        //     long res = 0;
        //     for (long i = 0; i < nodeCount; i++)
        //     {
        //         if (!visited[i])
        //         {
        //             DFS(i, graph, visited);
        //             res++;
        //         }
        //     }
        //     return res;
        // }
        #endregion

        #region Q3
        //https://stackoverflow.com/questions/583876/how-do-i-check-if-a-directed-graph-is-acyclic
        // public static bool isCyclic(List<List<long>> graph, long to_be_checked, bool[] visited, bool[] marker)
        // {
        //     if (marker[to_be_checked])
        //     {
        //         return true;
        //     }
        //     if (visited[to_be_checked])
        //     {
        //         return false;
        //     }
        //     marker[to_be_checked] = true;
        //     visited[to_be_checked] = true;
        //     if (graph[(int)to_be_checked] != null)
        //     {
        //         foreach (var v in graph[(int)to_be_checked])
        //         {
        //             if (isCyclic(graph, v, visited, marker))
        //             {
        //                 return true;
        //             }
        //         }
        //     }
        //     marker[to_be_checked] = false;
        //     return false;
        // }
        // public static long Solve(long nodeCount, long[][] edges)
        // {
        //     bool[] visited = new bool[nodeCount];
        //     bool[] marker = new bool[nodeCount];
        //     List<List<long>> graph = new List<List<long>>();
        //     for (long i = 0; i < nodeCount; i++)
        //     {
        //         graph.Add(new List<long>());
        //     }
        //     for (long i = 0; i < edges.Length; i++)
        //     {
        //         long source = edges[i][0];
        //         long dest = edges[i][1];
        //         graph[(int)source - 1].Add(dest - 1);
        //     }
        //     for (long i = 0; i < nodeCount; i++)
        //     {
        //         if (!visited[i] && isCyclic(graph, i, visited, marker))
        //         {
        //             return 1;
        //         }
        //     }
        //     return 0;
        // }
        #endregion

        #region Q4
        // https://www.tutorialspoint.com/Topological-Sorting
        // public static void topologicalSorting(long to_be_sorted, bool[] visited, Stack<long> stack, List<List<long>> graph)
        // {
        //     visited[to_be_sorted] = true;
        //     foreach (var v in graph[(int)to_be_sorted])
        //     {
        //         if (!visited[v])
        //         {
        //             topologicalSorting(v, visited, stack, graph);
        //         }
        //     }
        //     stack.Push(to_be_sorted);
        // }
        // public static long[] Solve(long nodeCount, long[][] edges)
        // {
        //     Stack<long> stack = new Stack<long>();
        //     bool[] visited = new bool[nodeCount];
        //     List<List<long>> graph = new List<List<long>>();
        //     for (long i = 0; i < nodeCount; i++)
        //     {
        //         graph.Add(new List<long>());
        //     }
        //     for (long i = 0; i < edges.Length; i++)
        //     {
        //         long source = edges[i][0];
        //         long dest = edges[i][1];
        //         graph[(int)source - 1].Add(dest - 1);
        //     }
        //     for (long i = 0; i < nodeCount; i++)
        //     {
        //         if (!visited[i])
        //         {
        //             topologicalSorting(i, visited, stack, graph);
        //         }
        //     }
        //     long[] res = new long[nodeCount];
        //     long j = 0;
        //     while (stack.Count != 0)
        //     {
        //         res[j] = stack.Pop() + 1;
        //         j++;
        //     }
        //     return res;
        // }
        #endregion

        #region Q5
        // https://www.topcoder.com/thrive/articles/kosarajus-algorithm-for-strongly-connected-components
        public static void DFS_Adj(long to_be_explored, bool[] visited, Stack<long> stack, List<Node> graph)
        {
            visited[to_be_explored] = true;
            for (long i = 0; i < graph[(int)to_be_explored].adj.Count; i++)
            {
                if (!visited[graph[(int)to_be_explored].adj[(int)i]])
                {
                    DFS_Adj(graph[(int)to_be_explored].adj[(int)i], visited, stack, graph);
                }
            }
            stack.Push(to_be_explored);
        }
        public static void DFS_Rev_Adj(long to_be_explored, bool[] visited, List<Node> graph)
        {
            visited[to_be_explored] = true;
            for (long i = 0; i < graph[(int)to_be_explored].reversed_adj.Count; i++)
            {
                if (!visited[graph[(int)to_be_explored].reversed_adj[(int)i]])
                {
                    DFS_Rev_Adj(graph[(int)to_be_explored].reversed_adj[(int)i], visited, graph);
                }
            }
        }
        public static long Solve(long nodeCount, long[][] edges)
        {
            bool[] visited = new bool[nodeCount];
            long number_of_components = 0;
            Stack<long> stack = new Stack<long>();
            List<Node> graph = new List<Node>();
            for (long i = 0; i < nodeCount; i++)
            {
                graph.Add(new Node());
            }
            for (long i = 0; i < edges.Length; i++)
            {
                long source = edges[i][0];
                long dest = edges[i][1];
                graph[(int)source - 1].adj.Add(dest - 1);
                graph[(int)dest - 1].reversed_adj.Add(source - 1);
            }
            for (long i = 0; i < nodeCount; i++)
            {
                if (!visited[i])
                {
                    DFS_Adj(i, visited, stack, graph);
                }
            }
            for (long i = 0; i < nodeCount; i++)
            {
                visited[i] = false;
            }
            while (stack.Count != 0)
            {
                long vertex = stack.Pop();
                if (!visited[vertex])
                {
                    DFS_Rev_Adj(vertex, visited, graph);
                    number_of_components++;
                }
            }
            return number_of_components;
        }
        #endregion
        static void Main(string[] args)
        {
            // var first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // long[][] adj = new long[first_line[1]][];

            // for (long i = 0; i < first_line[1]; i++)
            // {
            //     adj[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // }

            // var last_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // var res = Solve(first_line[0], adj, last_line[0], last_line[1]);
            // System.Console.WriteLine(res);
            // **************************
            // var first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // long[][] adj = new long[first_line[1]][];

            // for (long i = 0; i < first_line[1]; i++)
            // {
            //     adj[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // }
            // System.Console.WriteLine(Solve(first_line[0], adj));
            // ***************************
            // var first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // long[][] adj = new long[first_line[1]][];

            // for (long i = 0; i < first_line[1]; i++)
            // {
            //     adj[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // }
            // System.Console.WriteLine(Solve(first_line[0], adj));
            // *****************************
            // var first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // long[][] adj = new long[first_line[1]][];
            // for (long i = 0; i < first_line[1]; i++)
            // {
            //     adj[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // }
            // var res = string.Join(" ", Solve(first_line[0], adj));
            // System.Console.WriteLine(res);
            // ******************************
            var first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            long[][] adj = new long[first_line[1]][];

            for (long i = 0; i < first_line[1]; i++)
            {
                adj[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            }
            System.Console.WriteLine(Solve(first_line[0], adj));
        }
    }
}
