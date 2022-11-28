using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class ZigzagConversion
    {
        public string Convert(string s, int numRows)
        {
            if (String.IsNullOrEmpty(s))
                return "";
            if (s.Length <= 2 || numRows < 2)
                return s;

            var result = new char[s.Length];
            short index = 0;
            short cycleLength = (short)(2 * numRows - 2);
            for (short i = 0; i < numRows; i++)
            {
                if (i == 0 || i == numRows - 1)
                    for (short j = i; j < s.Length; j += cycleLength)
                        result[index++] = s[j];
                else
                    for (short j = i; j < s.Length; j += cycleLength)
                    {
                        result[index++] = s[j];
                        if (j + cycleLength - 2 * i < s.Length)
                            result[index++] = s[j + cycleLength - 2 * i];
                    }
            }
            return new string(result);
        }
    }
}
