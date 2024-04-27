using System;
using System.Collections.Generic;
using TestCommon;
using System.Linq;

namespace E1
{
    public class Q2SubStrings : Processor
    {
        public Q2SubStrings(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) => 
        E1Processors.ProcessQ3SubStrings(inStr, Solve);

        public virtual long Solve(long n, String text)
        {
            throw new NotImplementedException();
        }   
    }
}
