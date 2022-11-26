using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class LongestSubstringWithoutRepeatingCharactersTests
    {

        [Theory]
        [ClassData(typeof(LongestSubstringWithoutRepeatingCharactersTestData))]
        public void CharDictionary(string s, int expectedResult)
        {
            var longestSubstring = new LongestSubstringWithoutRepeatingCharacters();
            var result = longestSubstring.CharDictionary(s);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [ClassData(typeof(LongestSubstringWithoutRepeatingCharactersTestData))]
        public void CharArray(string s, int expectedResult)
        {
            var longestSubstring = new LongestSubstringWithoutRepeatingCharacters();
            var result = longestSubstring.CharArray(s);
            result.Should().Be(expectedResult);
        }


        [Theory]
        [ClassData(typeof(LongestSubstringWithoutRepeatingCharactersTestData))]
        public void CharHashset(string s, int expectedResult)
        {
            var longestSubstring = new LongestSubstringWithoutRepeatingCharacters();
            var result = longestSubstring.CharHashSet(s);
            result.Should().Be(expectedResult);
        }
    }

    public class LongestSubstringWithoutRepeatingCharactersTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "abcabcbb",
                3
            };
            yield return new object[] {
                "bbbbbb",
                1
            };
            yield return new object[] {
                "pwwkew",
                3
            };
            yield return new object[] {
                " ",
                1
            };
            yield return new object[] {
                "dvdf",
                3
            };
            yield return new object[] {
                "~ ",
                2
            };
            yield return new object[] {
                "abcdeeeeabcdefghjkl",
                11
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}