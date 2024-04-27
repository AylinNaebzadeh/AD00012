using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{

    public class Q1BuildingRoads : Processor
    {
        public Q1BuildingRoads(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], double>)Solve);

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
            return System.Math.Round(ans, 6);
        }
    }
}
