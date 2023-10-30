using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0015_3SumTests
    {
        [Theory]
        [ClassData(typeof(_3SumTestData))]
        public void _3SumBruteForce(int[] nums, List<List<int>> expected)
        {
            var _3sum = new _3Sum();
            var result = _3sum._3SumBruteForce(nums);

            result.Count.Should().Be(expected.Count);
            if (!result.All(x => expected.Any(y => y.All(z => x.Contains(z)))))
                Assert.False(true);
        }
    }

    public class _3SumTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new int[] { -1, -1, 0 },
                new List<List<int>>()
            };
            yield return new object[] {
                new int[] { 0, 0, 0 },
                new List<List<int>>()
                {
                    new List<int>() { 0, 0, 0 }
                }
            };
            yield return new object[] {
                new int[] {-1, 0, 1, 2, -1, -4 },
                new List<List<int>>()
                {
                    new List<int>() { -1, 0, 1 },
                    new List<int>() { -1, -1, 3 },
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}