using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0647_PalindromicSubstringsTests
    {
        [Theory]
        [ClassData(typeof(PalindromicSubstringsTestData))]
        public void BruteForce(string s, int expected)
        {
            var palindromicSubstrings = new PalindromicSubstrings();
            var result = palindromicSubstrings.BruteForce(s);
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(PalindromicSubstringsTestData))]
        public void DynamicProgramming(string s, int expected)
        {
            var palindromicSubstrings = new PalindromicSubstrings();
            var result = palindromicSubstrings.DynamicProgramming(s);
            result.Should().Be(expected);
        }


        [Theory]
        [ClassData(typeof(PalindromicSubstringsTestData))]
        public void DynamicProgrammingEvenAndOdd(string s, int expected)
        {
            var palindromicSubstrings = new PalindromicSubstrings();
            var result = palindromicSubstrings.DynamicProgrammingEvenAndOdd(s);
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(PalindromicSubstringsTestData))]
        public void Manacher(string s, int expected)
        {
            var palindromicSubstrings = new PalindromicSubstrings();
            var result = palindromicSubstrings.Manacher(s);
            result.Should().Be(expected);
        }
    }

    public class PalindromicSubstringsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "abc",
                3
            };
            yield return new object[] {
                "aaa",
                6
            };
            yield return new object[] {
                "",
                0
            };
            yield return new object[] {
                "ababa",
                9
            };
            yield return new object[] {
                "abababababababbababbbbabbababababbab",
                124
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

