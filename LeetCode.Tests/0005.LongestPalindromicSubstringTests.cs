using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0005_LongestPalindromicSubstringTests
    {

        [Theory]
        [ClassData(typeof(LongestPalindromicSubstringTestData))]
        public void FromTheMiddle(string s, string[] expectedResult)
        {
            var longestPalindromicSubstring = new LongestPalindromicSubstring();
            var result = longestPalindromicSubstring.FromTheMiddle(s);
            expectedResult.Should().Contain(result);
        }
    }

    public class LongestPalindromicSubstringTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "",
                new [] { "" }
            };
            yield return new object[] {
                null,
                new [] { "" }
            };
            yield return new object[] {
                "a",
                new [] { "a" }
            };
            yield return new object[] {
                "ab",
                new [] { "a", "b"}
            };
            yield return new object[] {
                "aa",
                new [] { "aa" }
            };
            yield return new object[] {
                "aab",
                new [] { "aa" }
            };
            yield return new object[] {
                "baa",
                new [] { "aa" }
            };
            yield return new object[] {
                "bab",
                new [] { "bab" }
            };
            yield return new object[] {
                "baba",
                new [] { "bab", "aba" }
            };
            yield return new object[] {
                "babab",
                new [] { "babab" }
            };
            yield return new object[] {
                "babab123",
                new [] { "babab" }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}