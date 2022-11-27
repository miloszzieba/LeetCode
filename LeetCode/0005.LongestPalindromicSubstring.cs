using LeetCode.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class LongestPalindromicSubstring
    {
        public string FromTheMiddle(string s)
        {
            if (String.IsNullOrEmpty(s)) return "";
            if (s.Length == 1) return s;
            if (s.Length == 2 && s[0] != s[1]) return s.Substring(0, 1);

            var longest = GetLongestPalindromeFromTheMiddle(s.AsSpan());
            int longestIndex = (s.Length - longest) / 2;

            if (longest == s.Length)
                return s;

            for (int maxLength = s.Length - 1; maxLength > 1; maxLength--)
            {
                if (longest == maxLength)
                    return s.Substring(longestIndex, longest);

                var length = GetLongestPalindromeFromTheMiddle(s.AsSpan(0, maxLength));
                if (length > longest)
                {
                    longest = length;
                    longestIndex = (maxLength - longest) / 2;
                    if (longest == maxLength)
                        return s.Substring(longestIndex, longest);
                }

                length = GetLongestPalindromeFromTheMiddle(s.AsSpan((s.Length - maxLength), maxLength));
                if (length > longest)
                {
                    longest = length;
                    longestIndex = (s.Length - maxLength) + ((maxLength - longest) / 2);
                    if (longest == maxLength)
                        return s.Substring(longestIndex, longest);
                }
            }

            return s.Substring(longestIndex, longest);
        }

        private int GetLongestPalindromeFromTheMiddle(ReadOnlySpan<char> span)
        {
            int left = (span.Length - 1) / 2;
            int right = left + 1 - span.Length % 2; // + 1 if even
            while (left >= 0 && right < span.Length && span[left] == span[right])
            {
                left--;
                right++;
            }
            return right - left - 1;
        }
    }
}
