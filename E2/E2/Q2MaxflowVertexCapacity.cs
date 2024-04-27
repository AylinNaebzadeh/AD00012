using TestCommon;

namespace E2;
/*
public class Edge
{
    public long from;
    public long to;
    public long capacity;
    public long flow;
    public Edge(long _from, long _to, long _capacity, long _flow)
    {
        this.from = _from;
        this.to = _to;
        this.capacity = _capacity;
        this.flow = _flow;
    }
}
*/
public class Network
{
    public List<List<long>> adj = new List<List<long>>();
    public long[,] res;

    public Network(long nodeCount)
    {
        
        for (int i = 0; i < nodeCount; i++)
        {
            adj.Add(new List<long>());
        }
        res = new long[nodeCount, nodeCount];
    }

    public void addEdge(long from, long to, long capacity)
    {
        adj[(int)from].Add(to);
        adj[(int)to].Add(from);
        res[from, to] += capacity;
    }
    public List<long> BFS(long from, long to, long nodeCount)
    {
        long[] parents = new long[nodeCount];
        long[] visited = new long[nodeCount];
        visited[from] = 1;
        Queue<long> queue = new Queue<long>();
        queue.Enqueue(from);
        long last = 1;
        for (int i = 0; i < nodeCount; i++)
        {
            Queue<long> tmpQ = new Queue<long>();
            while (queue.Count != 0)
            {
                long to_be_checked = queue.Dequeue();
                foreach (long neighbor in adj[(int)to_be_checked])
                {
                    if (visited[neighbor] == 0 && res[to_be_checked, neighbor] != 0)
                    {
                        parents[neighbor] = to_be_checked;
                        tmpQ.Enqueue(neighbor);
                        visited[neighbor] = last + 1;
                    }
                }
            }
            last++;
            queue = tmpQ;
        }
        List<long> result = new List<long>();
        if (visited[to] != 0)
        {
            result.Add(to);
            while (from != to)
            {
                to = parents[to];
                result.Add(to);
            }
        }
        result.Reverse();
        return result;
    }
}
public class Q2MaxflowVertexCapacity : Processor
{
    public Q2MaxflowVertexCapacity(string testDataName) : base(testDataName)
    {
        // this.ExcludeTestCases(1, 2, 3);
        // this.ExcludeTestCaseRangeInclusive(1, 3);
    }
    public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long[], long, long, long>)Solve);

            public virtual long Solve(long nodeCount, long edgeCount, long[][] edges, long[] nodeWeight, long startNode , long endNode)
        {
            Network flowGraph = new Network(nodeCount);
            foreach (var edge in edges)
            {
                var legalFlow = Math.Min(nodeWeight[edge[0] - 1], nodeWeight[edge[1] - 1]);
                if (legalFlow < edge[2])
                    flowGraph.addEdge(edge[0] - 1, edge[1] - 1, legalFlow);
                else
                    flowGraph.addEdge(edge[0] - 1, edge[1] - 1, edge[2]);
            }
            long sum = 0;
            while (true)
            {
                var path = flowGraph.BFS(startNode - 1, endNode - 1, nodeCount);
                if (path.Count == 0)
                {
                    return sum;
                }
                else
                {
                    long min = int.MaxValue;
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        min = Math.Min(min, flowGraph.res[path[i], path[i + 1]]);
                        min = Math.Min(min, nodeWeight[path[i]]);
                        min = Math.Min(min, nodeWeight[path[i + 1]]);
                    }
                    bool check = false;
                    for (int i = 0; i < path.Count; i++) 
                    {
                        if (nodeWeight[path[i]] <= 0)
                            check = true;
                    }
                    if (!check)
                    {
                        sum += min;
                        for (int i = 0; i < path.Count - 1; i++)
                        {
                            flowGraph.res[path[i], path[i + 1]] -= min;
                            flowGraph.res[path[i + 1], path[i]] += min;
                        }
                        foreach (var node in path)
                        {
                            nodeWeight[node] -= min;
                        }
                        foreach (var node in nodeWeight)
                        {
                            if (node < 0)
                                return sum-min;
                        }
                    }
                    else
                        return sum;
                }
            }
        }
}
