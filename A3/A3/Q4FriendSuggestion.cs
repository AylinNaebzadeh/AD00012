using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using Priority_Queue;

namespace A3
{
    public class Graph
    {
        long nodeCount;
        public ArrayList[][] adj = new ArrayList[2][];
        public ArrayList[][] costs = new ArrayList[2][];
        public long[][] distance;
        public List<SimplePriorityQueue<long>> queue;
        public bool[] visited;
        
        public ArrayList intersect;
        const long maxDist = long.MaxValue / 10;

        public List<long> minAnswer = new List<long>();
        public Graph(long NodeCount)
        {
            nodeCount = NodeCount;
            visited = new bool[nodeCount];
            intersect = new ArrayList();
            distance = new long[][]{new long[nodeCount], new long[nodeCount]};
            for (long i = 0; i < nodeCount; i++)
            {
                distance[0][i] = distance[1][i] = maxDist;
            }
            queue = new List<SimplePriorityQueue<long>>();
            queue.Add(new SimplePriorityQueue<long>());
            queue.Add(new SimplePriorityQueue<long>());
        }

        public void clear()
        {
            foreach (long i in intersect)
            {
                distance[0][i] = distance[1][i] = maxDist;
                visited[i] = false;
            }
            intersect.Clear();
            queue[0].Clear();
            queue[1].Clear();
        }
        public void visit(long to_be_checked, long minDist)
        {
            if (distance[0][to_be_checked] > minDist)
            {
                distance[0][to_be_checked] = minDist;
                queue[0].Enqueue(to_be_checked, minDist);
                intersect.Add(to_be_checked);
            }
        }
        public void visitR(long to_be_checked, long minDist)
        {
            if (distance[1][to_be_checked] > minDist)
            {
                distance[1][to_be_checked] = minDist;
                queue[1].Enqueue(to_be_checked, minDist);
                intersect.Add(to_be_checked);
            }
        }
        public void Process(long to_be_checked, ArrayList[] adj, ArrayList[] cost)
        {
            for (int i = 0; i < adj[to_be_checked].Count; i++)
            {
                // if (!visited[(long)adj[to_be_checked][i]])
                    visit((long)adj[to_be_checked][i], distance[0][to_be_checked] + (long)cost[to_be_checked][i]);
            }
        }
        public void ProcessR(long to_be_checked, ArrayList[] adj, ArrayList[] cost)
        {
            for (int i = 0; i < adj[to_be_checked].Count; i++)
            {
                // if (!visited[(long)adj[to_be_checked][i]])
                    visitR((long)adj[to_be_checked][i], distance[1][to_be_checked] + (long)cost[to_be_checked][i]);
            }
        }

        public long shortestPath(long to_be_checked)
        {
            long dist = maxDist;
            foreach (long i in intersect)
            {
                if (distance[0][i] >= maxDist || distance[1][i] >= maxDist)
                {
                    continue;
                }
                if (distance[0][i] + distance[1][i] < dist)
                {
                    dist = distance[0][i] + distance[1][i];
                }
            }
            if (dist  >= maxDist || dist  <= -1)
            {
                return -1;
            }
            return dist;
        }
        public long BidirectionalDijkstra(long s, long t)
        {
            if (s == t)
            {
                return 0;
            }
            clear();
            visit(s, 0);
            visitR(t, 0);

            while (queue[0].Count != 0 && queue[1].Count != 0)
            {
                long to_be_checked = queue[0].Dequeue();
                Process(to_be_checked, adj[0], costs[0]);
                if (visited[to_be_checked])
                {
                    return shortestPath(to_be_checked);
                }
                visited[to_be_checked] = true;

                long to_be_checked_r = queue[1].Dequeue();
                ProcessR(to_be_checked_r, adj[1], costs[1]);
                if (visited[to_be_checked_r])
                {
                    return shortestPath(to_be_checked_r);
                }
                visited[to_be_checked_r] = true;
            }

            return -1;
        }
    }
    // public class Node : IComparable<Node> 
    // {
    //     public long nodeValue;
    //     public long newCost;
    //     public Node(long node, long cost)
    //     {
    //         nodeValue = node;
    //         newCost = cost;
    //     }
    //     public int CompareTo(Node other)
    //     {
    //         return newCost < other.newCost ? -1 : newCost > other.newCost ? 1 : 0;
    //     }
    // }
    public class Q4FriendSuggestion:Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { ExcludeTestCaseRangeInclusive(14, 50); }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long,long[][], long[]>)Solve);
        public static long[] Solve(long NodeCount, long EdgeCount, long[][] edges, long QueriesCount, long[][]Queries)
        {
            Graph graph = new Graph(NodeCount);
            for (long side = 0; side < 2; side++)
            {
                graph.adj[side] = new ArrayList[NodeCount];
                graph.costs[side] = new ArrayList[NodeCount];
                for (long i = 0; i < NodeCount; i++)
                {
                    graph.adj[side][i] = new ArrayList();
                    graph.costs[side][i] = new ArrayList();
                }
            }
            for (long j = 0; j < EdgeCount; j++)
            {
                long x = edges[j][0];
                long y = edges[j][1];
                long z = edges[j][2];
                graph.adj[0][x - 1].Add(y - 1);
                graph.costs[0][x - 1].Add(z);
                graph.adj[1][y - 1].Add(x - 1);
                graph.costs[1][y - 1].Add(z);
            }
            long[] result = new long[QueriesCount];
            for (long j = 0; j < QueriesCount; j++)
            {
                result[j] = graph.BidirectionalDijkstra(Queries[j][0] - 1, Queries[j][1] - 1);
            }
            return result;
        }

    }
}
