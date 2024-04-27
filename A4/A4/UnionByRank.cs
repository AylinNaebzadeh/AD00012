using System.Collections.Generic;

namespace A4
{
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
}