using System;
using System.Collections.Generic;
using System.Collections;
using Priority_Queue;
using System.Linq;


namespace A33
{
    public class Graph
    {
        long nodeCount;
        public ArrayList[][] adj = new ArrayList[2][];
        public ArrayList[][] costs = new ArrayList[2][];
        public long[][] distance;
        public List<SimplePriorityQueue<long>> queue;
        public bool[] visited;
        public ArrayList intersect;
        const long maxDist = long.MaxValue / 10;

        public List<long> minAnswer = new List<long>();
        public Graph(long NodeCount)
        {
            nodeCount = NodeCount;
            visited = new bool[nodeCount];
            intersect = new ArrayList();
            distance = new long[][]{new long[nodeCount], new long[nodeCount]};
            for (long i = 0; i < nodeCount; i++)
            {
                distance[0][i] = distance[1][i] = maxDist;
            }
            queue = new List<SimplePriorityQueue<long>>();
            queue.Add(new SimplePriorityQueue<long>());
            queue.Add(new SimplePriorityQueue<long>());
        }

        public void clear()
        {
            foreach (long i in intersect)
            {
                distance[0][i] = distance[1][i] = maxDist;
                visited[i] = false;
            }
            intersect.Clear();
            queue[0].Clear();
            queue[1].Clear();
        }
        public void visit(long to_be_checked, long minDist)
        {
            if (distance[0][to_be_checked] > minDist)
            {
                distance[0][to_be_checked] = minDist;
                queue[0].Enqueue(to_be_checked, minDist);
                intersect.Add(to_be_checked);
            }
        }
        public void visitR(long to_be_checked, long minDist)
        {
            if (distance[1][to_be_checked] > minDist)
            {
                distance[1][to_be_checked] = minDist;
                queue[1].Enqueue(to_be_checked, minDist);
                intersect.Add(to_be_checked);
            }
        }
        public void Process(long to_be_checked, ArrayList[] adj, ArrayList[] cost)
        {
            for (int i = 0; i < adj[to_be_checked].Count; i++)
            {
                visit((long)adj[to_be_checked][i], distance[0][to_be_checked] + (long)cost[to_be_checked][i]);
            }
        }
        public void ProcessR(long to_be_checked, ArrayList[] adj, ArrayList[] cost)
        {
            for (int i = 0; i < adj[to_be_checked].Count; i++)
            {
                visitR((long)adj[to_be_checked][i], distance[1][to_be_checked] + (long)cost[to_be_checked][i]);
            }
        }

        public long shortestPath(long to_be_checked)
        {
            long dist = maxDist;
            foreach (long i in intersect)
            {
                if (distance[0][i] + distance[1][i] < dist)
                {
                    dist = distance[0][i] + distance[1][i];
                }
            }
            if (dist  >= maxDist || dist  <= -1)
            {
                return -1;
            }
            return dist;
        }
        public long BidirectionalDijkstra(long s, long t)
        {
            if (s == t)
            {
                return 0;
            }
            clear();
            visit(s, 0);
            visitR(t, 0);

            while (queue[0].Count != 0 && queue[1].Count != 0)
            {
                long to_be_checked = queue[0].Dequeue();
                Process(to_be_checked, adj[0], costs[0]);
                if (visited[to_be_checked])
                {
                    return shortestPath(to_be_checked);
                }
                visited[to_be_checked] = true;

                long to_be_checked_r = queue[1].Dequeue();
                ProcessR(to_be_checked_r, adj[1], costs[1]);
                if (visited[to_be_checked_r])
                {
                    return shortestPath(to_be_checked_r);
                }
                visited[to_be_checked_r] = true;
            }

            return -1;
        }
    }
    public static class Node
    {

    }
    class Program
    {
        #region Q1
        // // dotnet add package OptimizedPriorityQueue --version 5.1.0
        // // https://www.gyanblog.com/coding-interview/min-priority-queue-with-heap/#:~:text=Min%20Priority%20Queue%20is%20a,Gives%20the%20minimum%20priority%20element.
        // public static long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
        // { // value pairs of node and distance
        //     List<List<long>> graph = new List<List<long>>();
        //     List<List<long>> costs = new List<List<long>>();
        //     for (long i = 0; i < nodeCount; i++)
        //     {
        //         graph.Add(new List<long>());
        //         costs.Add(new List<long>());
        //     }
        //     for (long i = 0; i < edges.Length; i++)
        //     {
        //         long source = edges[i][0];
        //         long distance = edges[i][1];
        //         long cost = edges[i][2];
        //         graph[(int)source - 1].Add(distance - 1);
        //         costs[(int)source - 1].Add(cost);
        //     }
        //     bool[] visited = new bool[nodeCount];
        //     long[] dist = new long[nodeCount];
        //     long maxDist = long.MaxValue;
        //     for (long i = 0; i < nodeCount; i++)
        //     {
        //         dist[i] = maxDist;
        //     }

