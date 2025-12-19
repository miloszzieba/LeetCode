using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0153_MinimumInRotatedSortedArrayTests
    {
        [Theory]
        [ClassData(typeof(MinimumInRotatedSortedArrayTestData))]
        public void BinarySearch(int[] array, int expected)
        {
            var minimumInRotatedSortedArray = new MinimumInRotatedSortedArray();
            var result = minimumInRotatedSortedArray.BinarySearch(array);
            result.Should().Be(expected);
        }

        public class MinimumInRotatedSortedArrayTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new int[] { 3,4,5,1,2 },
                    1
                };
                yield return new object[] {
                    new int[] { 4,5,6,7,0,1,2 },
                    0
                };
                yield return new object[] {
                    new int[] { 11,13,15,17 },
                    11
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}

