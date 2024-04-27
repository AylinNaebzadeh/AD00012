using System.Collections.Generic;

class FindingAnExitFromMaze
{
        public static bool DFS(List<List<long>> graph, long source, long dest, bool[] visited)
        {
            if (source == dest)
            {
                return true;
            }
            if (visited[source])
            {
                return false;
            }
            visited[source] = true;
            foreach (var next in graph[(int)source])
            {
                if (DFS(graph, next, dest, visited))
                {
                    return true;
                }
            }
            return false;
        }
        public static long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            bool[] visited = new bool[nodeCount];
            List<List<long>> graph = new List<List<long>>();
            for (long i = 0; i < nodeCount; i++)
            {
                graph.Add(new List<long>());
            }
            for (int i = 0; i < edges.Length; i++)
            {
                long source = edges[i][0];
                long destination = edges[i][1];
                graph[(int)source - 1].Add(destination - 1);
                graph[(int)destination - 1].Add(source - 1);
            }
            if (DFS(graph, StartNode-=1, EndNode-=1, visited))
            {
                return 1;
            }
            return 0;
        }
}