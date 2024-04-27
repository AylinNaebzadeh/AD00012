using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;


namespace E1
{
    public class Edge
    {
        public long source;
        public long target;
        public long dist;
    }
    public class Q1SecondMST : Processor
    {
        public Q1SecondMST(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);


        public static List<long> to_be_checked = new List<long>();
        public static List<Edge> _edges = new List<Edge>();
        public static long[] parents = new long[1000];
        public static int edgeCount = 0;
        public static long Find(long node)
        {
            if (node == parents[(int)node])
            {
                return node;
            }
            return parents[(int)node] = Find(parents[(int)node]);
        }

        public static long MSTUnion(int i, long mst)
        {
            long source_id = Find(_edges[i].source);
            long target_id = Find(_edges[i].target);
            if (source_id != target_id)
            {
                parents[(int)source_id] = target_id;

                to_be_checked.Add(i);

                mst += _edges[i].dist;
            }
            return mst;
        }

        public static long _MSTUnion(int j, long _mst)
        {
            long source_id = Find(_edges[j].source);
            long target_id = Find(_edges[j].target);
            if (source_id != target_id)
            {
                parents[(int)source_id] = target_id;

                // to_be_checked.Add(j);

                _mst += _edges[j].dist;
                edgeCount++;
            }
            return _mst;
        }
        public static long Solve(long nodeCount, long[][] edges)
        {
            // long ans = 0;
            // initializing nodes
            for (long i = 1; i <= nodeCount; i++)
            {
                parents[i] = i;
            }
            // initializing edges
            for (int i = 0; i < edges.Length; i++)
            {
                _edges.Add(new Edge());
                _edges[i].source = edges[i][0];
                _edges[i].target = edges[i][1];
                _edges[i].dist = edges[i][2];
            }
            // sorting the edges
            _edges.Sort((x, y) => x.dist.CompareTo(y.dist));

            // finding best spanning tree

            long MST = 0;
            for (int i = 0; i < edges.Length; i++)
            {
                MST = MSTUnion(i, MST);
            }

            long _MST = int.MaxValue;
            MST = 0;
            for (long i = 0; i < to_be_checked.Count; i++)
            {
                for (int j = 1; j <= nodeCount; j++)
                {
                    parents[j] = j;
                }
                edgeCount = 0;
                for (int j = 0; j < edges.Length; j++)
                {
                    if (j == to_be_checked[(int)i])
                    {
                        continue;
                    }
                    MST = _MSTUnion(j, MST);
                }

                if (edgeCount != nodeCount - 1)
                {
                    MST = 0;
                    continue;
                }

                if (_MST > MST)
                {
                    _MST = MST;
                }

                MST = 0;

            }
            if (_MST != int.MaxValue)
                return _MST;
            else
            {
                return -1;
            }
        }

    }
}
