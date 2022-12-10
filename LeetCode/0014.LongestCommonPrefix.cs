using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class LongestCommonPrefix
    {
        public string Solution(string[] strs)
        {
            if (strs.Length == 0)
                return "";
            if (strs.Length == 1)
                return strs[0];

            int longest = strs[0].Length;
            for (int i = 1; i < strs.Length; i++)
            {

                if (longest > strs[i].Length)
                    longest = strs[i].Length;
                for (int j = 0; j < longest; j++)
                    if (strs[0][j] != strs[i][j])
                    {
                        longest = j;
                        break;
                    }
                if (longest == 0)
                    return String.Empty;
            }
            return strs[0].Substring(0, longest);
        }
    }
}
