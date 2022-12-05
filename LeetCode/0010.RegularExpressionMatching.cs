using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    // [InProcess]
    public class RegularExpressionMatching
    {
        public bool IsMatchRecursive(string s, string p)
        {
            return IsMatchRecursive(s.AsSpan(), p.AsSpan());
        }

        private bool IsMatchRecursive(ReadOnlySpan<char> s, ReadOnlySpan<char> p)
        {
            if (s.Length == 0 && p.Length == 0) return true;
            if (p.Length == 0) return false;
            var c = p[0];
            var any = p.Length > 1 && p[1] == '*';

            if (any)
            {
                if (s.Length > 0 && (c == s[0] || c == '.') && IsMatchRecursive(s.Slice(1), p))
                    return true;
                if (IsMatchRecursive(s, p.Slice(2)))
                    return true;
            }
            else if (s.Length > 0 && (c == s[0] || c == '.') && IsMatchRecursive(s.Slice(1), p.Slice(1)))
                return true;
            return false;
        }

        public bool IsMatchDynamic(string s, string p)
        {
            var dp = new bool[s.Length + 1, p.Length + 1];
            dp[s.Length, p.Length] = true;

            for (var i = s.Length; i >= 0; i--)
            {
                for (var j = p.Length - 1; j >= 0; j--)
                {
                    var firstMatch = (i < s.Length &&
                                           (p[j] == s[i] ||
                                            p[j] == '.'));
                    if (j + 1 < p.Length && p[j + 1] == '*')
                    {
                        dp[i, j] = dp[i, j + 2] || (firstMatch && dp[i + 1, j]);
                    }
                    else
                    {
                        dp[i, j] = firstMatch && dp[i + 1, j + 1];
                    }
                }
            }
            return dp[0, 0];
        }
    }
}
