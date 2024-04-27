using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
/*
* construct the implication graph G
* find SCC’s of G
* for all variables x:
* if x and x lie in the same SCC of G:
* return “unsatisfiable”
* find a topological ordering of SCC’s
* for all SCC’s C in reverse order:
* if literals of C are not assigned yet:
* set all of them to 1
* set their negations to 0
* return the satisfying assignment
*/


namespace A11
{
    class Program
    {
        #region Q1
        // private static void DFS(long v, bool[] visited, List<List<long>> adj, bool[] inOrder, List<long> topoOrder)
        // {
        //     Stack<long> st = new Stack<long>();
        //     st.Push(v);
        //     while(st.Count > 0) {
        //         long current = st.Peek();
        //         if (!visited[current]) {
        //             visited[current] = true;
        //             for (int i = 0; i < adj[(int)current].Count; ++i) {
        //                 long child = adj[(int)current][i];
        //                 if (!visited[child]) {
        //                     st.Push(child);
        //                 }
        //             }
        //         } else if (!inOrder[current]) {
        //             inOrder[current] = true;
        //             topoOrder.Add(current);
        //             st.Pop();
        //         } else {
        //             st.Pop();
        //         }
        //     }
        // }

        // private static void ReverseDFS(long v, bool[] Rvisited, List<List<long>> toBeChecked, long holder, List<List<long>> Radj, List<long> fill)
        // {
        //     Stack<long> st = new Stack<long>();
        //     st.Push(v);
        //     while(st.Count > 0) {
        //         long current = st.Pop();
        //         if (!Rvisited[current]) {
        //             Rvisited[current] = true;
        //             toBeChecked[(int)holder].Add(current);
        //             fill[(int)current] = (int)holder;
        //             for (int i = 0; i < Radj[(int)current].Count; ++i) {
        //                 long child = Radj[(int)current][i];
        //                 if (!Rvisited[child]) {
        //                     st.Push(child);
        //                 }
        //             }
        //         }
        //     }
        // }

        // private static long getIndex(long v)
        // {
        //     return v > 0 ? 2 * (v - 1) : 2 * (-v - 1) + 1;
        // }
        // private static long getComplement(long v)
        // {
        //     return v % 2 == 0 ? v + 1 : v - 1;
        // }
        // private static long getVariable(long x)
        // {
        //     return x % 2 == 0 ? (x / 2) + 1 : -(((x - 1) / 2) + 1);
        // }

        // public static Tuple<bool, long[]> integratedCircuitDesign(int V, int C, int[][] cnf)
        // {
        //     int n = 2 * V;
        //     List<List<long>> adj = new List<List<long>>();
        //     List<List<long>> radj = new List<List<long>>();
        //     bool[] inOrder = new bool[n];
        //     List<long> fill = new List<long>();
        //     for (int i = 0; i < n; ++i) {
        //         fill.Add(-1);
        //         adj.Add(new List<long>());
        //         radj.Add(new List<long>());
        //     }
        //     bool[] visited = new bool[n];
        //     bool[] Rvisitetd = new bool[n];
        //     List<long> topoOrder = new List<long>();
        //     List<List<long>> toBeChecked = new List<List<long>>();
        //     long[] ans = new long[V];
        //     for (int i = 0; i < C; ++i) {
        //         adj[(int)getIndex(-cnf[i][0])].Add(getIndex(cnf[i][1]));
        //         adj[(int)getIndex(-cnf[i][1])].Add(getIndex(cnf[i][0]));

        //         radj[(int)getIndex(cnf[i][1])].Add(getIndex(-cnf[i][0]));
        //         radj[(int)getIndex(cnf[i][0])].Add(getIndex(-cnf[i][1]));
        //     }

        //     for (long i = 0; i < n; ++i) {
        //         if (!visited[i]) {
        //             DFS(i, visited, adj, inOrder, topoOrder);
        //         }
        //     }
        //     topoOrder.Reverse();
        //     long holder = 0;
        //     for (int i = 0; i < topoOrder.Count; ++i) {
        //         if (!Rvisitetd[topoOrder[i]]) {
        //             toBeChecked.Add(new List<long>());
        //             ReverseDFS(topoOrder[i], Rvisitetd, toBeChecked, holder, radj, fill);
        //             holder++;
        //         }
        //     }
        //     for (int i = 0; i < n; ++i) {
        //         if (fill[i] == fill[(int)getComplement(i)]) {
        //             return new Tuple<bool, long[]>(false, new long[0]);
        //         }
        //     }
        //     for (long i = fill.Count - 1; i >= 0; --i) {
        //         for (long j = 0; j < toBeChecked[(int)i].Count; ++j) {
        //             long u = toBeChecked[(int)i][(int)j];
        //             if (ans[Math.Abs(getVariable(u)) - 1] == 0)
        //                 ans[Math.Abs(getVariable(u)) - 1] = getVariable(u);
        //         }
        //     }
        //     return new Tuple<bool, long[]>(true, ans);
        // }
        #endregion

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
        static void Main(string[] args)
        {   // V C
            #region Q1Driver
            // int[] first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToInt32);
            // int[][] info = new int[first_line[1]][];
            // for (int i = 0; i < first_line[1]; i++)
            // {
            //     info[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToInt32);
            // }
            // var res = integratedCircuitDesign(first_line[0], first_line[1], info);
            // if (res.Item1 == true) 
            // {
            //     Console.WriteLine("SATISFIABLE");
            //     StringBuilder ans = new StringBuilder();
            //     System.Console.WriteLine(string.Join(" ", res.Item2));
            // } 
            // else 
            // {
            //     Console.WriteLine("UNSATISFIABLE");
            // }
            #endregion

            #region Q2Driver
            long n = long.Parse(Console.ReadLine().Split()[0]);
            long[] funArr = new long[n];
            string[] temp = Console.ReadLine().Split();
            for (int i = 0; i < n; ++i) {
                funArr[i] = long.Parse(temp[i]);
            }
            long[][] matrix = new long[n - 1][];
            for (int i = 0; i < n - 1; ++i) {
                string[] tmp = Console.ReadLine().Split();
                matrix[i] = new long[2];
                for (int j = 0; j < 2; ++j) {
                    matrix[i][j] = long.Parse(tmp[j]);
                }
            }
            Console.WriteLine(funParty(n, funArr, matrix));
            #endregion
        }
    }
}