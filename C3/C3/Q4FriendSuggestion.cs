using System;
using System.Collections.Generic;
using TestCommon;

namespace C3
{
    public class Q4FriendSuggestion : Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], 
            long, long[][], long[]>)Solve);

        public long[] Solve(long nodeCount, long edgeCount,
                              long[][] edges, long queriesCount,
                              long[][] queries)
        {
            throw new NotImplementedException();
        }
    }
}
