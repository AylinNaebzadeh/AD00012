class constructSuffixArray
{
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
}