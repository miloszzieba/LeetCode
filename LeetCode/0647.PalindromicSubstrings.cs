using BenchmarkDotNet.Attributes;

namespace LeetCode
{

    //BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
    //Intel Core i9-9900K CPU 3.60GHz(Coffee Lake), 1 CPU, 16 logical and 8 physical cores
    //.NET SDK 10.0.101
    //  [Host] : .NET 10.0.1 (10.0.1, 10.0.125.57005), X64 RyuJIT x86-64-v3
    //Job = InProcess  Toolchain=InProcessEmitToolchain

    //// * Summary for 77 chars*

    //| Method                       | Mean       | Error    | StdDev   | Gen0   | Allocated |
    //|----------------------------- |-----------:|---------:|---------:|-------:|----------:|
    //| BruteForce                   | 8,217.5 ns | 83.63 ns | 78.23 ns |      - |         - |
    //| DynamicProgramming           | 7,263.1 ns | 34.69 ns | 32.45 ns | 0.7095 |    5976 B |
    //| DynamicProgrammingEvenAndOdd |   296.0 ns |  1.67 ns |  1.56 ns |      - |         - |
    //| Manacher                     |   761.8 ns |  4.60 ns |  4.30 ns | 0.1631 |    1368 B |

    //// * Summary for 154 chars*

    //| Method                       | Mean        | Error     | StdDev    | Gen0   | Allocated |
    //|----------------------------- |------------:|----------:|----------:|-------:|----------:|
    //| BruteForce                   | 51,996.4 ns | 481.61 ns | 450.50 ns |      - |         - |
    //| DynamicProgramming           | 30,296.9 ns | 172.66 ns | 161.51 ns | 2.8076 |   23760 B |
    //| DynamicProgrammingEvenAndOdd |    778.7 ns |   3.86 ns |   3.42 ns |      - |         - |
    //| Manacher                     |  1,568.8 ns |  17.06 ns |  15.96 ns | 0.3090 |    2600 B |

    //// * Summary for 308 chars*

    //| Method                       | Mean       | Error     | StdDev    | Gen0    | Gen1    | Gen2    | Allocated |
    //|----------------------------- |-----------:|----------:|----------:|--------:|--------:|--------:|----------:|
    //| BruteForce                   | 230.962 us | 1.6784 us | 1.4878 us |       - |       - |       - |         - |
    //| DynamicProgramming           | 136.696 us | 0.5121 us | 0.4539 us | 29.2969 | 29.2969 | 29.2969 |   94972 B |
    //| DynamicProgrammingEvenAndOdd |   1.500 us | 0.0079 us | 0.0070 us |       - |       - |       - |         - |
    //| Manacher                     |   2.407 us | 0.0045 us | 0.0037 us |  0.4463 |       - |       - |    3760 B |

    //// * Summary for 525 characters with many overlapping palindromes ('a's at the end)*
    //// I was accidentally testing the best-case scenario for DynamicProgrammingEvenAndOdd() before.

    //| Method                       | Mean         | Error      | StdDev    | Gen0    | Gen1    | Gen2    | Allocated |
    //|----------------------------- |-------------:|-----------:|----------:|--------:|--------:|--------:|----------:|
    //| BruteForce                   | 1,023.863 us | 10.6836 us | 9.9935 us |       - |       - |       - |         - |
    //| DynamicProgramming           |   396.699 us |  2.6425 us | 2.4718 us | 83.0078 | 83.0078 | 83.0078 |  275864 B |
    //| DynamicProgrammingEvenAndOdd |    14.793 us |  0.0391 us | 0.0346 us |       - |       - |       - |         - |
    //| Manacher                     |     4.237 us |  0.0150 us | 0.0125 us |  0.7553 |       - |       - |    6360 B |

    [InProcess]
    [MemoryDiagnoser(true)]
    public class PalindromicSubstrings
    {

        private string _s;

        [GlobalSetup]
        public void BenchmarkSetup()
        {
            this._s = "abababababababbababbbbabbababababbabbababababababababbabababababababababababababababababababababbaabababababbabababababaababababbababababababababababababaabababababababbababbbbabbababababbabbababababababababbabababababababababababababababababababababbaabababababbabababababaababababbababababababababababababaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        }

        [Benchmark]
        public int BruteForce() => BruteForce(this._s);
        [Benchmark]
        public int DynamicProgramming() => DynamicProgramming(this._s);
        [Benchmark]
        public int DynamicProgrammingEvenAndOdd() => DynamicProgrammingEvenAndOdd(this._s);
        [Benchmark]
        public int Manacher() => Manacher(this._s);


        public int BruteForce(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;

            var result = s.Length;
            for (int i = s.Length; i > 1; i--)
            {
                for (int j = 0; j <= s.Length - i; j++)
                {
                    if (IsPalindrome(s, j, j + i - 1))
                        result++;
                }
            }
            return result;
        }

        private bool IsPalindrome(String s, int left, int right)
        {
            while (left < right)
                if (s[left++] != s[right--]) return false;

            return true;
        }

        public int DynamicProgramming(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;

            var array = new bool[s.Length, s.Length];
            var result = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                for (int j = i; j < s.Length; j++)
                {
                    if (i == j)
                        array[i, j] = true;
                    else if (j == i + 1)
                        array[i, j] = s[i] == s[j];
                    else
                        array[i, j] = s[i] == s[j] && array[i + 1, j - 1];

                    if (array[i, j])
                        result++;
                }
            }
            return result;
        }

        public int DynamicProgrammingEvenAndOdd(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            
            var result = 0;
            
            for (int i = 0; i < s.Length; i++)
            {
                result += CountPalindromesFromCenter(s, i, i);
                result += CountPalindromesFromCenter(s, i, i + 1);
            }
            return result;
        }

        private int CountPalindromesFromCenter(String s, int left, int right)
        {
            int count = 0;
            while (left >= 0 && right < s.Length && s[left] == s[right])
            {
                count++;
                left--;
                right++;
            }
            return count;
        }

        public int Manacher(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            char[] charArray = new char[s.Length * 2 + 1];
            for (int i = 0; i < s.Length; i++)
            {
                charArray[i * 2] = '#';
                charArray[i * 2 + 1] = s[i];
            }
            charArray[charArray.Length - 1] = '#';

            int[] palindromeArray = new int[charArray.Length];
            int center = 0, right = 0, result = 0;
            for (int i = 0; i < charArray.Length; i++)
            {
                var mirror = 2 * center - i;
                if (i < right)
                    palindromeArray[i] = Math.Min(right - i, palindromeArray[mirror]);
                
                // Attempt to expand palindrome centered at i
                int a = i + (1 + palindromeArray[i]);
                int b = i - (1 + palindromeArray[i]);
                while (a < charArray.Length && b >= 0 && charArray[a] == charArray[b])
                {
                    palindromeArray[i]++;
                    a++;
                    b--;
                }
                // If palindrome centered at i expands past right,
                // adjust center and right boundaries
                if (i + palindromeArray[i] > right)
                {
                    center = i;
                    right = i + palindromeArray[i];
                }
                // Count the palindromes found at index i
                result += (palindromeArray[i] + 1) / 2;
            }
            return result;
        }
    }
}
