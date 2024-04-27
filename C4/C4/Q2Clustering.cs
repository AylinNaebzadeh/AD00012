using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using static C4.Q1BuildingRoads;

namespace C4
{
    public class Q2Clustering : Processor
    {
        public Q2Clustering(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, double>)Solve);

        public static double calculateDistance(long[] p1, long[] p2)
        {
            return Math.Sqrt(Math.Pow(p1[0] - p2[0], 2) + Math.Pow(p1[1] - p2[1], 2));
        }
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
                    return System.Math.Round(edge.dist, 6);
                }
            }
            return 0;
        }
    }
}