using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q1Evaquating : Processor
    {
        public Q1Evaquating(string testDataName) : base(testDataName) { ExcludeTestCaseRangeInclusive(34, 38);}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public static long BFS(long s, long t, long[] parent, List<List<long>> adj, long[,] capacity)
        {
            Array.Fill(parent, -1);
            parent[s] = -2;
            Queue<Tuple<long, long>> q = new Queue<Tuple<long, long>>();
            q.Enqueue(Tuple.Create(s, long.MaxValue));

            while (q.Count != 0)
            {
                long current = q.Peek().Item1;
                long flow = q.Peek().Item2;
                q.Dequeue();

                foreach (var next in adj[(int)current])
                {
                    if (parent[next] == -1 && capacity[current, next] != 0)
                    {
                        parent[next] = current;
                        long new_flow = Math.Min(flow, capacity[current, next]);
                        if (next == t)
                        {
                            return new_flow;
                        }
                        q.Enqueue(Tuple.Create(next, new_flow));
                    }
                }
            }
            return 0;
        }
        public static long maxFlow(long s, long t, long nodeCount, long[,] capacity, List<List<long>> adj)
        {
            long flow = 0;
            long[] parent = new long[nodeCount];
            long newFlow = 0;

            while ((newFlow = BFS(s, t, parent, adj, capacity)) != 0)
            {
                flow += newFlow;
                long current = t;
                while (current != s)
                {
                    long prev = parent[current];
                    capacity[prev, current] -= newFlow;
                    capacity[current, prev] += newFlow;
                    current = prev;
                }
            }
            return flow;
        }
        public static long Solve(long nodeCount, long edgeCount, long[][] edges)
        {
            if (edgeCount == 0)
            {
                return 0;
            }
            if (edgeCount == 1)
            {
                return edges[0][2];
            }
            long[,] capacity = new long[edgeCount, edgeCount];
            List<List<long>> adj = new List<List<long>>();
            for (int i = 0; i < nodeCount; i++)
            {
                adj.Add(new List<long>());
            }
            for (int i = 0; i < edgeCount; i++)
            {
                long source = edges[i][0];
                long target = edges[i][1];
                adj[(int) source - 1].Add(target - 1);
                capacity[source - 1, target - 1] += edges[i][2];
            }
            return maxFlow(0, nodeCount - 1, nodeCount, capacity, adj);
        }
    }
}
