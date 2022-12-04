using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class PalindromeNumberTests
    {

        [Theory]
        [ClassData(typeof(PalindromeNumberTestData))]
        public void IsPalindrome(int x, bool expectedResult)
        {
            var palindromeNumber = new PalindromeNumber();
            var result = palindromeNumber.IsPalindrome(x);
            result.Should().Be(expectedResult);
        }
    }

    public class PalindromeNumberTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                -1,
                false
            };
            yield return new object[] {
                0,
                true
            };
            yield return new object[] {
               1,
               true
            };
            yield return new object[] {
               9,
               true
            };
            yield return new object[] {
               88,
               true
            };
            yield return new object[] {
               78,
               false
            };
            yield return new object[] {
               Int32.MinValue,
               false
            };
            yield return new object[] {
               Int32.MaxValue,
               false
            };
            yield return new object[] {
                2147447412,
                true
            };
            yield return new object[]
            {
                121,
                true
            };
            yield return new object[]
            {
                10,
                false
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}