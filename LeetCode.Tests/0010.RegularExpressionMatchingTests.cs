using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0010_RegularExpressionMatchingTests
    {

        [Theory]
        [ClassData(typeof(RegularExpressionMatchingTestData))]
        public void IsMatchRecursive(string s, string p, bool expectedResult)
        {
            var regularExpressionMatching = new RegularExpressionMatching();
            var result = regularExpressionMatching.IsMatchRecursive(s, p);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [ClassData(typeof(RegularExpressionMatchingTestData))]
        public void IsMatchDynamic(string s, string p, bool expectedResult)
        {
            var regularExpressionMatching = new RegularExpressionMatching();
            var result = regularExpressionMatching.IsMatchDynamic(s, p);
            result.Should().Be(expectedResult);
        }
    }

    public class RegularExpressionMatchingTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                "a",
                "a",
                true
            };
            yield return new object[] {
                "a",
                "b",
                false
            };
            yield return new object[] {
                "aa",
                "aa",
                true
            };
            yield return new object[] {
                "ab",
                "a",
                false
            };
            yield return new object[] {
                "ab",
                "b",
                false
            };
            yield return new object[] {
                "ab",
                "..",
                true
            };
            yield return new object[] {
                "b",
                "b*b",
                true
            };
            yield return new object[] {
                "",
                "b*b*",
                true
            };
            yield return new object[] {
                "b",
                "b*b*b",
                true
            };
            yield return new object[] {
                "b",
                ".*b*b",
                true
            };
            yield return new object[] {
                "asfdjhasgdfb",
                ".*b*b",
                true
            };
            yield return new object[] {
                "asdasfsadfasdfasdfasdfsadfasdfbbbbbbbbbbbaaa",
                "asdasfsadfasdfasdf.*bb*aaaa*",
                true
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}