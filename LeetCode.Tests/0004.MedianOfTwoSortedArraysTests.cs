using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class MedianOfTwoSortedArraysTests
    {

        [Theory]
        [ClassData(typeof(MedianOfTwoSortedArraysTestData))]
        public void FromTheMiddle(int[] nums1, int[] nums2, double expectedResult)
        {
            var medianOfTwoSortedArrays = new MedianOfTwoSortedArrays();
            var result = medianOfTwoSortedArrays.FromTheMiddle(nums1, nums2);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [ClassData(typeof(MedianOfTwoSortedArraysTestData))]
        public void BinarySearch(int[] nums1, int[] nums2, double expectedResult)
        {
            var medianOfTwoSortedArrays = new MedianOfTwoSortedArrays();
            var result = medianOfTwoSortedArrays.BinarySearch(nums1, nums2);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [ClassData(typeof(MedianOfTwoSortedArraysTestData))]
        public void MergeSort(int[] nums1, int[] nums2, double expectedResult)
        {
            var medianOfTwoSortedArrays = new MedianOfTwoSortedArrays();
            var result = medianOfTwoSortedArrays.MergeSort(nums1, nums2);
            result.Should().Be(expectedResult);
        }
    }

    public class MedianOfTwoSortedArraysTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new [] { 1, 3 },
                new [] { 2 },
                2d
            };
            yield return new object[] {
                new [] { 3, 6, 9, 12, 15 },
                new [] { 2, 4, 6, 8, 10},
                7d
            };
            yield return new object[] {
                new [] { 1, 2 },
                new [] { 3, 4},
                2.5d
            };
            yield return new object[] {
                new [] { 1 },
                new [] { 1 },
                1d
            };
            yield return new object[] {
                new [] { 1 },
                new [] { 2, 3, 4 },
                2.5d
            };
            yield return new object[] {
                new [] { 2, 3, 4 },
                new [] { 1 },
                2.5d
            };
            yield return new object[] {
                new [] { 2, 3, 5 },
                new [] { 4 },
                3.5d
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}