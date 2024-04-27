using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C66
{
    class Program
    {

        #region Q1
        // public static string Solve(string text)
        // {
        //     int textLength = text.Length;
        //     string newText = text.Substring(0, textLength - 1);
        //     char[][] BWT = new char[textLength][];
        //     for (int i = 0; i < textLength; i++)
        //     {
        //         BWT[i] = new char[textLength];
        //     }
        //     BWT[0] = ('$' + newText).ToCharArray();
        //     for (int i = 1; i < textLength; i++)
        //     {
        //         for (int j = 0; j < textLength; j++)
        //         {
        //             if (j == 0)
        //             {
        //                 BWT[i][j] = BWT[i - 1][textLength - 1];
        //             }
        //             else
        //             {
        //                 BWT[i][j] = BWT[i - 1][j - 1];
        //             }
        //         }
        //     }
        //     StringBuilder result = new StringBuilder();
        //     string[] newArray = new string[textLength];
        //     for (int row = 0; row < textLength; row++)
        //     {
        //         newArray[row] = new string(BWT[row]);
        //     }
        //     Array.Sort(newArray);
        //     for (int i = 0; i < textLength; i++)
        //     {
        //         result.Append(newArray[i][textLength - 1]);
        //     }
        //     return result.ToString();
        // }
        #endregion Q1
        #region Q2

        // public static string Solve(string bwt)
        // {
        //     StringBuilder result = new StringBuilder(); // save each char of the bwt and its position
        //     List<Tuple<char,int>> bwtMatrix = new List<Tuple<char,int>>();
        //     for (int i = 0; i < bwt.Length; i++) 
        //     {
        //         bwtMatrix.Add(Tuple.Create(bwt[i],i));
        //     }
        //     // var sorted = bwtMatrix.OrderBy(i => i.Item1).ToList();
        //     bwtMatrix.Sort(); // sort by the ascii value of chars
        //     var current = bwtMatrix[0];
        //     for (int i = 0; i < bwt.Length - 1; i++) 
        //     {
        //         current = bwtMatrix[current.Item2];
        //         result.Append(current.Item1);
        //     }
            
        //     return result.ToString() + "$";  
        // }
        #endregion Q2
        #region Q3
        public static void preprocessBWT(string bwt, ref Dictionary<char, int> firstOccurrences, ref Dictionary<char, int[]> Count)
        {
            // firstOccurrences searches in the First Column(its maximum length is 5),
            // it saves the first position that each char was seen in the first column
            firstOccurrences = new Dictionary<char, int>();
            firstOccurrences.Add('A', -1);
            firstOccurrences.Add('C', -1);
            firstOccurrences.Add('G', -1);
            firstOccurrences.Add('T', -1);
            firstOccurrences.Add('$', -1);

            char[] characters = bwt.ToArray();
            Array.Sort(characters);
            string sortedBWT = new string(characters);

            for (int i = 0; i < bwt.Length; i++)
            {
                if (firstOccurrences[sortedBWT[i]] == -1)
                {
                    firstOccurrences[sortedBWT[i]] = i;
                }
            }

            // Count searches in the Last Column(bwt)
            Count = new Dictionary<char, int[]>();
            Count.Add('A',  new int[bwt.Length + 1]);
            Count.Add('C',  new int[bwt.Length + 1]);
            Count.Add('G',  new int[bwt.Length + 1]);
            Count.Add('T',  new int[bwt.Length + 1]);
            Count.Add('$',  new int[bwt.Length + 1]);
            // {
            //     ['A'] = new int[bwt.Length + 1],
            //     ['C'] = new int[bwt.Length + 1],
            //     ['G'] = new int[bwt.Length + 1],
            //     ['T'] = new int[bwt.Length + 1],
            //     ['$'] = new int[bwt.Length + 1],
            // };

            for (int i = 0; i < bwt.Length; i++)
            {
                char current = bwt[i];
                foreach (var pair in Count)
                {
                    if (pair.Key == current)
                    {
                        pair.Value[i + 1] = pair.Value[i] + 1;
                    }
                    else
                    {
                        pair.Value[i + 1] = pair.Value[i];
                    }
                }
            }
        }
        public static long BetterBWMatching(string bwt, string pattern , Dictionary<char, int> firstOccurrences, Dictionary<char, int[]> Count)
        {
            int top = 0;
            int bottom = bwt.Length - 1;
            // bool flag = false;
            while (top <= bottom)
            {
                if (pattern.Length != 0)
                {
                    int lastIndex = pattern.Length - 1;
                    char symbol = pattern[lastIndex];
                    pattern = pattern.Remove(lastIndex);
                    if (firstOccurrences[symbol] == -1)
                    {
                        return 0;
                    }

                    top = firstOccurrences[symbol] + Count[symbol][top];
                    bottom = firstOccurrences[symbol] + Count[symbol][bottom + 1] - 1;

                }
                else
                {
                    return bottom - top + 1;
                }
            }
            return 0;
        }
        public static long[] Solve(string text, long n, String[] patterns)
        {
            Dictionary<char, int> firstOccurrences = new Dictionary<char, int>();
            Dictionary<char, int[]> Count = new Dictionary<char, int[]>();
            preprocessBWT(text, ref firstOccurrences, ref Count);
            long[] result = new long[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = BetterBWMatching(text, patterns[i], firstOccurrences, Count);
            }
            return result;
        }
        #endregion Q3
        #region Q4
        // public static long[] computeSuffixArray(string text)
        // {
        //     long[] result = new long[text.Length];

        //     List<Tuple<string, int>> suffix = new List<Tuple<string, int>>();
        //     for (int i = 0; i < text.Length; i++)
        //     {
        //         suffix.Add(Tuple.Create(text.Substring(i), i));
        //     }
        //     suffix.Sort();
        //     for (int i = 0; i < text.Length; i++)
        //     {
        //         result[i] = suffix[i].Item2;
        //     }
        //     return result;
        // }
        #endregion
        static void Main(string[] args)
        {
            // string input = Console.ReadLine();
            // System.Console.WriteLine(Solve(input));
            // *******************************************
            // string input = Console.ReadLine();
            // System.Console.WriteLine(Solve(input));
            // *******************************************
            string text = Console.ReadLine();
            int noOfpatterns = Convert.ToInt32(Console.ReadLine());
            string[] patterns = new string[noOfpatterns];
            for (int i = 0; i < noOfpatterns; i++)
            {
                patterns[i] = Console.ReadLine();
            }
            var res = string.Join(" ", Solve(text, noOfpatterns, patterns));

            System.Console.WriteLine(res);
            // *********************************************

            // string input = Console.ReadLine();

            // var res = string.Join(" ", computeSuffixArray(input));
            // System.Console.WriteLine(res);

        }
    }
}
