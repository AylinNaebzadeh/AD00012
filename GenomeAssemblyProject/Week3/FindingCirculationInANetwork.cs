using System;
using System.Collections.Generic;

class Q1
{
        private static bool BFS(int source, int target, int[,] graph, int[] parents)
        {
            int size = graph.GetLength(0);
            bool[] visited = new bool[size];
            Queue<int> bfs = new Queue<int>();
            bfs.Enqueue(source);
            visited[source] = true;

            while (bfs.Count != 0)
            {
                int to_be_checked = bfs.Dequeue();
                if (target == to_be_checked)
                {
                    break;
                }
                for (int i = 0; i < size; i++)
                {
                    if (!visited[i] && graph[to_be_checked, i] > 0)
                    {
                        visited[i] = true;
                        parents[i] = to_be_checked;
                        bfs.Enqueue(i);
                    }
                }
            }
            return visited[target];
        }

        private static int calculateMaxFlow(int[, ] graph, int source, int target)
        {
            int size = graph.GetLength(0);
            int[] parent = new int[size];
            for (int i = 0; i < size; i++)
            {
                parent[i] = -1;
            }
            int res = 0;

            while (BFS(source, target, graph, parent))
            {
                int currentFlow = int.MaxValue;
                int currentNode = target;
                while (currentNode != source)
                {
                    currentFlow = Math.Min(currentFlow, graph[parent[currentNode], currentNode]);
                    currentNode = parent[currentNode];
                }

                currentNode = target;
                while (currentNode != source)
                {
                    graph[parent[currentNode], currentNode] -= currentFlow;
                    graph[currentNode, parent[currentNode]] += currentFlow;
                    currentNode = parent[currentNode];
                }

                res += currentFlow;
            }
            return res;
        }
}