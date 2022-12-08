using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0006_ZigzagConversionTests
    {

        [Theory]
        [ClassData(typeof(ZigzagConversionTestData))]
        public void Convert(string s, int numRows, string expectedResult)
        {
            var zigzagConversion = new ZigzagConversion();
            var result = zigzagConversion.Convert(s, numRows);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }

    public class ZigzagConversionTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "",
                5,
                ""
            };
            yield return new object[] {
                null,
                123,
                ""
            };
            yield return new object[] {
                "a",
                123,
                "a"
            };
            yield return new object[] {
                "ab",
                123,
                "ab"
            };
            yield return new object[] {
                "abc",
                2,
                "acb"
            };
            yield return new object[] {
                "abcd",
                1,
                "abcd"
            };
            yield return new object[] {
                "abcd",
                2,
                "acbd"
            };
            yield return new object[] {
                "abcd",
                3,
                "abdc"
            };
            yield return new object[] {
                "PAYPALISHIRING",
                3,
                "PAHNAPLSIIGYIR"
            };
            yield return new object[] {
                "PAYPALISHIRING",
                4,
                "PINALSIGYAHRPI"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}