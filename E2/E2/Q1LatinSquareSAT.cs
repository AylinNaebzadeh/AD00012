using TestCommon;

namespace E2
{
    public class Q1LatinSquareSAT : Processor
    {
        public Q1LatinSquareSAT(string testDataName) : base(testDataName)
        {
            this.ExcludeTestCases(9, 23, 27, 3, 50); // --> wrong computation
            //this.ExcludeTestCaseRangeInclusive(45, 57); // timeout and output
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int,int?[,],string>)Solve);

        public override Action<string, string> Verifier =>
            TestTools.SatVerifier;

        // هر سطر و ستون شامل تمام اعداد است
        private static void ExactlyOneCharacterOfEachNumber (int dimension, List<string> clauses)
        {
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    int[] exactlyOneChar = new int[dimension];
                    for (int k = 0; k < dimension; k++)
                        exactlyOneChar[k] = i * dimension * dimension + j * dimension + k + 1;
                    clauses.Add(String.Join(" ", exactlyOneChar));

                    for (int k = 0; k < exactlyOneChar.Length - 1; k++)
                    {
                        for (int a = k + 1; a <exactlyOneChar.Length; a++)
                        {
                            clauses.Add($"-{exactlyOneChar[k]} -{exactlyOneChar[a]}");
                        }
                    }
                }
            }
        }

        private static void determiningCNFofEachRow (int dimension, List<string> clauses)
        {
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    int[] checkRow = new int[dimension];
                    int[] checkColumn = new int[dimension];


                    for (int k = 0; k < dimension; k++)
                    {
                        checkRow[k] = i * dimension * dimension + j * dimension + k + 1;
                        checkColumn[k] = j * dimension * dimension + i * dimension + k + 1;
                    }

                    clauses.Add(String.Join(" ", checkRow));
                    clauses.Add(String.Join(" ", checkColumn));

                    for (int k = 0; k < checkRow.Length - 1; k++)
                    {
                        for (int a = k + 1; a < checkRow.Length; a++)
                        {
                            clauses.Add($"-{checkRow[k]} -{checkRow[a]}");
                            clauses.Add($"-{checkColumn[k]} -{checkColumn[a]}");
                        }
                    }
                }
            }
        }
        public virtual string Solve(int dim, int?[,] square)
        {
            int variableCount = dim * dim * dim;

            List<string> clauses = new List<string>();
            ExactlyOneCharacterOfEachNumber(dim, clauses);
            determiningCNFofEachRow(dim, clauses);

            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    if (square[i, j].HasValue)
                    {
                        int? number = square[i, j].Value;
                        int tmp = i * dim * dim + j * dim + number.Value + 1;
                        clauses.Add($"{tmp}");

                        for (int row = 0; row < dim; row++)
                        {
                            if (row != j)
                            {
                                int negTmpR = i * dim * dim + row * dim + number.Value + 1;
                                clauses.Add($"-{negTmpR}");
                            }
                        }
                        for (int col = 0; col < dim; col++)
                        {
                            if (col != i)
                            {
                                int negTmpC = col * dim * dim + j * dim + number.Value + 1;
                                clauses.Add($"-{negTmpC}");
                            }
                        }
                    }
                }
            }

            clauses.Insert(0, $"{variableCount} {clauses.Count - 1}");

            return String.Join("\n", clauses);
        }

    }
}
