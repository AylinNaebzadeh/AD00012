using System.Collections.Generic;

class AddingExitToMaze
{
        private static void DFS(long to_be_explored, List<List<long>> edges, bool[] visited)
        {
            if (visited[to_be_explored])
            {
                return;
            }
            visited[to_be_explored] = true;
            for (long i = 0; i < edges[(int)to_be_explored].Count; i++)
            {
                DFS(edges[(int)to_be_explored][(int)i], edges, visited);
            }
        }
        public static long Solve(long nodeCount, long[][] edges)
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
            long res = 0;
            for (long i = 0; i < nodeCount; i++)
            {
                if (!visited[i])
                {
                    DFS(i, graph, visited);
                    res++;
                }
            }
            return res;
        }
}