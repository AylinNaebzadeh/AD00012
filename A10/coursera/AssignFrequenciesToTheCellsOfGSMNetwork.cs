using System;
using System.Collections.Generic;

class Q1
{
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
}