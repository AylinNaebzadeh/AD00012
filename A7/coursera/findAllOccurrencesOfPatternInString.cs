class findAllOccurrences
{
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
}