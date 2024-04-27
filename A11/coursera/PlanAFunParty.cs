class Q2
{
        private static void DFS(long x, bool[] visited, List<long>[] adj, long[] toBe, long[] notToBe, long[] Val)
        {
            visited[x] = true;
            for (int i = 0; i < adj[x].Count; i++)
            {
                long child = adj[x][i];
                if (!visited[child])
                {
                    DFS(child, visited, adj, toBe, notToBe, Val);
                    toBe[x] += notToBe[child];
                    notToBe[x] += Math.Max(toBe[child], notToBe[child]);
                }
            }
            toBe[x] += Val[x];
        }
        public static long funParty (long n, long[] funFactors, long[][] Hierarchy)
        {
            List<long>[] adj = new List<long>[n];
            for (int i = 0; i < n; i++)
            {
                adj[i] = new List<long>();
            }
            long[] toBe = new long[n];
            long[] notToBe = new long[n];
            bool[] visited = new bool[n];
            long[] Val = funFactors;
            for (int i = 0; i < n - 1; i++)
            {
                long u = Hierarchy[i][0] - 1;
                long v = Hierarchy[i][1] - 1;
                adj[u].Add(v);
                adj[v].Add(u);
            }
            long res = 0;
            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    DFS(i, visited, adj, toBe, notToBe, Val);
                    res += Math.Max(toBe[i], notToBe[i]);
                }
            }
            return res;
        }
}