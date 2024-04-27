using System;
using System.Collections.Generic;
using TestCommon;

namespace A6
{
    public class Q4ConstructSuffixArray : Processor
    {
        public Q4ConstructSuffixArray(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        /// <summary>
        /// Construct the suffix array of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> SuffixArray(Text), that is, the list of starting positions
        /// (0-based) of sorted suffixes separated by spaces </returns>
        public long[] Solve(string text)
        {
            long[] result = new long[text.Length];

            List<Tuple<string, int>> suffix = new List<Tuple<string, int>>();
            for (int i = 0; i < text.Length; i++)
            {
                suffix.Add(Tuple.Create(text.Substring(i), i));
            }
            suffix.Sort();
            for (int i = 0; i < text.Length; i++)
            {
                result[i] = suffix[i].Item2;
            }
            return result;
        }
    }
}
