using System;
using System.Collections.Generic;
using System.Text;

namespace A100
{
    class Program
    {
        #region Q1
        public static String[] Solve(int V, int E, long[][] matrix)
        {
            List<string> clauses = new List<string>();
            exactlyOneColor(clauses, V);
            for (int i = 0; i < matrix.Length / 2; i++)
            {
                atMostOneColor(clauses, matrix[i][0], matrix[i][1]);
            }
            clauses.Insert(0, (3 * V).ToString() + " " + (4 * V + 3 * E).ToString());
            return clauses.ToArray();

        }

        private static void atMostOneColor(List<string> clauses, long x, long y)
        {
            clauses.Add("-"+ ((x - 1) * 3 + 1).ToString() + " " + "-" + ((y - 1) * 3 + 1).ToString() +" " + (0).ToString());
            clauses.Add("-"+ ((x - 1) * 3 + 2).ToString() + " " + "-" + ((y - 1) * 3 + 2).ToString() +" " + (0).ToString());
            clauses.Add("-"+ ((x - 1) * 3 + 3).ToString() + " " + "-" + ((y - 1) * 3 + 3).ToString() +" " + (0).ToString());
        }

        private static void exactlyOneColor(List<string> clauses, int V)
        {
            int r = 0;
            int g = 0;
            int b = 0;
            for (int i = 0; i < V; i++)
            {
                r = i * 3 + 1;
                g = i * 3 + 2;
                b = i * 3 + 3;
                clauses.Add((r).ToString() + " " + (g).ToString() + " " + (b).ToString() +  (0).ToString());
            }
        }
        #endregion Q1
        #region Q2
        public static string[] Q2Solve(int V, int E, long[][] matrix)
        {
            int variableCount = V * V;
            List<string> result = new List<string>();

            result.Add("first line");

            string temp = "";

            // 1v2v3v...vV satri
            for (int i = 0; i < V; i++)
            {
                temp = "";
                for (int j = 0; j < V; j++)
                {
                    temp += $"{i * V + j + 1} ";
                }
                temp += "0";
                result.Add(temp);
            }

            // satri 2 taii
            for (int i = 0; i < V; i++)
            {
                for (int j = i * V + 1; j <= (i + 1) * V; j++)
                {
                    for (int k = j + 1; k <= (i + 1) * V; k++)
                    {
                        temp = $"-{j} -{k} 0";
                        result.Add(temp);
                    }
                }
            }

            // sotooni
            for (int i = 0; i < V; i++)
            {
                temp = "";
                for (int j = 0; j < V; j++)
                {
                    temp += $"{j * V + 1 + i} ";
                }
                temp += "0";
                result.Add(temp);
            }

            // sotooni 2 taii
            for (int i = 0; i < V; i++)
            {
                for (int j = i + 1; j < V * V; j += V)
                {
                    for (int k = j + V; k <= V * V; k += V)
                    {
                        temp = $"-{j} -{k} 0";
                        result.Add(temp);
                    }
                }
            }

            // build reverse adjacency graph
            int[,] revAdjMatrix = new int[V + 1, V + 1];
            for (int i = 1; i <= V; i++)
            {
                for (int j = 1; j <= V; j++)
                {
                    revAdjMatrix[i, j] = 1;
                }
            }

            for (int i = 0; i < E; i++)
            {
                revAdjMatrix[matrix[i][0], matrix[i][1]] = 0;
                revAdjMatrix[matrix[i][1], matrix[i][0]] = 0;
            }

            for (int i = 0; i < V + 1; i++)
            {
                revAdjMatrix[i, i] = 0;
            }

            // check adjacency
            for (int i = 1; i <= V; i++)
            {
                for (int j = 1; j <= V; j++)
                {
                    if (revAdjMatrix[i, j] == 1)
                    {
                        temp = "";
                        for (int k = 1; k < V; k++)
                        {
                            temp = $"-{V * i - k} -{V * j - k + 1} 0";
                            result.Add(temp);
                            temp = $"-{V * i - k + 1} -{V * j - k} 0";
                            result.Add(temp);
                        }
                    }
                }
            }

            result[0] = $"{result.Count - 1} {V * V}";

            return result.ToArray();
        }
        #endregion Q2
        static void Main(string[] args)
        {
            int[] first_line = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToInt32);
            long[][] matrix = new long[first_line[0]][];

            for (int i = 0; i < first_line[0]; i++)
            {
                matrix[i] = new long[2];
                matrix[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToInt64);
            }

            var res = Q2Solve(first_line[0], first_line[1], matrix);
            foreach (string s in res)
            {
                System.Console.WriteLine(s);
            }
        }
    }
}
