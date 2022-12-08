using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0011_ContainerWithMostWaterTests
    {
        [Theory]
        [ClassData(typeof(ContainerWithMostWaterTestData))]
        public void MaxAreaBruteForce(int[] height, int expectedResult)
        {
            var containerWithMostWater = new ContainerWithMostWater();
            var result = containerWithMostWater.MaxAreaBruteForce(height);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [ClassData(typeof(ContainerWithMostWaterTestData))]
        public void MaxAreaFromBothSides(int[] height, int expectedResult)
        {
            var containerWithMostWater = new ContainerWithMostWater();
            var result = containerWithMostWater.MaxAreaFromBothSides(height);
            result.Should().Be(expectedResult);
        }
    }

    public class ContainerWithMostWaterTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 },
                49
            };
            yield return new object[] {
                new int[] { 1, 1 },
                1
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}