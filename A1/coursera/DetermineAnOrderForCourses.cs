using System.Collections.Generic;

class DetermineOrderforCourses
{
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
}