using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0016_ThreeSumClosestTests
    {
        [Theory]
        [ClassData(typeof(ThreeSumClosestTestData))]
        public void BruteForce(int[] nums, int target, int expected)
        {
            var threeSumClosest = new ThreeSumClosest();
            var result = threeSumClosest.BruteForce(nums, target);
        }

        [Theory]
        [ClassData(typeof(ThreeSumClosestTestData))]
        public void TwoPointers(int[] nums, int target, int expected)
        {
            var threeSumClosest = new ThreeSumClosest();
            var result = threeSumClosest.TwoPointers(nums, target);

            Assert.Equal(expected, result);
        }
    }

    public class ThreeSumClosestTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new int[] { -1, -1, 0 },
                0,
                -2
            };
            yield return new object[] {
                new int[] { 0, 0, 0 },
                0,
                0
            };
            yield return new object[] {
                new int[] {-1, 0, 1, 2, -1, -4 },
                4,
                3
            };
            yield return new object[] {
                new int[] {-2,-3,0,0,-2},
                5,
                -2
            };
            yield return new object[] {
                new int[] {-1, 2, -1, -4},
                1,
                0
            };
            yield return new object[] {
                new int[] {-1000, -1000, -1000},
                10000,
                -3000
            };
            yield return new object[]
            {
                new int[] {4, 0, 5, -5, 3, 3, 0, -4, -5},
                -2,
                -2
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

