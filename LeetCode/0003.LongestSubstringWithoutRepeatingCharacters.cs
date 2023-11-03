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
    //// * Summary *

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

    [InProcess]
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

            var dict = new Dictionary<char, int>();
            var max = 0;
            for (int left = 0, right = 0; right < s.Length; right++)
            {
                if (dict.ContainsKey(s[right]))
                    left = Math.Max(left, dict[s[right]] + 1);
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

            var hashSet = new HashSet<char>();
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
