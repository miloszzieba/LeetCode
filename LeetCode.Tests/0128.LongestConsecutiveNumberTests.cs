using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0128_LongestConsecutiveNumberTests
    {
        [Theory]
        [ClassData(typeof(LongestConsecutiveNumberTestData))]
        public void SortedArray(int[] array, int expected)
        {
            var longestConsecutiveNumber = new LongestConsecutiveNumber();
            var result = longestConsecutiveNumber.SortedArray(array);
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(LongestConsecutiveNumberTestData))]
        public void SortedDictionary(int[] array, int expected)
        {
            var longestConsecutiveNumber = new LongestConsecutiveNumber();
            var result = longestConsecutiveNumber.SortedDictionary(array);
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(LongestConsecutiveNumberTestData))]
        public void HashSet(int[] array, int expected)
        {
            var longestConsecutiveNumber = new LongestConsecutiveNumber();
            var result = longestConsecutiveNumber.HashSet(array);
            result.Should().Be(expected);
        }
    }

    public class LongestConsecutiveNumberTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                (int[])[ 100, 4, 200, 1, 3, 2],
                4
            };
            yield return new object[] {
                (int[])[ 0, 3, 7, 2, 5, 8, 4, 6, 0, 1],
                9
            };
            yield return new object[] {
                Array.Empty<int>(),
                0
            };
            yield return new object[] {
                (int[])[ 1, 0, 1, 2],
                3
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

