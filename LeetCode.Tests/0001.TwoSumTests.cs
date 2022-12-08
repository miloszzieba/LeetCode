using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0001_TwoSumTests
    {

        [Theory]
        [ClassData(typeof(TwoSumTestData))]
        public void TwoLoops(int[] nums, int target, int[] expectedResult)
        {
            var twoSum = new TwoSum();
            var result = twoSum.TwoLoops(nums, target);
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [ClassData(typeof(TwoSumTestData))]
        public void TwoLoopsSorted(int[] nums, int target, int[] expectedResult)
        {
            var twoSum = new TwoSum();
            var result = twoSum.TwoLoopsSorted(nums, target);
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [ClassData(typeof(TwoSumTestData))]
        public void Dictionary(int[] nums, int target, int[] expectedResult)
        {
            var twoSum = new TwoSum();
            var result = twoSum.Dictionary(nums, target);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }

    public class TwoSumTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new [] {3, 2, 4 },
                6,
                new [] { 1, 2 }
            };
            yield return new object[] {
                new [] {2, 7, 11, 15 },
                9,
                new [] { 0, 1 }
            };
            yield return new object[] {
                new [] {3, 3 },
                6,
                new [] { 0, 1 }
            };
            yield return new object[] {
                new [] {-9, 3, 2, 1 },
                -8,
                new [] { 0, 3 }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}