using System;
using System.Collections.Generic;
using System.Linq;

namespace Q1
{
    class Program
    {
        
        private static int calculateOverlap(string read1, string read2)
        {
            for (int i = 0; i < read1.Length; ++i)
            {
                if (read1.Substring(i) == read2.Substring(0, read1.Length - i))
                {
                    return read1.Length - i;
                }
            }
            return 0;
        }

        static void Main(string[] args)
        {
            List<string> reads = new List<string>();
            string s = "";
            while (!string.IsNullOrEmpty(s = Console.ReadLine()))
            {
                reads.Add(s);
            }

            bool[] visited = new bool[reads.Count];
            int current = 0;
            string final = reads[0];
            for (int i = 0; i < reads.Count; i++)
            {
                visited[current] = true;
                int index = -1;
                int best = 0;
                for (int j = 0; j < reads.Count; ++j)
                {
                    if (!visited[j])
                    {
                        int overlap = calculateOverlap(reads[current], reads[j]);
                        if (overlap >= best)
                        {
                            best = overlap;
                            index = j;
                        }
                    }
                }
                if (index == -1)
                {
                    break;
                }
                final += reads[index].Substring(best);
                current = index;
            }
            Console.WriteLine(final.Substring(calculateOverlap(reads[current], reads[0])));
        }
    }
}