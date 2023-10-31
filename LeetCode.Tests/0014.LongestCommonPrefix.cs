using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0014_LongestCommonPrefix
    {
        [Theory]
        [ClassData(typeof(LongestCommonPrefixTestData))]
        public void Solution(string[] strs, string expectedResult)
        {
            var longestCommonPrefix = new LongestCommonPrefix();
            var result = longestCommonPrefix.Solution(strs);
            result.Should().Be(expectedResult);
        }
    }

    public class LongestCommonPrefixTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new string[]{"Flower", "Flow", "Flight"},
                "Fl"
            };
            yield return new object[] {
                new string[] {"Dog", "Racecar", "Car"},
                ""
            };
            yield return new object[] {
                new string[0],
                ""
            };
            yield return new object[] {
                new string[]{ "Wahahaha" },
                "Wahahaha"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}