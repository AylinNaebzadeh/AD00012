using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q2DetectingAnomalies:Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


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
    }
}
