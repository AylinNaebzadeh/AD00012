using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using Priority_Queue;

namespace A4
{
    public class Graph
    {
        public long nodeCount;
        public double[] x;
        public double[] y;
        public ArrayList[] adj;
        public ArrayList[] costs;
        public double[] distance;
        SimplePriorityQueue<long> queue;
        public bool[] visited;
        public List<long> intersect;
        public Dictionary<long, double> nodeDist;
        const long MAX_DIST = long.MaxValue / 10;
        public Graph(long _nodeCount)
        {
            nodeCount = _nodeCount;
            visited = new bool[_nodeCount];
            x = y =  new double[_nodeCount];
            intersect = new List<long>();
            distance = new double[_nodeCount];
            nodeDist = new Dictionary<long, double>();
            for (int i = 0; i < _nodeCount; i++)
            {
                distance[i] = MAX_DIST;
            }
            queue = new SimplePriorityQueue<long>();
        }

        void clear()
        {
            foreach (long v in intersect)
            {
                distance[v] = MAX_DIST;
                visited[v] = false;
            }
            intersect.Clear();
            queue.Clear();
            nodeDist.Clear();
        }

        void relax(long vertex, double dist, double potentialDist)
        {
            if (distance[vertex] > dist)
            {
                distance[vertex] = dist;
                queue.Enqueue(vertex, (float)(distance[vertex] + potentialDist));
                intersect.Add(vertex);
            }
        }
        double calculatePotentialFunction(long source, long target)
        {
            if (!nodeDist.ContainsKey(source))
            {
                nodeDist.Add(source, Math.Sqrt((x[source] - x[target]) * (x[source] - x[target]) + (y[source] - y[target]) * (y[source] - y[target])));
            }
            return nodeDist[source];
        }
        void process(long source, long target, ArrayList[] adj, ArrayList[] cost)
        {
            for (int i = 0; i < adj[source].Count; i++)
            {
                var child = (double)adj[source][i];
                if (!visited[(long)child])
                {
                    this.relax((long)child, distance[source] + (double)costs[source][i], calculatePotentialFunction((long)child, target));
                }
            }
        }
        public double A_star(long source, long target)
        {
            this.clear();
            relax(source, 0, calculatePotentialFunction(source, target));
            while (queue.Count != 0)
            {
                long to_be_checked = queue.Dequeue();
                if (to_be_checked == target)
                {
                    if (distance[target] != MAX_DIST)
                    {
                        return distance[target];
                    }
                    return -1;
                }
                if (!visited[to_be_checked])
                {
                    process(to_be_checked, target, adj, costs);
                    this.visited[to_be_checked] = true;
                }
            }
            return -1;
        }
    }
    public class Q3ComputeDistance : Processor
    {
        public Q3ComputeDistance(string testDataName) : base(testDataName) { }

        public static readonly char[] IgnoreChars = new char[] { '\n', '\r', ' ' };
        public static readonly char[] NewLineChars = new char[] { '\n', '\r' };
        private static double[][] ReadTree(IEnumerable<string> lines)
        {
            return lines.Select(line => 
                line.Split(IgnoreChars, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(n => double.Parse(n)).ToArray()
                            ).ToArray();
        }
        public override string Process(string inStr)
        {
            return Process(inStr, (Func<long, long, double[][], double[][], long,
                                    long[][], double[]>)Solve);
        }
        public static string Process(string inStr, Func<long, long, double[][]
                                ,double[][], long, long[][], double[]> processor)
        {
            var lines = inStr.Split(NewLineChars, StringSplitOptions.RemoveEmptyEntries);
            long[] count = lines.First().Split(IgnoreChars,
                                            StringSplitOptions.RemoveEmptyEntries)
                                        .Select(n => long.Parse(n))
                                        .ToArray();
            double[][] points = ReadTree(lines.Skip(1).Take((int)count[0]));
            double[][] edges = ReadTree(lines.Skip(1 + (int)count[0]).Take((int)count[1]));
            long queryCount = long.Parse(lines.Skip(1 + (int)count[0] + (int)count[1]) 
                                        .Take(1).FirstOrDefault());
            long[][] queries = ReadTree(lines.Skip(2 + (int)count[0] + (int)count[1]))
                                        .Select(x => x.Select(z => (long)z).ToArray())
                                        .ToArray();

            return string.Join("\n", processor(count[0], count[1], points, edges,
                                queryCount, queries));
        }
        public double[] Solve(long nodeCount,
                            long edgeCount,
                            double[][] points,
                            double[][] edges,
                            long queriesCount,
                            long[][] queries)
        {
            List<double> res = new List<double>();
            Graph graph = new Graph(nodeCount);
            graph.adj = new ArrayList[nodeCount];
            graph.costs = new ArrayList[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                graph.adj[i] = new ArrayList();
                graph.costs[i] = new ArrayList();
            }
            
            for (long i = 0; i < nodeCount; i++)
            {
                graph.x[i] = points[i][0];
                graph.y[i] = points[i][1];
            }
            for (long i = 0; i < edgeCount; i++)
            {
                double u = edges[i][0];
                double v = edges[i][1];
                double l = edges[i][2];
                graph.adj[(int)u - 1].Add(v - 1);
                graph.costs[(int)u - 1].Add(l);

            }
            for (long i = 0; i < queriesCount; i++)
            {
                res.Add(graph.A_star(queries[i][0] -= 1, queries[i][1] -= 1));
            }
            return res.ToArray();
        }
    }
}