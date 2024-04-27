using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A77
{
    class Program
    {
        #region Q1
        /* 37 & 28 */
        public static int[] computePrefixFunction(string pattern)
        {
            long patternLength = pattern.Length;
            int[] s = new int[patternLength];
            s[0] = 0;
            int border = 0;
            for (int i = 1; i < patternLength; i++)
            {
                while (border > 0 && (pattern[i] != pattern[border]))
                {
                    border = s[border - 1];
                }
                if (pattern[i] == pattern[border])
                {
                    border += 1;
                }
                else
                {
                    border = 0;
                }
                s[i] = border;
            }
            return s;
        }
        public static long[] Solve(string text, string pattern)
        {
            int patternLen = pattern.Length;
            if (patternLen > text.Length)
            {
                return new long[]{};
            }
            StringBuilder S = new StringBuilder();
            S.Append(pattern + "$" + text);
            var s = computePrefixFunction(S.ToString());
            List<long> result = new List<long>();
            // int textLen = text.Length;
            for (int i = patternLen + 1; i < S.Length; i++)
            {
                if (s[i] == patternLen)
                {
                    result.Add(i - 2 * patternLen);
                }
            }
            return result.ToArray();
        }
        #endregion Q1
        /*22 & 26 & 35 & 40 & 42*/
        #region Q2
        public static long[] sortCharacters(string S)
        {
            Dictionary<char, int> countDict = new Dictionary<char, int>();

            countDict.Add('$', 0);
            countDict.Add('A', 1);
            countDict.Add('C', 2);
            countDict.Add('G', 3);
            countDict.Add('T', 4);

            long[] order = new long[S.Length];

            int[] count = new int[5];

            for (int i = 0; i < S.Length; i++)
            {
                count[countDict[S[i]]] += 1;
            }

            for (int j = 1; j < 5; j++)
            {
                count[j] += count[j - 1];
            }

            for (int i = S.Length - 1; i >= 0; i--)
            {
                char c = S[i];
                order[count[countDict[c]] -= 1] = i;
            }

            return order;
        }
        public static long[] computeCharClasses(string S, long[] order)
        {
            long[] classes = new long[S.Length];
            classes[order[0]] = 0;

            for (int i = 1; i < S.Length; i++)
            {
                if (S[(int)order[i]] != S[(int)order[i - 1]])
                {
                    classes[order[i]] = classes[order[i - 1]] + 1;
                }
                else
                {
                    classes[order[i]] = classes[order[i - 1]];
                }
            }

            return classes;
        }

        public static long[] sortDoubled(string S, long L, long[] order, long[] classes)
        {
            long[] count = new long[S.Length];
            long[] newOrder = new long[S.Length];

            for (int i = 0; i < S.Length; i++)
            {
                count[classes[i]] += 1;
            }

            for (int j = 1; j < S.Length; j++)
            {
                count[j] += count[j - 1];
            }

            for (int i = S.Length - 1; i >= 0; i--)
            {
                long start = (order[i] - L + S.Length) % S.Length;
                long cl = classes[start];
                count[cl] -= 1;
                newOrder[count[cl]] = start;
            }

            return newOrder;
        }
        public static long[] updateClasses(long[] newOrder, long[] classes, long L)
        {
            long n = newOrder.Length;
            long[] newClasses = new long[n];
            newClasses[newOrder[0]] = 0;
            for (long i = 1; i < n; i++)
            {
                long curr = newOrder[i];
                long prev = newOrder[i - 1];
                long mid = curr + L;
                long midPrev = (prev + L) % n;
                if (classes[curr] != classes[prev] || classes[mid] != classes[midPrev])
                {
                    newClasses[curr] = newClasses[prev] + 1;
                }
                else
                {
                    newClasses[curr] = newClasses[prev];
                }
            }
            return newClasses;
        }
        public static long[] Solve(string text)
        {
            var order = sortCharacters(text);
            var classes = computeCharClasses(text, order);
            long L = 1;
            while (L < text.Length)
            {
                order = sortDoubled(text, L, order, classes);
                classes = updateClasses(order, classes, L);
                L = 2 * L;
            }
            return order;
        }
        #endregion Q2
        #region Q3
        public static List<long> patternMatchingWithSuffixArray(string text, string pattern, long[] suffixArray)
        {
            List<long> res = new List<long>();
            long textLen = text.Length;
            long minIndex = 0;
            long maxIndex = textLen;
            long midIndex = 0;
            while (minIndex < maxIndex)
            {
                midIndex = (minIndex + maxIndex) / 2;
                string suffix = text.Substring((int)suffixArray[midIndex], Math.Min((int)suffixArray[midIndex] + pattern.Length, text.Length) - (int)suffixArray[midIndex]);
                if (pattern.CompareTo(suffix) > 0)
                {
                    minIndex = midIndex + 1;
                }
                else
                {
                    maxIndex = midIndex;
                }
            }
            long start = minIndex;
            maxIndex = textLen;
            while (minIndex < maxIndex)
            {
                midIndex = (minIndex + maxIndex) / 2;
                string suffix = text.Substring((int)suffixArray[midIndex], Math.Min((int)suffixArray[midIndex] + pattern.Length, text.Length) - (int)suffixArray[midIndex]);
                if (pattern.CompareTo(suffix) < 0)
                {
                    maxIndex = midIndex;
                }
                else
                {
                    minIndex = midIndex + 1;
                }
            }
            long end = maxIndex;
            if (start > end)
            {
                return null;
            }
            for (long i = start; i < end; i++)
            {
                res.Add(suffixArray[i]);
            }
            return res;
        }
        public static long[] Solve(string text, long n, string[] patterns)
        {
            List<long> res = new List<long>();
            long[] suffixArray = Solve(text + '$');

            for (int i = 0; i < n; i++)
            {
                var output = patternMatchingWithSuffixArray(text + '$', patterns[i], suffixArray);
                foreach(var o in output)
                {
                    res.Add(o);
                }
            }

            if (res.Count == 0)
            {
                return new long[]{-1};
            }
            return res.ToArray();
        }
        #endregion Q3
        static void Main(string[] args)
        {
            // string p = Console.ReadLine();
            // string t = Console.ReadLine();
            // var res = string.Join(" ", Solve(t, p));
            // System.Console.WriteLine(res);
            // *************************************
            
            // string t = Console.ReadLine();
            // var res = string.Join(" ", Solve(t));
            // System.Console.WriteLine(res);
            // *************************************

            string t = Console.ReadLine();
            int n = Convert.ToInt32(Console.ReadLine());
            string[] patterns = new string[n];
            for (int i = 0; i < n; i++)
            {
                patterns = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToString);
            }
            var res = string.Join(" ", Solve(t, n, patterns));
            System.Console.WriteLine(res);
        }
    }
}
