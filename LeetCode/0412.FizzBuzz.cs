using BenchmarkDotNet.Attributes;

namespace LeetCode
{
    //// * Summary for 30_000 *

    // BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19042.1889/20H2/October2020Update)
    // Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
    // .NET SDK= 6.0.300
    // [Host] : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT AVX2

    // Job = InProcess  Toolchain=InProcessEmitToolchain

    //|        Method |     Mean |   Error |  StdDev |     Gen0 |    Gen1 |    Gen2 |  Allocated |
    //|-------------- |---------:|--------:|--------:|---------:|--------:|--------:|-----------:|
    //|   FizzBuzzOne | 793.2 us | 4.28 us | 4.01 us | 142.5781 | 71.2891 | 71.2891 | 1171.66 KB |
    //|   FizzBuzzTwo | 359.1 us | 1.76 us | 1.65 us |  71.2891 | 71.2891 | 71.2891 |  734.29 KB |
    //| FizzBuzzThree | 352.2 us | 1.93 us | 1.81 us |  71.2891 | 71.2891 | 71.2891 |  734.29 KB |
    [InProcess]
    [MemoryDiagnoser(true)]
    public class FizzBuzz
    {
        private const string fizz = "Fizz";
        private const string buzz = "Buzz";
        private const string fizzBuzz = "FizzBuzz";

        [Benchmark]
        public void FizzBuzzOne() => FizzBuzzOne(30_000);
        [Benchmark]
        public void FizzBuzzTwo() => FizzBuzzTwo(30_000);
        [Benchmark]
        public void FizzBuzzThree() => FizzBuzzThree(30_000);

        // Memory: O(n)
        // Performance: O(n)
        public IList<string> FizzBuzzOne(int n)
        {
            var result = new string[n];
            for (int i = 1; i <= n; i++)
                result[i - 1] = i.ToString();
            for (int i = 2; i < n; i += 3)
                result[i] = fizz;
            for (int i = 4; i < n; i += 5)
                result[i] = buzz;
            for (int i = 14; i < n; i += 15)
                result[i] = fizzBuzz;

            return result;
        }

        // Memory: O(n)
        // Performance: O(n)
        public IList<string> FizzBuzzTwo(int n)
        {
            var result = new string[n];
            for (int i = 1; i <= n; i++)
            {
                if (i % 15 == 0)
                    result[i - 1] = fizzBuzz;
                else if (i % 3 == 0)
                    result[i - 1] = fizz;
                else if (i % 5 == 0)
                    result[i - 1] = buzz;
                else
                    result[i - 1] = i.ToString();

            }

            return result;
        }

        // Memory: O(n)
        // Performance: O(n)
        public IList<string> FizzBuzzThree(int n)
        {
            var result = new string[n];
            for (int i = 1, i3 = 1, i5 = 1; i <= n; i++)
            {
                if (i3 == 3 && i5 == 5)
                {
                    result[i - 1] = fizzBuzz;
                    i3 = 0;
                    i5 = 0;
                }
                else if (i3 == 3)
                {
                    result[i - 1] = fizz;
                    i3 = 0;
                }
                else if (i5 == 5)
                {
                    result[i - 1] = buzz;
                    i5 = 0;
                }
                else
                    result[i - 1] = i.ToString();

                i3++;
                i5++;
            }

            return result;
        }
    }
}
