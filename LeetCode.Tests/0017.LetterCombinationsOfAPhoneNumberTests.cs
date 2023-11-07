using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0017_LetterCombinationsOfAPhoneNumberTests
    {
        [Theory]
        [ClassData(typeof(LetterCombinationsOfAPhoneNumberTestData))]
        public void Solution(string digits, List<string> expected)
        {
            var letterCombinationsOfAPhoneNumber = new LetterCombinationsOfAPhoneNumber();
            var result = letterCombinationsOfAPhoneNumber.Solution(digits);

            Assert.Equal(expected.Count, result.Count);
            if (!expected.All(x => result.Contains(x)))
                Assert.False(true);
        }
    }

    public class LetterCombinationsOfAPhoneNumberTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "",
                new List<string>()
            };
            yield return new object[] {
                "2",
                new List<string>() { "a", "b", "c" }
            };
            yield return new object[] {
                "23",
                new List<string>() { "ad", "bd", "cd", "ae", "be", "ce", "af", "bf", "cf", }
            };
            yield return new object[] {
                "79",
                new List<string>() { "pw", "qw", "rw", "sw", "px", "qx", "rx", "sx", "py", "qy", "ry", "sy", "pz", "qz", "rz", "sz" }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

