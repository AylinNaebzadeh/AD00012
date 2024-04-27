class Node
{
    public Dictionary<char, Node> children = new Dictionary<char, Node>();
    public bool isLeaf = false;
}


class TrieMatching
{
    public static long[] Solve(string text, long n, string[] patterns)
    {
        List<long> result = new List<long>();
        Node root = new Node();
        foreach (var pattern in patterns)
        {
            Node currentNode = root;
            for (int i = 0; i < pattern.Length; i++)
            {
                char currentSymbol = pattern[i];
                // The new if for duplicate patterns
                if (!currentNode.children.ContainsKey(currentSymbol))
                {
                    currentNode.children[currentSymbol] = new Node();
                }
                if (pattern.Length - 1 == i)
                {
                    currentNode.children[currentSymbol].isLeaf = true;
                }
                else
                {
                    currentNode = currentNode.children[currentSymbol];
                }
            }
        }
        // This part is to check prefixTrieMatching
        for (int i = 0; i < text.Length; i++)
        {
            int position = i;
            Node currentNode = root;
            while (position < text.Length)
            {
                char currentSymbol = text[position];
                if (!currentNode.children.ContainsKey(currentSymbol))
                {
                    break;
                }
                currentNode = currentNode.children[currentSymbol];
                if (currentNode.isLeaf)
                {
                    result.Add(i);
                    break;
                }
                position++;
            }
        }
        if (result.Count == 0)
        {
            return new long[]{};
        }
        result.Sort();
        return result.ToArray();
    }
}