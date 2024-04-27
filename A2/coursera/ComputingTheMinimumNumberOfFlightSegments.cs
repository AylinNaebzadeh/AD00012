class ShortestPath
{
    public static long Solve(long NodeCount, long[][] edges, long StartNode,  long EndNode)
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
        // bool[] visited = new bool[NodeCount];
        long[] distance = new long[NodeCount];
        // for (long i = 0; i < NodeCount; i++)
        // {
        //     distance[i] = Int64.MaxValue;
        // }
        Queue<long> queue = new Queue<long>();
        
        distance[StartNode - 1] = 0;
        queue.Enqueue(StartNode - 1);
        // visited[StartNode - 1] = true;
        while (queue.Count != 0)
        {
            long x = queue.Dequeue();
            foreach (var v in graph[(int)x])
            {
                if (distance[v] == 0)
                {
                    queue.Enqueue(v);
                    distance[v] = distance[x] + 1;
                }
            }
        }
        if (distance[EndNode - 1] == 0)
        {
            return -1;
        }
        return distance[EndNode - 1];
    }
}