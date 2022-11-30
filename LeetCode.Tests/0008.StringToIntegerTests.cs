using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class StringToIntegerTests
    {

        [Theory]
        [ClassData(typeof(StringToIntegerTestData))]
        public void Convert(string s, int expectedResult)
        {
            var stringToInteger = new StringToInteger();
            var result = stringToInteger.MyAtoi(s);
            result.Should().Be(expectedResult);
        }
    }

    public class StringToIntegerTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "",
                0
            };
            yield return new object[] {
                null,
                0
            };
            yield return new object[] {
               "d",
               0
            };
            yield return new object[] {
               "    ",
               0
            };
            yield return new object[] {
               "    1",
               1
            };
            yield return new object[] {
               "    -1ddd",
               -1
            };
            yield return new object[] {
               "+-12",
               0
            };
            yield return new object[] {
               "00000-42a1234",
               0
            };
            yield return new object[] {
                "   " + int.MaxValue.ToString() + "ddd",
                int.MaxValue
            };
            yield return new object[] {
                "   " + int.MinValue.ToString() + "ddd",
                int.MinValue
            };
            yield return new object[] {
                "   2" + int.MaxValue.ToString() + "ddd",
                int.MaxValue
            };
            yield return new object[] {
                "   -2" + int.MaxValue.ToString() + "ddd",
                int.MinValue
            };
            yield return new object[] {
                "2147483648",
                int.MaxValue
            };
            yield return new object[] {
                "-2147483649",
                int.MinValue
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}