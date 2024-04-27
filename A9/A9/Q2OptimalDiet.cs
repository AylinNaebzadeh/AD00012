using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A9
{
    public class Q2OptimalDiet : Processor
    {
        public Q2OptimalDiet(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);
        

        public string Solve(int N, int M, double[,] matrix)
        { // N --> the number of restrictions on diet
          // M --> the number of available dishes/drinks

            // create a new matrix of equations
            double[,] newMatrix = new double[N + 1, N + M + 2];
            for (int i = 0; i < N + 1; i++)
            {
                newMatrix[i, i + M] = 1;
                for (int j = 0; j < M; j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
                newMatrix[i, newMatrix.GetLength(1) - 1] = matrix[i, matrix.GetLength(1) - 1];
            }

            for (int i = 0; i < M; i++)
            {
                newMatrix[newMatrix.GetLength(0) - 1, i] *= -1;
            }
            /*-----------------------------------------------------------------------------------*/
            Tuple<int, int> pivot = Tuple.Create(0, 0);
            findPivot(newMatrix, pivot);

            while (pivot.Item2 != -1)
            {
                gaussianElimination(newMatrix, pivot);
                findPivot(newMatrix, pivot);

                if (pivot.Item1 == -1)
                {
                    return "Infinity";
                }
            }

            return backSubstitution(newMatrix, matrix, N, M);
        }

        private string backSubstitution(double[,] newMatrix, double[,] matrix, int n, int m)
        {
            string result = "Bounded Solution\n";
            double[] res = new double[m];
            bool flag;
            for (int i = 0; i < m; i++)
            {
                flag = false;

                for (int j = 0; j < n; j++)
                {
                    if (newMatrix[j, i] == 1 && !flag)
                    {
                        res[i] = newMatrix[j, newMatrix.GetLength(1) - 1];
                        flag = true;
                    }
                    else if (newMatrix[j, i] != 0 && flag)
                    {
                        res[i] = 0;
                        break;
                    }
                }
            }

            double temp = 0;

            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                temp = 0;
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    temp += matrix[i, j] * res[j];
                }
                temp = Math.Round(temp * 2) / 2;
                if (temp > matrix[i, matrix.GetLength(1) - 1])
                    return "No Solution";
            }

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = Math.Round(res[i] * 2) / 2;
                result = result + res[i] + " ";
            }
            return result.TrimEnd(' ');
        }

        private static void findPivot (double[,] newMatrix, Tuple<int, int> pivot)
        {
            int column = -1, row = -1;
            double min = double.MaxValue;
            for (int i = 0; i < newMatrix.GetLength(1) - 1; i++)
            {
                if (newMatrix[newMatrix.GetLength(0) - 1, i] < 0 && newMatrix[newMatrix.GetLength(0) - 1, i] < min)
                {
                    min = newMatrix[newMatrix.GetLength(0) - 1, i];
                    column =  i;
                }
            }

            if (column == -1)
            {
                pivot = Tuple.Create(0, -1);
            }

            min = double.MaxValue;

            for (int i = 0; i < newMatrix.GetLength(0) - 1; i++)
            {
                var quotient = newMatrix[i, newMatrix.GetLength(1) - 1] / newMatrix[i, column];
                if (newMatrix[i, column] == 0 || quotient < 0)
                {
                    continue;
                }
                if (quotient < min)
                {
                    row = i;
                    min = quotient;
                }
            }

            if (row == -1)
            {
                pivot = Tuple.Create(-1, 0);
            }

            min = newMatrix[row, newMatrix.GetLength(1) - 1] / newMatrix[row, column];

            for (int i = 0; i < newMatrix.GetLength(1); i++)
            {
                if (i == column)
                {
                    continue;
                }
                newMatrix[row, i] /= newMatrix[row, column];
            }

            newMatrix[row, column] = 1;
            pivot = Tuple.Create(row, column);
        }

        private static void gaussianElimination(double[,] matrix, Tuple<int, int> pivot)
        {
            double quotient = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i == pivot.Item1)
                {
                    continue;
                }

                quotient = matrix[i, pivot.Item2] / matrix[pivot.Item1, pivot.Item2] * -1;

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] += (matrix[pivot.Item1, j] * quotient);
                }
            }
        }
    }
}
