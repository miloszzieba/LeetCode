//using FluentAssertions;
//using LeetCode.Extensions;
//using LeetCode.Models;
//using System.Collections;

//namespace LeetCode.Tests
//{
//    public class _0018_FourSumTests
//    {
//        [Theory]
//        [ClassData(typeof(FourSumTestData))]
//        public void BruteForce(int[] nums, int target, List<List<int>> expected)
//        {
//            var fourSum = new FourSum();
//            var result = fourSum.BruteForce(nums, target);

//            result.Count().Should().Be(expected.Count);
//            if (!result.All(x => expected.Any(y => y.All(z => x.Contains(z)))))
//                Assert.False(true);
//        }
//    }

//    public class FourSumTestData : IEnumerable<object[]>
//    {
//        public IEnumerator<object[]> GetEnumerator()
//        {
//            yield return new object[] {
//                new int[] { 1, 0, -1, 0, -2, 2 },
//                0,
//                new List<List<int>>()
//                {
//                    new List<int> { -2, 2, 0, 0 },
//                    new List<int> { -2, 2, -1, 1 },
//                    new List<int> { 0, 0, -1, 1 },
//            };
//            yield return new object[] {
//                new int[] { 0, 0, 0, 0 },
//                new List<List<int>>()
//                {
//                    new List<int>() { 0, 0, 0, 0 }
//                }
//            };
//            yield return new object[] {
//                new int[] { 0, 0, 0, 0, 0 },
//                0,
//                new List<List<int>>()
//                {
//                    new List<int>() { 0, 0, 0, 0 }
//                }
//            };
//            yield return new object[] {
//                new int[] { 1_000_000_000, 1_000_000_000, 1_000_000_000, 1_000_000_000 },
//                -294_967_296,
//                new List<List<int>>()
//            };
//        }

//        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
//    }
//}

