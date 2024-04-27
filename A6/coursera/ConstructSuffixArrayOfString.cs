class constructSuffixArray
{
    public static long[] computeSuffixArray(string text)
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