using System.Collections.Generic;
using System.Linq;

class Q2
{
        private static List<int> findingEulerianCycle (List<int>[] adj)
        {

            if (adj.Length == 0)
            {
                return new List<int>();
            }
            Dictionary<int, int> nodeCount = new Dictionary<int, int>();
            for (int i = 0; i < adj.Length; i++)
            {
                nodeCount[i] = adj[i].Count;
            }

            List<int> path = new List<int>();
            List<int> cycle = new List<int>();
            path.Add(0);
            int deadend = 0;
            while (path.Count > 0)
            {
                if (nodeCount[deadend] != 0)
                {
                    path.Add(deadend);
                    int next = adj[deadend].Last();
                    nodeCount[deadend] -= 1;
                    adj[deadend].RemoveAt(adj[deadend].Count - 1);
                    deadend = next;
                }
                else
                {
                    cycle.Add(deadend);
                    deadend = path.Last();
                    path.RemoveAt(path.Count - 1);
                }
            }
            return cycle;
        }

        private static bool checkForDegrees (Dictionary<int, int> inEdges, Dictionary<int, int> outEdges)
        {
            foreach (var key in inEdges.Keys)
            {
                if (inEdges[key] != inEdges[key])
                {
                    return false;
                }
            }
            return true;
        }
}