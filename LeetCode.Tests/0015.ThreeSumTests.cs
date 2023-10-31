using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0015_ThreeSumTests
    {
        [Theory]
        [ClassData(typeof(ThreeSumTestData))]
        public void BruteForce(int[] nums, List<List<int>> expected)
        {
            var threeSum = new ThreeSum();
            var result = threeSum.BruteForce(nums);

            result.Count().Should().Be(expected.Count);
            if (!result.All(x => expected.Any(y => y.All(z => x.Contains(z)))))
                Assert.False(true);
        }

        [Theory]
        [ClassData(typeof(ThreeSumTestData))]
        public void BruteForceTrimmed(int[] nums, List<List<int>> expected)
        {
            var threeSum = new ThreeSum();
            var result = threeSum.BruteForceTrimmed(nums);

            result.Count().Should().Be(expected.Count);
            if (!result.All(x => expected.Any(y => y.All(z => x.Contains(z)))))
                Assert.False(true);
        }

        [Theory]
        [ClassData(typeof(ThreeSumTestData))]
        public void TwoPointers(int[] nums, List<List<int>> expected)
        {
            var threeSum = new ThreeSum();
            var result = threeSum.TwoPointers(nums);

            result.Count().Should().Be(expected.Count);
            if (!result.All(x => expected.Any(y => y.All(z => x.Contains(z)))))
                Assert.False(true);
        }

        [Theory]
        [ClassData(typeof(ThreeSumTestData))]
        public void Dictionary(int[] nums, List<List<int>> expected)
        {
            var threeSum = new ThreeSum();
            var result = threeSum.Dictionary(nums);

            result.Count().Should().Be(expected.Count);
            if (!result.All(x => expected.Any(y => y.All(z => x.Contains(z)))))
                Assert.False(true);
        }
    }

    public class ThreeSumTestData : IEnumerable<object[]>
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
                new int[] { 0, 0, 0, 0 },
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
                    new List<int>() { -1, -1, 2 },
                }
            };
            yield return new object[] {
                new int[] {-2,-3,0,0,-2},
                new List<List<int>>()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

