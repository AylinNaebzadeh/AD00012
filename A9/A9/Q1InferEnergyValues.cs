using System;
using TestCommon;

namespace A9
{
    public class Q1InferEnergyValues : Processor
    {
        public Q1InferEnergyValues(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, double[,], double[]>)Solve);
        
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
                if (result[i] == -0)
                {
                    result[i] = 0;
                }
            }
        }

        private static void BackSubstitution(double[,] matrix, double[] answer, long rowCount)
        {
            for (long i = rowCount - 1; i >= 0; i--)
            {
                answer[i] = matrix[i, rowCount];
                for (long j = i + 1; j < rowCount; j++)
                {
                    answer[i] -= matrix[i, j] * answer[j];
                }

                answer[i] = answer[i] / matrix[i, i];
            }
        }

        private static void swapLines(double[,] matrix, long MATRIX_SIZE, long row_1, long row_2)
        {
            for (long i = 0; i <= MATRIX_SIZE; i++)
            {
                double tmp = matrix[row_1, i];
                matrix[row_1, i] = matrix[row_2, i];
                matrix[row_2, i] = tmp;
            }
        }
        public static double[] Solve(long MATRIX_SIZE, double[,] matrix)
        {
            double[] result = new double[MATRIX_SIZE];
            for (long i = 0; i < MATRIX_SIZE; i++)
            {
                long index = i;
                double max = (int)matrix[index, i];

                for (long j = i + 1; j < MATRIX_SIZE; j++)
                {
                    if (max == 0 && matrix[j, i] != 0)
                    {
                        swapLines(matrix, MATRIX_SIZE, i, j);
                    }
                    if (Math.Abs(matrix[j, i]) > max)
                    {
                        index = j;
                        max = (int)matrix[j, i];
                    }
                }

                if (max != 0)
                {
                    swapLines(matrix, MATRIX_SIZE, index, i);
                }
                
                for (long j = i + 1; j < MATRIX_SIZE; j++)
                {
                    double ratio = matrix[j, i] / matrix[i, i];
                    for (long k = i + 1; k < MATRIX_SIZE; k++)
                    {
                        matrix[j, k] -= ratio * matrix[i, k]; 
                    }
                    matrix[j, i] = 0;
                    matrix[j, MATRIX_SIZE] -= matrix[i, MATRIX_SIZE] * ratio;
                }

            }
            BackSubstitution(matrix, result, MATRIX_SIZE);
            Round(result);
            return result;
        }
    }
}
