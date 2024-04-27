using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
using TestCommon;

namespace C3
{
    public class Q1MinCost : Processor
    {
        public Q1MinCost(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);


        public long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
        {
            List<List<long>> graph = new List<List<long>>();
            List<List<long>> costs = new List<List<long>>();
            for (long i = 0; i < nodeCount; i++)
            {
                graph.Add(new List<long>());
                costs.Add(new List<long>());
            }
            for (long i = 0; i < edges.Length; i++)
            {
                long source = edges[i][0];
                long distance = edges[i][1];
                long cost = edges[i][2];
                graph[(int)source - 1].Add(distance - 1);
                costs[(int)source - 1].Add(cost);
            }
            bool[] visited = new bool[nodeCount];
            long[] dist = new long[nodeCount];
            long maxDist = long.MaxValue;
            for (long i = 0; i < nodeCount; i++)
            {
                dist[i] = maxDist;
            }

            dist[startNode - 1] = 0;
            /* 
            item --> node
            priority --> distance
            */
            SimplePriorityQueue<long, long> pq = new SimplePriorityQueue<long, long>();
            pq.Enqueue(startNode - 1, 0);
            while (pq.Count != 0)
            {
                long u = pq.Dequeue();
                visited[u] = true;
                int i = 0;
                foreach (var v in graph[(int)u])
                {
                    long minDist = dist[u] + costs[(int)u][i];
                    if (!visited[v] && minDist < dist[v])
                    {
                        dist[v] = minDist;
                        pq.Enqueue(v, minDist);
                    }
                    i++;
                }
            }

            return (dist[endNode - 1] != maxDist) ? dist[endNode - 1] : -1;
        }
    }
}
