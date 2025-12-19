using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0152_MaximumProductSubarrayTests
    {
        [Theory]
        [ClassData(typeof(MaximumProductSubarrayTestData))]
        public void BruteForce(int[] array, int expected)
        {
            var maximumProductSubarray = new MaximumProductSubarray();
            var result = maximumProductSubarray.BruteForce(array);
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(MaximumProductSubarrayTestData))]
        public void Scan(int[] array, int expected)
        {
            var maximumProductSubarray = new MaximumProductSubarray();
            var result = maximumProductSubarray.Scan(array);
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(MaximumProductSubarrayTestData))]
        public void MinMax(int[] array, int expected)
        {
            var maximumProductSubarray = new MaximumProductSubarray();
            var result = maximumProductSubarray.MinMax(array);
            result.Should().Be(expected);
        }

        public class MaximumProductSubarrayTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new int[] { 2,3,-2,4 },
                    6
                };
                yield return new object[] {
                    new int[] { -2,0,-1 },
                    0
                };
                yield return new object[] {
                    new int[] { 0, 0, 0, -1 },
                    0
                };
                yield return new object[] {
                    new int[] { -3, -1, -1 },
                    3
                };
                yield return new object[] {
                    new int[] { 2,-5,-2,-4,3 },
                    24
                };
                yield return new object[] {
                    new int[] { 2, 0, 5, 3, 0, -2, 0, -4 },
                    15
                };
                yield return new object[] {
                    new int[] { 5, 3, -1, 6, 8 },
                    48
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}

