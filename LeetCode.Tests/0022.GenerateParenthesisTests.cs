using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0022_GenerateParenthesisTests
    {
        [Theory]
        [ClassData(typeof(GenerateParenthesisTestData))]
        public void Solution(int count, IList<string> expected)
        {
            var generateParenthesis = new GenerateParenthesis();
            var result = generateParenthesis.Solution(count);
            result.Should().BeEquivalentTo(expected);
        }
    }

    public class GenerateParenthesisTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                1,
                new List<string>() {
                    "()"
                }
            };
            yield return new object[] {
                2,
                new List < string >() {
                    "()()",
                    "(())"
                }
            };
            yield return new object[] {
                3,
                new List < string >() {
                    "()()()",
                    "(())()",
                    "()(())",
                    "((()))"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

