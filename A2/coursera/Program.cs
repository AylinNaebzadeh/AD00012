using System;
using System.Collections.Generic;

namespace A22
{
    class Program
    {
        #region Q1
        // public static long Solve(long NodeCount, long[][] edges, long StartNode,  long EndNode)
        // {
        //     List<List<long>> graph = new List<List<long>>();
        //     for (long i = 0; i < NodeCount; i++)
        //     {
        //         graph.Add(new List<long>());
        //     }
        //     for (long i = 0; i < edges.Length; i++)
        //     {
        //         long source = edges[i][0];
        //         long dist = edges[i][1];
        //         graph[(int)source - 1].Add(dist - 1);
        //         graph[(int)dist - 1].Add(source - 1);
        //     }
        //     // bool[] visited = new bool[NodeCount];
        //     long[] distance = new long[NodeCount];
        //     // for (long i = 0; i < NodeCount; i++)
        //     // {
        //     //     distance[i] = Int64.MaxValue;
        //     // }
        //     Queue<long> queue = new Queue<long>();
            
        //     distance[StartNode - 1] = 0;
        //     queue.Enqueue(StartNode - 1);
        //     // visited[StartNode - 1] = true;
        //     while (queue.Count != 0)
        //     {
        //         long x = queue.Dequeue();
        //         foreach (var v in graph[(int)x])
        //         {
        //             if (distance[v] == 0)
        //             {
        //                 queue.Enqueue(v);
        //                 distance[v] = distance[x] + 1;
        //             }
        //         }
        //     }
        //     if (distance[EndNode - 1] == 0)
        //     {
        //         return -1;
        //     }
        //     return distance[EndNode - 1];
        // }
        #endregion
        #region Q2
        // https://www.baeldung.com/cs/graphs-bipartite
        public static long Solve(long NodeCount, long[][] edges)
        { 
            List<List<long>> graph = new List<List<long>>();
            for (long i = 0; i < NodeCount; i++)
            {
                graph.Add(new List<long>());
            }
            for (long i = 0; i < edges.Length; i++)
            {
                long source = edges[i][0];
                long dist = edges[i][1];
                graph[(int)source - 1].Add(dist - 1);
                graph[(int)dist - 1].Add(source - 1);
            }
            string[] color = new string[NodeCount];
            color[graph[0][0]] = "RED";
            Queue<long> queue = new Queue<long>();
            queue.Enqueue(graph[0][0]);
            while (queue.Count != 0)
            {
                long x = queue.Dequeue();
                foreach (var v in graph[(int)x])
                {
                    if (color[v] == null)
                    {
                        color[v] = (color[x] == "RED") ? "BLUE" : "RED";
                        queue.Enqueue(v);
                    }
                    else if (color[v] == color[x])
                    {
                        return 0;
                    }
                }
            }
            return 1;
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
            // *********************************
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
