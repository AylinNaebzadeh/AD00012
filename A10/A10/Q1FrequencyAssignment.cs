using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Q1FrequencyAssignment : Processor
    {
        public Q1FrequencyAssignment(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);


        public String[] Solve(int V, int E, long[,] matrix)
        {
            List<string> clauses = new List<string>();
            exactlyOneColor(clauses, V);
            for (int i = 0; i < matrix.Length / 2; i++)
            {
                atMostOneColor(clauses, matrix[i, 0], matrix[i, 1]);
            }
            clauses.Insert(0, $"{3 * V} {4 * V + 3 * E}");
            return clauses.ToArray();

        }

        private static void atMostOneColor(List<string> clauses, long x, long y)
        {
            clauses.Add($"-{(x - 1) * 3 + 1} -{(y - 1) * 3 + 1} 0");
            clauses.Add($"-{(x - 1) * 3 + 2} -{(y - 1) * 3 + 2} 0");
            clauses.Add($"-{(x - 1) * 3 + 3} -{(y - 1) * 3 + 3} 0");
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
                clauses.Add($"{r} {g} {b} 0");
            }
        }
        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;
    }
}
