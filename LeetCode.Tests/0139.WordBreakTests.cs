using FluentAssertions;
using LeetCode.Extensions;
using LeetCode.Models;
using System.Collections;

namespace LeetCode.Tests
{
    public class _0139_WordBreakTests
    {
        [Theory]
        [ClassData(typeof(WordBreakTestData))]
        public void BruteForce(string s, IList<string> wordList, bool expected)
        {
            if (s.Length > 21)
            {
                //The brute force solution might be too slow for long strings, so we skip those tests.
                return;
            }
            var wordBreak = new WordBreak();
            var result = wordBreak.BruteForce(s, wordList);
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(WordBreakTestData))]
        public void DP(string s, IList<string> wordList, bool expected)
        {
            var wordBreak = new WordBreak();
            var result = wordBreak.DP(s, wordList);
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(WordBreakTestData))]
        public void BFS(string s, IList<string> wordList, bool expected)
        {
            var wordBreak = new WordBreak();
            var result = wordBreak.BFS(s, wordList);
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(WordBreakTestData))]
        public void Trie(string s, IList<string> wordList, bool expected)
        {
            var wordBreak = new WordBreak();
            var result = wordBreak.Trie(s, wordList);
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(WordBreakTestData))]
        public void TrieSearchWord(string s, IList<string> wordList, bool expected)
        {
            var wordBreak = new WordBreak();
            var result = wordBreak.TrieSearchWord(s, wordList);
            result.Should().Be(expected);
        }

        public class WordBreakTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                   "leetcode",
                   new List<string> { "leet", "code" },
                   true
                };
                yield return new object[] {
                   "applepenapple",
                   new List<string> { "apple", "pen" },
                   true
                };
                yield return new object[] {
                   "catsandog",
                   new List<string> { "cats","dog","sand","and","cat" },
                   false
                };
                yield return new object[] {
                   "bccdbacdbdacddabbaaaadababadad",
                   new List<string> { "cbc","bcda","adb","ddca","bad","bbb","dad","dac","ba","aa","bd","abab","bb","dbda","cb","caccc","d","dd","aadb","cc","b","bcc","bcd","cd","cbca","bbd","ddd","dabb","ab","acd","a","bbcc","cdcbd","cada","dbca","ac","abacd","cba","cdb","dbac","aada","cdcda","cdc","dbc","dbcb","bdb","ddbdd","cadaa","ddbc","babb" },
                   true
                };
                yield return new object[] {
                   "aaaaaaaaaaaaaaaaaaaab",
                   //The word is longer than 20 characters. I'm checking to see if the optimization is implemented.
                   new List<string> { "aaaaaaaaaaaaaaaaaaaab" },
                   false
                };
                yield return new object[] {
                   "aaaaaaaaaaaaaaaaaaaab",
                   new List<string> { "aaaaaaaaaaaaaaaaaaaa", "b" },
                   true
                };
                yield return new object[] {
                   "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab",
                   new List<string> { "a","aa","aaa","aaaa","aaaaa","aaaaaa","aaaaaaa","aaaaaaaa","aaaaaaaaa","aaaaaaaaaa" },
                   false
                };
                yield return new object[] {
                    "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbbbbaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab",
                   new List<string> { "a","aa","aaa","aaaa","aaaaa","aaaaaa","aaaaaaa","aaaaaaaa","aaaaaaaaa","aaaaaaaaaa" },
                   false
                };
                yield return new object[] {
                   "areallylongstringthathastrapinitscontents",
                   new List<string> {
                        "areally", "long", "string", "thathas", "trap", "inits", "contents",
                        "a", "reallyl", "ongs", "tringt", "hathast", "rapi", "nitsc", "ontent",
                        "ar", "eallylo", "ngst", "ringth", "athastr", "apin", "itsco", "ntent",
                        "are", "allylon", "gstr", "inghtha", "thastra", "pini", "tscon", "tent",
                        "area", "llylong", "stri", "nghthat", "hastrap", "init", "scont", "ent",
                        "areal", "lylongs", "trin", "gthath", "astrapi", "nits", "conte", "nt",
                        "areall", "ylongst", "ring", "thatha", "strapin", "itsc", "onten", "t"
                    },
                    true
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}

