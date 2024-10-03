using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0020_ValidParenthesesTests
    {
        [Theory]
        [ClassData(typeof(ValidParenthesesTestData))]
        public void Solution(string s, bool expected)
        {
            var ValidParentheses = new ValidParentheses();
            var result = ValidParentheses.IsValid(s);

            Assert.Equal(result, expected);
        }
    }

    public class ValidParenthesesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "",
                true
            };
            yield return new object[] {
                "()",
                true
            };
            yield return new object[] {
                "(",
                false
            };
            yield return new object[] {
                "()[]{}",
                true
            };
            yield return new object[] {
                "(]",
                false
            };
            yield return new object[] {
                "([])",
                true
            };
            yield return new object[]
            {
                "(){}}{",
                false
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

