using System;
using System.Collections.Generic;
using TestCommon;
namespace C3
{
    public class Q3ExchangingMoney : Processor
    {
        public Q3ExchangingMoney(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, string[]>)Solve);


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
    }
}
