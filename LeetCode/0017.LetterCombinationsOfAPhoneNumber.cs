using BenchmarkDotNet.Attributes;
using Iced.Intel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class LetterCombinationsOfAPhoneNumber
    {
        private static readonly char[][] dict = 
        {
            null,
            null,
            new char[] {'a', 'b', 'c' },
            new char[] {'d', 'e', 'f' },
            new char[] {'g', 'h', 'i' },
            new char[] {'j', 'k', 'l' },
            new char[] {'m', 'n', 'o' },
            new char[] {'p', 'q', 'r', 's' },
            new char[] {'t', 'u', 'v' },
            new char[] {'w', 'x', 'y', 'z' }
        };

        public IList<string> Solution(string digits)
        {
            if (String.IsNullOrEmpty(digits)) return new List<string>();
            var result = new List<string>();

            var chars = digits.Select(x => dict[x - '0']).ToArray();
            var answerLength = chars.Aggregate(1, (x, y) => x * y.Length);

            var charArray = new char[digits.Length];
            int divider = 1;
            for (int i = 0; i < answerLength; i++)
            {
                divider = 1;
                for (int j = 0; j < digits.Length; j++)
                {
                    divider = divider * chars[j].Length;
                    charArray[j] = chars[j][(i / (answerLength / divider)) % chars[j].Length];
                }
                result.Add(new string(charArray));
            }

            return result;
        }


    }
}