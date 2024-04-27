using System;
using System.Collections.Generic;
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
class BuildingRoads
{
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
}