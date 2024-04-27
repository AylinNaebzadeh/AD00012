using System;
using System.Collections;
using System.Collections.Generic;
using Priority_Queue;

namespace A44
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
        const double MAX_DIST = double.PositiveInfinity / 4;
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
            if (source == target)
            {
                return 0;
            }
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
    public class Node
    {
        public long rank;
        public long parent;
        public long x;
        public long y;
    }
    public class Edge
    {
        public long source;
        public long target;
        public double dist;
    }
    public class DisjointSet
    {
        public long size;
        public List<Node> DS;
        public DisjointSet(long size)
        {
            this.size = size;
            DS = new List<Node>(new Node[size]);
            // Make sets
            for (int i = 0; i < size; i++)
            {
                DS[i] =  new Node();
            }
        }
        public long Find(long node)
        {
            while (node != DS[(int)node].parent)
            {
                node = DS[(int)node].parent;
            }
            return node;
        }
        public void Union(long i, long j)
        {
            long i_id = Find(i);
            long j_id = Find(j);

            if (i_id == j_id)
            {
                return;
            }

            if (DS[(int)i_id].rank > DS[(int)j_id].rank)
            {
                DS[(int)j_id].parent = i_id;
                DS[(int)i_id].rank += DS[(int)j_id].rank;
            }
            else
            {
                DS[(int)i_id].parent = j_id;
                DS[(int)j_id].rank += DS[(int)i_id].rank;
            }
        }
    }
    class Program
    {
        #region Q1
        public static double calculateDistance(long[] p1, long[] p2)
        {
            return Math.Sqrt(Math.Pow(p1[0] - p2[0], 2) + Math.Pow(p1[1] - p2[1], 2));
        }
        public static double Solve(long pointCount, long[][] points)
        {
            double ans = 0.0;
            DisjointSet disjointSet = new DisjointSet(pointCount);
            /*  The first step is to set the value of all the nodes */
            Node node;
            for (long i = 0; i < pointCount; i++)
            {
                node = new Node();
                node.parent = i;
                node.x = points[i][0];
                node.y = points[i][1];
                disjointSet.DS[(int)i] = node;
            }
            /* The second step is to construct an array
                for all the edges that can be built
                using the above nodes (from every node to the others)
            */
            List<Edge> edges = new List<Edge>();
            for (long i = 0; i < pointCount; i++)
            {
                Edge edge;
                for (long j = i + 1; j < pointCount; j++)
                {
                    edge = new Edge();
                    edge.source = i;
                    edge.target = j;
                    edge.dist = calculateDistance(points[i], points[j]);
                    edges.Add(edge);
                }
            }
            /* The next step is to sort the edges in non-decreasing order
                base on their weights
            */
            edges.Sort((x, y) => x.dist.CompareTo(y.dist));
            /* The final step is to call Union on nodes in every edge*/
            foreach (var edge in edges)
            {
                if (disjointSet.Find(edge.target) != disjointSet.Find(edge.source))
                {
                    ans += edge.dist;
                    disjointSet.Union(edge.target, edge.source);
                }
            }
            return ans;
        }
        #endregion
        #region Q2
        public static double Solve(long pointCount, long[][] points, long clusterCount)
        {
            double ans = 0.0;
            DisjointSet disjointSet = new DisjointSet(pointCount);

            Node node;
            for (long i = 0; i < pointCount; i++)
            {
                node = new Node();
                node.parent = i;
                node.x = points[i][0];
                node.y = points[i][1];
                disjointSet.DS[(int)i] = node;
            }

            List<Edge> edges = new List<Edge>();
            for (long i = 0; i < pointCount; i++)
            {
                Edge edge;
                for (long j = i + 1; j < pointCount; j++)
                {
                    edge = new Edge();
                    edge.source = i;
                    edge.target = j;
                    edge.dist = calculateDistance(points[i], points[j]);
                    edges.Add(edge);
                }
            }

            edges.Sort((x, y) => x.dist.CompareTo(y.dist));

            foreach (var edge in edges)
            {
                if (disjointSet.Find(edge.target) != disjointSet.Find(edge.source))
                {
                    ans += 1;
                    disjointSet.Union(edge.target, edge.source);
                }
                if (ans > pointCount - clusterCount)
                {
                    return edge.dist;
                }
            }
            return 0;
        }
        #endregion
        #region Q3
        public static double[] Solve(long nodeCount,
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
        #endregion
        static void Main(string[] args)
        {
            // var first_line = Convert.ToInt64(Console.ReadLine());
            // long[][] points = new long[first_line][];
            // for (long i = 0; i < first_line; i++)
            // {
            //     points[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // }
            // var res = Solve(first_line, points);
            // System.Console.WriteLine(res);
            // *****************************************************
            // var numberOfPoints = Convert.ToInt64(Console.ReadLine());
            // long[][] points = new long[numberOfPoints][];
            // for (long i = 0; i < numberOfPoints; i++)
            // {
            //     points[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            // }
            // var clusterCount = Convert.ToInt64(Console.ReadLine());
            // var res = Solve(numberOfPoints, points, clusterCount);
            // System.Console.WriteLine(res);
            // ********************************************************
            var first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            double[][] points = new double[first_line[0]][];
            for (long i = 0; i < first_line[0]; i++)
            {
                points[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToDouble);
            }
            double[][] edges = new double[first_line[1]][];
            for (long i = 0; i < first_line[1]; i++)
            {
                edges[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToDouble);
            }
            long queriesCount = Convert.ToInt64(Console.ReadLine());
            long[][] queries = new long[queriesCount][];
            for (long i = 0; i < queriesCount; i++)
            {
                queries[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' ') , Convert.ToInt64);
            }
            var result = Solve(first_line[0], first_line[1], points, edges, queriesCount, queries);
        }
    }
}
