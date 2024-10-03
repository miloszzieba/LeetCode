using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class Extra_StringInputToNumberWithCommas
    {

        [Theory]
        [ClassData(typeof(StringInputToNumberWithCommasTestData))]
        public void Execute(
            string oldInput, 
            string newInput, 
            int selectorEnd,
            string expectedResult,
            int expectedSelector)
        {
            var stringInputToNumberWithCommas = new StringInputToNumberWithCommas();
            var result = stringInputToNumberWithCommas.Execute(oldInput, newInput, selectorEnd);
            result.Result.Should().Be(expectedResult);
            result.Selector.Should().Be(expectedSelector);
        }
    }

    public class StringInputToNumberWithCommasTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "",
                "a",
                1,
                "",
                0
            };
            yield return new object[] {
                "",
                "1",
                1,
                "1",
                1
            };
            yield return new object[] {
                "",
                "11",
                2,
                "11",
                2
            };
            yield return new object[] {
                "",
                "1d1",
                3,
                "11",
                2
            };
            yield return new object[] {
                "123",
                "1234",
                4,
                "1,234",
                5
            };
            yield return new object[] {
                "1,234",
                "1234",
                1,
                "1,234",
                1
            };
            yield return new object[] {
                "1,234",
                "1,34",
                2,
                "134", 
                1
            };
            yield return new object[] {
                "1,234",
                "134",
                1,
                "134", 
                1
            };
            yield return new object[] {
                "1,234,567",
                "167",
                1,
                "167",
                1
            };
            yield return new object[] {
                "1,234,567,890",
                "1,2390",
                3,
                "12,390",
                3
            };
            yield return new object[] {
                "1,222,333,444",
                "1,222444",
                5,
                "1,222,444",
                6
            };
            yield return new object[] {
                "1,222,333,444",
                "1,22244",
                5,
                "122,244",
                5
            };
            yield return new object[] {
                "1,222,333,444",
                "1,222d,333,444",
                6,
                "1,222,333,444",
                5
            };
            yield return new object[] {
                "1,222,333,444",
                "1,222dd,333,444",
                7,
                "1,222,333,444",
                5
            };
            yield return new object[] {
                "1,222,333,444",
                "1,dd,333,444",
                4,
                "1,333,444",
                1
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}