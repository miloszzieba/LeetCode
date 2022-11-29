using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class ReverseIntegerTests
    {

        [Theory]
        [ClassData(typeof(ReverseIntegerTestData))]
        public void Convert(int x, int expectedResult)
        {
            var reverseInteger = new ReverseInteger();
            var result = reverseInteger.Reverse(x);
            result.Should().Be(expectedResult);
        }
    }

    public class ReverseIntegerTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                0,
                0
            };
            yield return new object[] {
                -1,
                -1
            };
            yield return new object[] {
               123,
               321
            };
            yield return new object[] {
               -123,
               -321
            };
            yield return new object[] {
               -1234,
               -4321
            };
            yield return new object[] {
                1111111119,
                0
            };
            yield return new object[] {
                -2147483648,
                0
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}