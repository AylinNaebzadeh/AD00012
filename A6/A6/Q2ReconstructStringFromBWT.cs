using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A6
{
    public static class sbExtension
    {
        public static void Reverse(this StringBuilder sb)
        {
            char t;
            int end = sb.Length - 1;
            int start = 0;
            
            while (end - start > 0)
            {
                t = sb[end];
                sb[end] = sb[start];
                sb[start] = t;
                start++;
                end--;
            }
        }
    }
    public class Q2ReconstructStringFromBWT : Processor
    {
        public Q2ReconstructStringFromBWT(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Reconstruct a string from its Burrows–Wheeler transform
        /// </summary>
        /// <param name="bwt"> A string Transform with a single “$” sign </param>
        /// <returns> The string Text such that BWT(Text) = Transform.
        /// (There exists a unique such string.) </returns>
        public string Solve(string bwt)
        {
            StringBuilder result = new StringBuilder(); // save each char of the bwt and its position
            List<(char,int)> bwtMatrix = new List<(char,int)>();
            for (int i = 0; i < bwt.Length; i++) 
            {
                bwtMatrix.Add((bwt[i],i));
            }
            // var sorted = bwtMatrix.OrderBy(i => i.Item1).ToList();
            bwtMatrix.Sort(); // sort by the ascii value of chars
            var current = bwtMatrix[0];
            for (int i = 0; i < bwt.Length - 1; i++) 
            {
                current = bwtMatrix[current.Item2];
                result.Append(current.Item1);
            }
            
            return result.ToString() + "$";  
        }
    }
}
