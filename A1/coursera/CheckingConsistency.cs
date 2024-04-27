using System.Collections.Generic;

class CheckingConsistency
{
        public static bool isCyclic(List<List<long>> graph, long to_be_checked, bool[] visited, bool[] marker)
        {
            if (marker[to_be_checked])
            {
                return true;
            }
            if (visited[to_be_checked])
            {
                return false;
            }
            marker[to_be_checked] = true;
            visited[to_be_checked] = true;
            if (graph[(int)to_be_checked] != null)
            {
                foreach (var v in graph[(int)to_be_checked])
                {
                    if (isCyclic(graph, v, visited, marker))
                    {
                        return true;
                    }
                }
            }
            marker[to_be_checked] = false;
            return false;
        }
        public static long Solve(long nodeCount, long[][] edges)
        {
            bool[] visited = new bool[nodeCount];
            bool[] marker = new bool[nodeCount];
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
                if (!visited[i] && isCyclic(graph, i, visited, marker))
                {
                    return 1;
                }
            }
            return 0;
        }
}