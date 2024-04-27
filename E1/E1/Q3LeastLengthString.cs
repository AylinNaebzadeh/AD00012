using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace E1
{
    public class Q3LeastLengthString : Processor
    {
        public Q3LeastLengthString(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        E1Processors.ProcessQ3FindAllOccur(inStr, Solve);

        public long Solve(string text, long k)
        {
            throw new NotImplementedException();
        }
    }
}