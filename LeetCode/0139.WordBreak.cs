using BenchmarkDotNet.Attributes;
using Iced.Intel;
using LeetCode.Extensions;
using LeetCode.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    //BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
    //Intel Core i9-9900K CPU 3.60GHz(Coffee Lake), 1 CPU, 16 logical and 8 physical cores
    //.NET SDK 10.0.101
    //  [Host] : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3

    //Job = InProcess  Toolchain=InProcessEmitToolchain

    //// * Summary for bccdbacdbdacddabbaaaadababadad *

    //| Method             | Mean     | Error     | StdDev    | Allocated |
    //|------------------- |---------:|----------:|----------:|----------:|
    //| BruteForce         | 1.163 us | 0.0220 us | 0.0206 us |   2.03 KB |
    //| DP                 | 1.301 us | 0.0083 us | 0.0073 us |   2.16 KB |
    //| BFS                | 1.656 us | 0.0316 us | 0.0376 us |   3.52 KB |
    //| Trie               | 4.025 us | 0.0777 us | 0.1114 us |  14.36 KB |
    //| TrieInitialization | 3.484 us | 0.0645 us | 0.0768 us |   14.3 KB |

    //// * Summary for bccdbacdbdacddabbaaaadababadad x40 *

    //| Method             | Mean      | Error     | StdDev    | Allocated |
    //|------------------- |----------:|----------:|----------:|----------:|
    //| BruteForce         | 28.762 us | 0.5617 us | 0.7304 us |  30.37 KB |
    //| DP                 | 26.386 us | 0.2133 us | 0.1891 us |  36.83 KB |
    //| BFS                | 36.681 us | 0.7044 us | 0.5882 us |   77.4 KB |
    //| Trie               | 31.715 us | 0.2835 us | 0.2652 us |  15.51 KB |
    //| TrieInitialization |  3.488 us | 0.0545 us | 0.0483 us |   14.3 KB |

    //// * Summary for bccdbacdbdacddabbaaaadababadad x40 
    ////    after switching to HashSet<string>.AlternateLookup<ReadOnlySpan<char>> *

    //| Method             | Mean      | Error     | StdDev    | Allocated |
    //|------------------- |----------:|----------:|----------:|----------:|
    //| BruteForce         | 12.760 us | 0.0482 us | 0.0451 us |    1.3 KB |
    //| DP                 | 11.226 us | 0.0627 us | 0.0555 us |   2.51 KB |
    //| BFS                | 16.297 us | 0.0526 us | 0.0466 us |    6.7 KB |
    //| Trie               | 33.809 us | 0.1716 us | 0.1605 us |  15.51 KB |
    //| TrieInitialization |  3.261 us | 0.0346 us | 0.0270 us |   14.3 KB |

    //// * Summary for 150a's and b *

    //| Method             | Mean        | Error    | StdDev   | Allocated |
    //|------------------- |------------:|---------:|---------:|----------:|
    //| DP                 |  3,669.8 ns | 26.70 ns | 23.67 ns |   4.41 KB |
    //| BFS                | 31,655.1 ns | 89.94 ns | 70.22 ns |  59.68 KB |
    //| Trie               | 13,694.7 ns | 43.02 ns | 38.14 ns |   2.74 KB |
    //| TrieInitialization |    831.0 ns |  3.71 ns |  2.90 ns |   2.57 KB |

    //// * Summary for 150a's and b
    ////   after switching to HashSet<string>.AlternateLookup<ReadOnlySpan<char>> *

    //| Method             | Mean        | Error    | StdDev   | Allocated |
    //|------------------- |------------:|---------:|---------:|----------:|
    //| DP                 |  1,700.8 ns |  5.21 ns |  4.35 ns |     552 B |
    //| BFS                | 14,635.9 ns | 53.05 ns | 49.62 ns |    2784 B |
    //| Trie               | 13,738.4 ns | 44.03 ns | 41.18 ns |    2808 B |
    //| TrieInitialization |    801.5 ns |  6.20 ns |  5.80 ns |    2632 B |

    //// * Summary for 150a's and b and single 20 chars word in wordDict *

    //| Method             | Mean       | Error    | StdDev   | Allocated |
    //|------------------- |-----------:|---------:|---------:|----------:|
    //| BruteForce         | 2,115.8 ns | 32.99 ns | 30.86 ns |    6.9 KB |
    //| DP                 | 3,875.1 ns | 24.71 ns | 23.12 ns |   7.11 KB |
    //| BFS                | 2,214.6 ns | 26.49 ns | 23.48 ns |   7.59 KB |
    //| Trie               | 2,188.8 ns | 31.02 ns | 29.01 ns |   5.16 KB |
    //| TrieInitialization |   672.9 ns | 13.38 ns | 23.09 ns |   4.99 KB |

    //// * Summary for areallylongstringthathastrapinitscontents *

    //| Method             | Mean      | Error     | StdDev    | Allocated |
    //|------------------- |----------:|----------:|----------:|----------:|
    //| BruteForce         |  6.459 us | 0.1099 us | 0.1266 us |  14.72 KB |
    //| DP                 |  3.206 us | 0.0622 us | 0.0851 us |   6.02 KB |
    //| BFS                |  3.762 us | 0.0507 us | 0.0423 us |   8.11 KB |
    //| Trie               | 10.826 us | 0.2140 us | 0.3070 us |  41.59 KB |
    //| TrieInitialization |  8.660 us | 0.1677 us | 0.2239 us |  41.52 KB |

    //// * Summary for areallylongstringthathastrapinitscontents x40 *

    //| Method             | Mean       | Error     | StdDev    | Allocated  |
    //|------------------- |-----------:|----------:|----------:|-----------:|
    //| BruteForce         | 698.814 us | 6.9053 us | 6.4592 us | 1990.31 KB |
    //| DP                 |  91.789 us | 0.3642 us | 0.3229 us |  188.56 KB |
    //| BFS                | 122.521 us | 0.9994 us | 0.8345 us |  304.66 KB |
    //| Trie               |  94.839 us | 0.1056 us | 0.0882 us |   43.16 KB |
    //| TrieInitialization |   8.344 us | 0.0655 us | 0.0580 us |   41.52 KB |

    //// * Summary for areallylongstringthathastrapinitscontents x40
    ////   after switching to HashSet<string>.AlternateLookup<ReadOnlySpan<char>> *

    //| Method             | Mean      | Error     | StdDev    | Allocated |
    //|------------------- |----------:|----------:|----------:|----------:|
    //| BruteForce         | 48.597 us | 0.2693 us | 0.2387 us |    1.3 KB |
    //| DP                 | 44.962 us | 0.2517 us | 0.2231 us |   2.94 KB |
    //| BFS                | 31.403 us | 0.1454 us | 0.1360 us |   7.13 KB |
    //| Trie               | 94.519 us | 0.3423 us | 0.3202 us |  43.16 KB |
    //| TrieInitialization |  8.477 us | 0.0838 us | 0.0784 us |  41.52 KB |

    //// * Summary for areallylongstringthathastrapinitscontents x40 with only one path *

    //| Method             | Mean      | Error     | StdDev    | Allocated |
    //|------------------- |----------:|----------:|----------:|----------:|
    //| DP                 | 38.357 us | 0.7649 us | 0.7155 us |   74.5 KB |
    //| BFS                | 36.258 us | 0.7248 us | 0.8347 us |  85.52 KB |
    //| Trie               | 16.822 us | 0.1519 us | 0.1421 us |   10.3 KB |
    //| TrieInitialization |  1.466 us | 0.0261 us | 0.0245 us |   8.66 KB |

    //// * Summary for areallylongstringthathastrapinitscontents x40 with only one path
    ////   after switching to HashSet<string>.AlternateLookup<ReadOnlySpan<char>> *

    //| Method             | Mean      | Error     | StdDev    | Allocated |
    //|------------------- |----------:|----------:|----------:|----------:|
    //| BruteForce         | 10.976 us | 0.0941 us | 0.0881 us |     376 B |
    //| DP                 | 20.177 us | 0.1405 us | 0.1314 us |    2048 B |
    //| BFS                | 14.378 us | 0.0697 us | 0.0652 us |    2112 B |
    //| Trie               | 17.528 us | 0.1137 us | 0.1063 us |   10544 B |
    //| TrieInitialization |  1.400 us | 0.0256 us | 0.0240 us |    8872 B |

    //// * Summary for areallylongstringthathastrapinitscontents when I remove successful path *

    //| Method             | Mean      | Error     | StdDev    | Allocated |
    //|------------------- |----------:|----------:|----------:|----------:|
    //| BruteForce         | 59.519 us | 0.5071 us | 0.4744 us | 140.29 KB |
    //| DP                 |  3.128 us | 0.0418 us | 0.0370 us |   6.09 KB |
    //| BFS                |  5.210 us | 0.0859 us | 0.0804 us |  12.73 KB |
    //| Trie               | 10.798 us | 0.2103 us | 0.2807 us |  41.35 KB |
    //| TrieInitialization |  8.990 us | 0.1752 us | 0.2513 us |  41.28 KB |

    //// * Summary for areallylongstringthathastrapinitscontents when I remove successful path
    ////   after switching to HashSet<string>.AlternateLookup<ReadOnlySpan<char>> *

    //| Method             | Mean      | Error     | StdDev    | Allocated |
    //|------------------- |----------:|----------:|----------:|----------:|
    //| BruteForce         | 27.800 us | 0.1245 us | 0.1164 us |    1.3 KB |
    //| DP                 |  1.919 us | 0.0061 us | 0.0054 us |   1.38 KB |
    //| BFS                |  2.886 us | 0.0111 us | 0.0104 us |   1.59 KB |
    //| Trie               | 10.081 us | 0.0727 us | 0.0644 us |  41.35 KB |
    //| TrieInitialization |  7.970 us | 0.0394 us | 0.0307 us |  41.28 KB |

    //// * Summary for areallylongstringthathastrapinitscontent x20 when I remove successful path at the last element only *

    //|------------------- |----------:|----------:|----------:|----------:|
    //| DP                 | 45.545 us | 0.1632 us | 0.1446 us |  92.19 KB |
    //| BFS                | 94.702 us | 1.4968 us | 2.1941 us | 211.22 KB |
    //| Trie               | 49.681 us | 0.1526 us | 0.1353 us |  42.09 KB |
    //| TrieInitialization |  8.238 us | 0.0838 us | 0.0743 us |  41.28 KB |

    //// * Summary for areallylongstringthathastrapinitscontent x20 when I remove successful path at the last element only
    ////   after switching to HashSet<string>.AlternateLookup<ReadOnlySpan<char>> *

    //| Method             | Mean      | Error     | StdDev    | Allocated |
    //|------------------- |----------:|----------:|----------:|----------:|
    //| DP                 | 23.121 us | 0.1490 us | 0.1394 us |   2.12 KB |
    //| BFS               | 41.152 us | 0.1832 us | 0.1713 us |    4.3 KB |
    //| Trie               | 50.435 us | 0.2024 us | 0.1893 us |  42.09 KB |
    //| TrieInitialization |  8.254 us | 0.0830 us | 0.0736 us |  41.28 KB |

    [InProcess]
    [MemoryDiagnoser(false)]
    public class WordBreak
    {
        private const int MAX_WORD_LENGTH = 20;
        private string _s;
        private List<string> _wordDict;

        [GlobalSetup]
        public void WordBreakSetup()
        {
            //I should write some wordList generation and s generation algorithm

            //this._s = "bccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadadbccdbacdbdacddabbaaaadababadad";
            //this._wordDict = new List<string> { "cbc", "bcda", "adb", "ddca", "bad", "bbb", "dad", "dac", "ba", "aa", "bd", "abab", "bb", "dbda", "cb", "caccc", "d", "dd", "aadb", "cc", "b", "bcc", "bcd", "cd", "cbca", "bbd", "ddd", "dabb", "ab", "acd", "a", "bbcc", "cdcbd", "cada", "dbca", "ac", "abacd", "cba", "cdb", "dbac", "aada", "cdcda", "cdc", "dbc", "dbcb", "bdb", "ddbdd", "cadaa", "ddbc", "babb" };

            //this._s = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            //this._wordDict = new List<string> { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa", "aaaaaaaa", "aaaaaaaaa", "aaaaaaaaaa" };

            //this._s = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            //this._wordDict = new List<string> { "aaaaaaaaaaaaaaaaaaaa" };

            this._s = "areallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontentareallylongstringthathastrapinitscontents";
            this._wordDict = new List<string> {
                "areally", "long", "string", "thathas", "trap", "inits", "content",
                "a", "reallyl", "ongs", "tringt", "hathast", "rapi", "nitsc", "ontent",
                "ar", "eallylo", "ngst", "ringth", "athastr", "apin", "itsco", "ntent",
                "are", "allylon", "gstr", "inghtha", "thastra", "pini", "tscon", "tent",
                "area", "llylong", "stri", "nghthat", "hastrap", "init", "scont", "ent",
                "areal", "lylongs", "trin", "gthath", "astrapi", "nits", "conte", "nt",
                "areall", "ylongst", "ring", "thatha", "strapin", "itsc", "onten", "t"
            };
        }

        [Benchmark]
        public bool BruteForce() => BruteForce(this._s, this._wordDict);
        [Benchmark]
        public bool DP() => DP(this._s, this._wordDict);
        [Benchmark]
        public bool BFS() => BFS(this._s, this._wordDict);
        [Benchmark]
        public bool Trie() => Trie(this._s, this._wordDict);
        [Benchmark]
        public bool TrieSearchWord() => TrieSearchWord(this._s, this._wordDict);
        [Benchmark]
        public bool TrieInitialization() => TrieInitialization(this._wordDict);


        public bool BruteForce(string s, IList<string> wordDict)
        {
            var source = s.AsSpan();
            var hashSet = wordDict.ToHashSet();
            var lookup = hashSet.GetAlternateLookup<ReadOnlySpan<char>>();
            return CanBreak(source, 0, lookup);
        }

        private bool CanBreak(ReadOnlySpan<char> source, int v, HashSet<string>.AlternateLookup<ReadOnlySpan<char>> lookup)
        {
            if (v == source.Length)
                return true;

            for (int i = v + 1; i <= source.Length; i++)
            {
                if (i - v > MAX_WORD_LENGTH)
                    return false;
                if (lookup.Contains(source[v..i]) && CanBreak(source, i, lookup))
                    return true;
            }
            return false;
        }

        public bool DP(string s, IList<string> wordDict)
        {
            var source = s.AsSpan();
            var hashSet = wordDict.ToHashSet();
            var lookup = hashSet.GetAlternateLookup<ReadOnlySpan<char>>();
            var dp = new bool[source.Length + 1];
            dp[0] = true;
            var maxWordLength = Math.Min(MAX_WORD_LENGTH, wordDict.Max(w => w.Length));
            for (int i = 1; i <= source.Length; i++)
                for (int j = i - 1; j >= Math.Max(0, i - maxWordLength); j--)
                    if (dp[j] && lookup.Contains(source[j..i]))
                    {
                        dp[i] = true;
                        break;
                    }

            return dp[s.Length];
        }

        public bool BFS(string s, IList<string> wordDict)
        {
            var source = s.AsSpan();
            var hashSet = wordDict.ToHashSet();
            var lookup = hashSet.GetAlternateLookup<ReadOnlySpan<char>>();

            var maxWordLength = Math.Min(MAX_WORD_LENGTH, wordDict.Max(w => w.Length));

            var visited = new bool[source.Length];
            var stack = new Stack<int>();
            stack.Push(0);

            while (stack.Any())
            {
                var i = stack.Pop();
                for (int j = 1; j <= maxWordLength && i + j <= s.Length; j++)
                {
                    if (lookup.Contains(source[i..(i + j)]))
                        if (i + j == source.Length)
                            return true;
                        else if (!visited[i + j])
                            stack.Push(i + j);

                }
                visited[i] = true;
            }

            return false;
        }

        public bool Trie(string s, IList<string> wordDict)
        {
            var source = s.AsSpan();
            var tree = new Trie();
            foreach (var word in wordDict)
                tree.AddWord(word);
            var dp = new bool[s.Length + 1];
            dp[0] = true;

            for (int i = 0; i < source.Length; i++)
            {
                if (!dp[i])
                    continue;

                var currentNode = tree.Root;
                for (int j = i; j < source.Length && j - i < MAX_WORD_LENGTH && currentNode.Children.ContainsKey(source[j]); j++)
                {
                    currentNode = currentNode.Children[s[j]];
                    if (currentNode.IsEnd)
                        dp[j + 1] = true;
                }
            }

            return dp[s.Length];
        }

        public bool TrieSearchWord(string s, IList<string> wordDict)
        {
            var source = s.AsSpan();
            var tree = new Trie();
            foreach (var word in wordDict)
                tree.AddWord(word);
            var dp = new bool[source.Length];
            var maxWordLength = Math.Min(MAX_WORD_LENGTH, wordDict.Max(w => w.Length));

            for (int i = 0; i < source.Length; i++)
                for (int j = Math.Max(0, i - maxWordLength + 1); j < i + 1; j++)
                    if (tree.Search(source[j..(i + 1)]))
                    {
                        dp[i] = (j > 0) ? dp[j - 1] : true;
                        if (dp[i]) break;
                    }

            return dp[s.Length - 1];
        }

        private bool TrieInitialization(IList<string> wordDict)
        {
            var tree = new Trie();
            foreach (var word in wordDict)
                tree.AddWord(word);
            return tree.Root != null;
        }
    }
}
