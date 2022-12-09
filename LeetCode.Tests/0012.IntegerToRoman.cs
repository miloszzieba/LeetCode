using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0012_IntegerToRoman
    {
        [Theory]
        [ClassData(typeof(IntegerToRomanTestData))]
        public void IntToRoman(int num, string expectedResult)
        {
            var integerToRoman = new IntegerToRoman();
            var result = integerToRoman.IntToRoman(num);
            result.Should().Be(expectedResult);
        }
    }

    public class IntegerToRomanTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                1,
                "I"
            };
            yield return new object[] {
                4,
                "IV"
            };
            yield return new object[] {
                3949,
                "MMMCMXLIX"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}