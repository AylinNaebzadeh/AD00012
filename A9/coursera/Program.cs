using System;
using System.Collections.Generic;
using System.Text;

namespace A99
{
    class Program
    {
        #region Q1
        public static double[] Q1Solve(long MATRIX_SIZE, double[][] matrix)
        {
            double[] result = new double[MATRIX_SIZE];
            for (long i = 0; i < MATRIX_SIZE; i++)
            {
                long index = i;
                double max = (int)matrix[index][i];

                for (long j = i + 1; j < MATRIX_SIZE; j++)
                {
                    if (max == 0 && matrix[j][i] != 0)
                    {
                        swapLines(matrix, MATRIX_SIZE, i, j);
                    }
                    if (Math.Abs(matrix[j][i]) > max)
                    {
                        index = j;
                        max = (int)matrix[j][i];
                    }
                }

                if (max != 0)
                {
                    swapLines(matrix, MATRIX_SIZE, index, i);
                }
                /* Rescaling */
                for (long j = i + 1; j < MATRIX_SIZE; j++)
                {
                    double ratio = matrix[j][i] / matrix[i][i];
                    for (long k = i + 1; k < MATRIX_SIZE; k++)
                    {
                        matrix[j][k] -= ratio * matrix[i][k]; 
                    }
                    matrix[j][i] = 0;
                    matrix[j][MATRIX_SIZE] -= matrix[i][MATRIX_SIZE] * ratio;
                }

            }
            BackSubstitution(matrix, result, MATRIX_SIZE);
            Round(result);
            return result;
        }

        private static void Round(double[] result)
        {
            for (long i = 0; i < result.Length; i++)
            {
                int current = (int)result[i];
                double fraction = result[i] - current;
                if (fraction != 0)
                {
                    if (Math.Abs(fraction) < 0.25)
                    {
                        result[i] = current;
                    }
                    else if (Math.Abs(fraction) > 0.25 && Math.Abs(fraction) < 0.75)
                    {
                        if (result[i] > 0)
                        {
                            result[i] = current + 0.5;
                        }
                        else
                        {
                            result[i] = current - 0.5;
                        }
                    }
                    else if (Math.Abs(fraction) > 0.75)
                    {
                        if (result[i] > 0)
                        {
                            result[i] = current + 1;
                        }
                        else
                        {
                            result[i] = current - 1;
                        }
                    }
                }
            }
        }

        private static void BackSubstitution(double[][] matrix, double[] answer, long rowCount)
        {
            for (long i = rowCount - 1; i >= 0; i--)
            {
                answer[i] = matrix[i][rowCount];
                for (long j = i + 1; j < rowCount; j++)
                {
                    answer[i] -= matrix[i][j] * answer[j];
                }

                answer[i] = answer[i] / matrix[i][i];
            }
        }

        private static void swapLines(double[][] matrix, long MATRIX_SIZE, long row_1, long row_2)
        {
            for (long i = 0; i <= MATRIX_SIZE; i++)
            {
                double tmp = matrix[row_1][i];
                matrix[row_1][i] = matrix[row_2][i];
                matrix[row_2][i] = tmp;
            }
        }

        #endregion Q1
        #region Q2
        
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

        #endregion
        static void Main(string[] args)
        {
            long first_line = Convert.ToInt64(Console.ReadLine());
            double[][] info = new double[first_line][];
            for (long i = 0; i < first_line; i++)
            {
                info[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToDouble);
            }

            var result = Q1Solve(first_line, info);
            System.Console.WriteLine(string.Join(' ', result));
        }
    }
}
