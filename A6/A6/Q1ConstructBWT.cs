using System;
using System.Text;
using TestCommon;

namespace A6
{
    public class Q1ConstructBWT : Processor
    {
        public Q1ConstructBWT(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Construct the Burrows–Wheeler transform of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> BWT(Text) </returns>
        public string Solve(string text)
        {
            int textLength = text.Length;
            string newText = text.Substring(0, textLength - 1);
            char[][] BWT = new char[textLength][];
            for (int i = 0; i < textLength; i++)
            {
                BWT[i] = new char[textLength];
            }
            BWT[0] = ('$' + newText).ToCharArray();
            for (int i = 1; i < textLength; i++)
            {
                for (int j = 0; j < textLength; j++)
                {
                    if (j == 0)
                    {
                        BWT[i][j] = BWT[i - 1][textLength - 1];
                    }
                    else
                    {
                        BWT[i][j] = BWT[i - 1][j - 1];
                    }
                }
            }
            StringBuilder result = new StringBuilder();
            string[] newArray = new string[textLength];
            for (int row = 0; row < textLength; row++)
            {
                newArray[row] = new string(BWT[row]);
            }
            Array.Sort(newArray);
            for (int i = 0; i < textLength; i++)
            {
                result.Append(newArray[i][textLength - 1]);
            }
            return result.ToString();
        }
    }
}
