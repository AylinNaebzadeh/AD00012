using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A6
{
    public class Q3MatchingAgainCompressedString : Processor
    {
        public Q3MatchingAgainCompressedString(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        /// <summary>
        /// Implement BetterBWMatching algorithm
        /// </summary>
        /// <param name="text"> A string BWT(Text) </param>
        /// <param name="n"> Number of patterns </param>
        /// <param name="patterns"> Collection of n strings Patterns </param>
        /// <returns> A list of integers, where the i-th integer corresponds
        /// to the number of substring matches of the i-th member of Patterns
        /// in Text. </returns>
        public static void preprocessBWT(string bwt, ref Dictionary<char, int> firstOccurrences, ref Dictionary<char, int[]> Count)
        {
            // firstOccurrences searches in the First Column(its maximum length is 5),
            // it saves the first position that each char was seen in the first column
            firstOccurrences = new Dictionary<char, int>{['A'] = -1, ['C'] = -1, ['G'] = -1, ['T'] = -1, ['$'] = -1};

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
            Count = new Dictionary<char, int[]>
            {
                ['A'] = new int[bwt.Length + 1],
                ['C'] = new int[bwt.Length + 1],
                ['G'] = new int[bwt.Length + 1],
                ['T'] = new int[bwt.Length + 1],
                ['$'] = new int[bwt.Length + 1],
            };

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
        public long[] Solve(string text, long n, String[] patterns)
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
    }
}
