using BenchmarkDotNet.Attributes;
using Iced.Intel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    //// * Summary for .NET 6*

    //BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.2251/21H1/May2021Update)
    //Intel Core i9-9900K CPU 3.60GHz(Coffee Lake), 1 CPU, 16 logical and 8 physical cores
    //.NET SDK= 6.0.402
    //  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2
    //  DefaultJob : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2


    //|         Method |       Mean |    Error |   StdDev | Allocated |
    //|--------------- |-----------:|---------:|---------:|----------:|
    //| CharDictionary | 2,669.8 ns | 19.29 ns | 18.04 ns |    3400 B |
    //|      CharArray |   277.1 ns |  2.11 ns |  1.76 ns |     408 B |
    //|    CharHashSet | 3,159.6 ns | 19.01 ns | 16.85 ns |    1312 B |

    //// * Summary for .NET 8*

    //BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.6466/22H2/2022Update)
    //Intel Core i9-9900K CPU 3.60GHz(Coffee Lake), 1 CPU, 16 logical and 8 physical cores
    //.NET SDK 10.0.101
    //  [Host]     : .NET 8.0.22 (8.0.2225.52707), X64 RyuJIT AVX2
    //  DefaultJob : .NET 8.0.22 (8.0.2225.52707), X64 RyuJIT AVX2


    //| Method         | Mean       | Error   | StdDev  | Allocated |
    //|--------------- |-----------:|--------:|--------:|----------:|
    //| CharDictionary | 1,771.2 ns | 7.37 ns | 6.90 ns |    3400 B |
    //| CharArray      |   234.4 ns | 1.13 ns | 0.94 ns |     408 B |
    //| CharHashSet    | 2,560.4 ns | 9.20 ns | 7.69 ns |    1312 B |

    // * Summary for .NET 10*

    //BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
    //Intel Core i9-9900K CPU 3.60GHz(Coffee Lake), 1 CPU, 16 logical and 8 physical cores
    //.NET SDK 10.0.101
    //  [Host]     : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3
    //  DefaultJob : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3


    //| Method         | Mean       | Error   | StdDev  | Allocated |
    //|--------------- |-----------:|--------:|--------:|----------:|
    //| CharDictionary | 1,629.5 ns | 9.42 ns | 8.35 ns |    3400 B |
    //| CharArray      |   162.6 ns | 1.11 ns | 1.03 ns |         - |
    //| CharHashSet    | 2,272.4 ns | 7.95 ns | 7.43 ns |    1312 B |

    // * Summary for 95 capacity on Hashset and Dictionary*

    //BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
    //Intel Core i9-9900K CPU 3.60GHz(Coffee Lake), 1 CPU, 16 logical and 8 physical cores
    //.NET SDK 10.0.101
    //  [Host]     : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3
    //  DefaultJob : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3


    //| Method         | Mean       | Error    | StdDev  | Allocated |
    //|--------------- |-----------:|---------:|--------:|----------:|
    //| CharDictionary | 1,407.1 ns | 10.39 ns | 9.21 ns |    2272 B |
    //| CharArray      |   162.0 ns |  0.52 ns | 0.48 ns |         - |
    //| CharHashSet    | 1,707.7 ns |  8.02 ns | 7.50 ns |    1768 B |

    [MemoryDiagnoser(false)]
    public class LongestSubstringWithoutRepeatingCharacters
    {
        private string _s;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            this._s = "FCHI-u*Zo Z[ha<mZ6i>g@NU@mY1Q+Z,Z_ka-=@h^awL-U|**NkeK6Mn4q|J'#W=,P\"&QX8{K8qh/A.BN9Eu_|IBIh!s%01VbLl6~CeN3<@EYp{m[f8lm\"+HTfYQ}(]9 ytACJkTZ;r,\\pnJ=n ,*,N9R3q?L*XnTtbQ\"6zZ$sB8C14(~B|T(> sf~L)%^xf%odU 79q";
        }

        [Benchmark]
        public int CharDictionary() => CharDictionary(this._s);
        [Benchmark]
        public int CharArray() => CharArray(this._s);
        [Benchmark]
        public int CharHashSet() => CharHashSet(this._s);



        public int CharDictionary(string s)
        {
            if (s.Length == 0) return 0;

            var dict = new Dictionary<char, int>(95);
            var max = 0;
            for (int left = 0, right = 0; right < s.Length; right++)
            {
                if (dict.TryGetValue(s[right], out int value))
                    left = Math.Max(left, value + 1);
                dict[s[right]] = right;
                max = Math.Max(max, right - left + 1);
            }

            return max;
        }

        public int CharArray(string s)
        {
            if (s.Length == 0) return 0;

            var array = new int[95];
            Array.Fill(array, -1);
            var max = 0;
            for (int left = 0, right = 0; right < s.Length; right++)
            {
                if (array[s[right] - 32] >= left)
                    left = Math.Max(left, array[s[right] - 32] + 1);
                array[s[right] - 32] = right;
                max = Math.Max(max, right - left + 1);
            }

            return max;
        }

        public int CharHashSet(string s)
        {
            if (s.Length == 0) return 0;

            var hashSet = new HashSet<char>(95);
            var max = 0;
            for (int left = 0, right = 0; right < s.Length; right++)
            {
                if (hashSet.Contains(s[right]))
                {
                    for (; s[left] != s[right]; left++)
                        hashSet.Remove(s[left]);
                }
                hashSet.Add(s[right]);
                max = Math.Max(max, hashSet.Count);
            }

            return max;
        }
    }
}
