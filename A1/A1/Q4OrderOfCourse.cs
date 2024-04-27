using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q4OrderOfCourse: Processor
    {
        public Q4OrderOfCourse(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);

        public static void topologicalSorting(long to_be_sorted, bool[] visited, Stack<long> stack, List<List<long>> graph)
        {
            visited[to_be_sorted] = true;
            foreach (var v in graph[(int)to_be_sorted])
            {
                if (!visited[v])
                {
                    topologicalSorting(v, visited, stack, graph);
                }
            }
            stack.Push(to_be_sorted);
        }
        public static long[] Solve(long nodeCount, long[][] edges)
        {
            Stack<long> stack = new Stack<long>();
            bool[] visited = new bool[nodeCount];
            List<List<long>> graph = new List<List<long>>();
            for (long i = 0; i < nodeCount; i++)
            {
                graph.Add(new List<long>());
            }
            for (long i = 0; i < edges.Length; i++)
            {
                long source = edges[i][0];
                long dest = edges[i][1];
                graph[(int)source - 1].Add(dest - 1);
            }
            for (long i = 0; i < nodeCount; i++)
            {
                if (!visited[i])
                {
                    topologicalSorting(i, visited, stack, graph);
                }
            }
            long[] res = new long[nodeCount];
            long j = 0;
            while (stack.Count != 0)
            {
                res[j] = stack.Pop() + 1;
                j++;
            }
            return res;
        }

        public override Action<string, string> Verifier { get; set; } = TopSortVerifier;

        public static void TopSortVerifier(string inFileName, string strResult)
        {
            long[] topOrder = strResult.Split(TestTools.IgnoreChars)
                .Select(x => long.Parse(x)).ToArray();

            long count;
            long[][] edges;
            TestTools.ParseGraph(File.ReadAllText(inFileName), out count, out edges);

            // Build an array for looking up the position of each node in topological order
            // for example if topological order is 2 3 4 1, topOrderPositions[2] = 0, 
            // because 2 is first in topological order.
            long[] topOrderPositions = new long[count];
            for (int i = 0; i < topOrder.Length; i++)
                topOrderPositions[topOrder[i] - 1] = i;
            // Top Order nodes is 1 based (not zero based).

            // Make sure all direct depedencies (edges) of the graph are met:
            //   For all directed edges u -> v, u appears before v in the list
            foreach (var edge in edges)
                if (topOrderPositions[edge[0] - 1] >= topOrderPositions[edge[1] - 1])
                    throw new InvalidDataException(
                        $"{Path.GetFileName(inFileName)}: " +
                        $"Edge dependency violoation: {edge[0]}->{edge[1]}");

        }
    }
}