        //     dist[startNode - 1] = 0;
        //     /* item --> node
        //     // priority --> distance
        //     */
        //     SimplePriorityQueue<long, long> pq = new SimplePriorityQueue<long, long>();
        //     pq.Enqueue(startNode - 1, 0);
        //     while (pq.Count != 0)
        //     {
        //         long u = pq.Dequeue();
        //         visited[u] = true;
        //         int i = 0;
        //         foreach (var v in graph[(int)u])
        //         {
        //             long minDist = dist[u] + costs[(int)u][i];
        //             if (!visited[v] && minDist < dist[v])
        //             {
        //                 dist[v] = minDist;
        //                 pq.Enqueue(v, minDist);
        //             }
        //             i++;
        //         }
        //     }

        //     return (dist[endNode - 1] != maxDist) ? dist[endNode - 1] : -1;
        // }
        #endregion
        #region Q2
        public static long Solve(long nodeCount, long[][] edges)
        {
            if (NegCycleDisconnected(nodeCount, edges))
            {
                return 1;
            }
            return 0;
        }
        public static bool BellmanFord(long nodeCount,long[][] edges, long src, long[] dist)
        {
            long maxDist = long.MaxValue;
            for (long i = 0; i < nodeCount; i++)
            {
                dist[i] = maxDist;
            }

            dist[src] = 0;

            for (long i = 1; i <= nodeCount - 1; i++)
            {
                for (long j = 0; j < edges.Length; j++)
                {
                    long u = edges[j][0] - 1;
                    long v = edges[j][1] - 1;
                    long weight = edges[j][2];
                    if (dist[u] != maxDist && dist[u] + weight < dist[v])
                    {
                        dist[v] = dist[u] + weight;
                    }
                }
            }

            for (long i = 0; i < edges.Length; i++)
            {
                    long u = edges[i][0] - 1;
                    long v = edges[i][1] - 1;
                    long weight = edges[i][2];
                    if (dist[u] != maxDist && dist[u] + weight < dist[v])
                    {
                        return true;
                    }
            }
            return false;
        }

