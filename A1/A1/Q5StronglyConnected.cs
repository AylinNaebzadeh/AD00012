using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Node
    {
        public List<long> adj = new List<long>();
        public List<long> reversed_adj = new List<long>();
    }
    public class Q5StronglyConnected: Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public static void DFS_Adj(long to_be_explored, bool[] visited, Stack<long> stack, List<Node> graph)
        {
            visited[to_be_explored] = true;
            for (long i = 0; i < graph[(int)to_be_explored].adj.Count; i++)
            {
                if (!visited[graph[(int)to_be_explored].adj[(int)i]])
                {
                    DFS_Adj(graph[(int)to_be_explored].adj[(int)i], visited, stack, graph);
                }
            }
            stack.Push(to_be_explored);
        }
        public static void DFS_Rev_Adj(long to_be_explored, bool[] visited, List<Node> graph)
        {
            visited[to_be_explored] = true;
            for (long i = 0; i < graph[(int)to_be_explored].reversed_adj.Count; i++)
            {
                if (!visited[graph[(int)to_be_explored].reversed_adj[(int)i]])
                {
                    DFS_Rev_Adj(graph[(int)to_be_explored].reversed_adj[(int)i], visited, graph);
                }
            }
        }
        public static long Solve(long nodeCount, long[][] edges)
        {
            bool[] visited = new bool[nodeCount];
            long number_of_components = 0;
            Stack<long> stack = new Stack<long>();
            List<Node> graph = new List<Node>();
            for (long i = 0; i < nodeCount; i++)
            {
                graph.Add(new Node());
            }
            for (long i = 0; i < edges.Length; i++)
            {
                long source = edges[i][0];
                long dest = edges[i][1];
                graph[(int)source - 1].adj.Add(dest - 1);
                graph[(int)dest - 1].reversed_adj.Add(source - 1);
            }
            for (long i = 0; i < nodeCount; i++)
            {
                if (!visited[i])
                {
                    DFS_Adj(i, visited, stack, graph);
                }
            }
            for (long i = 0; i < nodeCount; i++)
            {
                visited[i] = false;
            }
            while (stack.Count != 0)
            {
                long vertex = stack.Pop();
                if (!visited[vertex])
                {
                    DFS_Rev_Adj(vertex, visited, graph);
                    number_of_components++;
                }
            }
            return number_of_components;
        }
    }
}
