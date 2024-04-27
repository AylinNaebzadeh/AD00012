using System;
using System.Collections.Generic;
using TestCommon;

namespace A2
{
    public class Q2BipartiteGraph : Processor
    {
        public Q2BipartiteGraph(string testDataName) : base(testDataName) { ExcludeTestCases(5);}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

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
    }
}