        public static bool NegCycleDisconnected(long nodeCount, long[][] edges)
        {
            bool[] visited = new bool[nodeCount];
            long[] dist = new long[nodeCount];
            for (long i = 0; i < nodeCount; i++)
            {
                if (!visited[i])
                {
                    if (BellmanFord(nodeCount, edges, i, dist))
                    {
                        return true;
                    }
                    for (long j = 0; j < nodeCount; j++)
                    {
                        if (dist[j] != long.MaxValue)
                        {
                            visited[j] = true;
                        }
                    }
                }
            }
            return false;
        }
        #endregion
        #region Q3
        public static string[] Solve(long nodeCount, long[][] edges, long startNode)
        {
            /*
            // first initializing two 2d lists for both edges and costs 
            */
            List<List<long>> adj = new List<List<long>>();
            List<List<long>> costs = new List<List<long>>();
            for (long i = 0; i < nodeCount; i++)
            {
                adj.Add(new List<long>());
                costs.Add(new List<long>());
            }
            for (long i = 0; i < edges.Length; i++)
            {
                long source = edges[i][0];
                long distance = edges[i][1];
                long cost = edges[i][2];
                adj[(int)source - 1].Add(distance - 1);
                costs[(int)source - 1].Add(cost);
            }
            /*
            // second create three arrays. 
            // The first one stores the distance between startNode and the other nodes.
            // The second one tells for each node whether it is reachable from the startNode or not.
            // The third one tells for each node whether the path between this node and startNode is shortest or not.
            */
            long[] dist = new long[nodeCount];
            long maxDist = long.MaxValue;
            for (long i = 0; i < nodeCount; i++)
            {
                dist[i] = maxDist;
            }
            bool[] isReachable = new bool[nodeCount];
            bool[] isShortest = new bool[nodeCount]; // it must be initialized with true
            for (long i = 0; i < nodeCount; i++)
            {
                isShortest[i] = true;
            }
            infiniteArbitrage(nodeCount, adj, costs, startNode -= 1, dist, isReachable, isShortest);
            string[] res = new string[nodeCount];
            for (long i = 0; i < res.Length; i++)
            {
                if (!isReachable[i])
                {
                    res[i] = "*";
                }
                else if (!isShortest[i])
                {
                    res[i] = "-";
                }
                else
                {
                    res[i] = dist[i].ToString();
                }
            }
            return res;
        }
        public static void infiniteArbitrage(long nodeCount, List<List<long>> adj, List<List<long>> costs, long start, long[] distance, bool[] reachable, bool[] shortest)
        {
            distance[start] = 0;
            reachable[start] = true;
            Queue<long> queue = new Queue<long>();
            for (long i = 0; i < adj.Count; i++)
            {
                for (int j = 0; j < adj.Count; j++)
                {
                    for (int k = 0; k < adj[(int)j].Count; k++)
                    {
                        long v = adj[(int)j][k];
                        if (distance[j] != long.MaxValue && distance[v] > distance[j] + costs[j][k])
                        {
                            distance[v] = distance[j] + costs[j][k];
                            reachable[v] = true;
                            if (i == adj.Count - 1)
                            {
                                queue.Enqueue(v);
                            }
                        }
                    }
                }
            }
            bool[] visited = new bool[nodeCount];
            while (queue.Count != 0)
            {
                long to_be_checked = queue.Dequeue();
                visited[to_be_checked] = true;
                shortest[to_be_checked] = false;
                for (int i = 0; i < adj[(int)to_be_checked].Count; i++)
                {
                    long v = adj[(int)to_be_checked][i];
                    if (!visited[v])
                    {
                        queue.Enqueue(v);
                        visited[v] = true;
                        shortest[v] = false;
                    }
                }
            }
            distance[start] = 0;
        }
        #endregion

        public static long[] Solve(long NodeCount, long EdgeCount, long[][] edges, long QueriesCount, long[][]Queries)
        {
            Graph graph = new Graph(NodeCount);
            for (long side = 0; side < 2; side++)
            {
                graph.adj[side] = new ArrayList[NodeCount];
                graph.costs[side] = new ArrayList[NodeCount];
                for (long i = 0; i < NodeCount; i++)
                {
                    graph.adj[side][i] = new ArrayList();
                    graph.costs[side][i] = new ArrayList();
                }
            }
            for (long j = 0; j < EdgeCount; j++)
            {
                long x = edges[j][0];
                long y = edges[j][1];
                long z = edges[j][2];
                graph.adj[0][x - 1].Add(y - 1);
                graph.costs[0][x - 1].Add(z);
                graph.adj[1][y - 1].Add(x - 1);
                graph.costs[1][y - 1].Add(z);
            }
            long[] result = new long[QueriesCount];
            for (long j = 0; j < QueriesCount; j++)
            {
                result[j] = graph.BidirectionalDijkstra(Queries[j][0] - 1, Queries[j][1] - 1);
            }
            return result;
        }

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
            // ***************************************************************
            // var first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // long[][] adj = new long[first_line[1]][];
            // for (long i = 0; i < first_line[1]; i++)
            // {
            //     adj[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // }
            // var res = Solve(first_line[0], adj);
            // System.Console.WriteLine(res);
            // ****************************************************************
            // var first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // long[][] adj = new long[first_line[1]][];
            // for (long i = 0; i < first_line[1]; i++)
            // {
            //     adj[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // }
            // var last_line = Console.ReadLine();
            // var res = Solve(first_line[0], adj, Int64.Parse(last_line));
            // for (long i = 0; i < first_line[0]; i++)
            // {
            //     System.Console.WriteLine(res[i]);
            // }
            // *****************************************************************
            var first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            long[][] edges = new long[first_line[1]][];
            for (long i = 0; i < first_line[1]; i++)
            {
                edges[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            }
            var queries_count = Int64.Parse(Console.ReadLine());
            long[][] queries = new long[queries_count][];
            for (long i = 0; i < queries_count; i++)
            {
                queries[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            }
            var res = Solve(first_line[0], first_line[1], edges, queries_count, queries);
            for (long i = 0; i < res.Length; i++)
            {
                System.Console.WriteLine(res[i]);
            }
        }
    }
}
