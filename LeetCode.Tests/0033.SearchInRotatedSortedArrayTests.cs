using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0033_SearchInRotatedSortedArrayTests
    {
        [Theory]
        [ClassData(typeof(SearchInRotatedSortedArrayTestData))]
        public void BinarySearch(int[] array, int target, int expected)
        {
            var searchInRotatedSortedArray = new SearchInRotatedSortedArray();
            var result = searchInRotatedSortedArray.BinarySearch(array, target);
            result.Should().Be(expected);
        }

        public class SearchInRotatedSortedArrayTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new int[] { 1 },
                    1,
                    0
                };
                yield return new object[] {
                    new int[] { 1, 3 },
                    2,
                    -1
                };
                yield return new object[] {
                    new int[] { 3, 1 },
                    3,
                    0
                };
                yield return new object[] {
                    new int[] { 1, 2 },
                    1,
                    0
                };
                yield return new object[] {
                    new int[] { 1, 3 },
                    3,
                    1
                };
                yield return new object[] {
                    new int[] { 4,5,6,7,0,1,2 },
                    0,
                    4
                };
                yield return new object[] {
                    new int[] { 4,5,6,7,0,1,2 },
                    3,
                    -1
                };
                yield return new object[] {
                    new int[] { 1 },
                    0,
                    -1
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}

