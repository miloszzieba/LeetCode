using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class RomanLiterals
    {
        private readonly Dictionary<char, int> dict = new Dictionary<char, int>(){
        { 'M', 1000 },
        { 'D', 500 },
        { 'C', 100 },
        { 'L', 50 },
        { 'X', 10 },
        { 'V', 5 },
        { 'I', 1 }
    };

        public int RomanToInt(string s)
        {
            var sum = 0;
            int num;
            for (int i = 0; i < s.Length; i++)
            {
                num = dict[s[i]];
                if (i + 1 < s.Length && dict[s[i + 1]] > num)
                    sum -= num;
                else
                    sum += num;
            }
            return sum;
        }
    }
}
