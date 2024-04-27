using System.Collections.Generic;

class Q2
{
        private static bool optimalKmer(int k, List<string> reads)
        {
            HashSet<string> kmers = new HashSet<string>();
            foreach (var read in reads)
            {
                for (int i = 0; i < read.Length - k + 1; i++)
                {
                    kmers.Add(read.Substring(i, k));
                }
            }

            HashSet<string> prefix = new HashSet<string>();
            HashSet<string> suffix = new HashSet<string>();

            foreach (var kmer in kmers)
            {
                prefix.Add(kmer.Substring(0, kmer.Length - 1));
                suffix.Add(kmer.Substring(1));
            }

            foreach (var pre in prefix)
            {
                if (!suffix.Contains(pre))
                {
                    return false;
                }
            }
            return true;
        }
}