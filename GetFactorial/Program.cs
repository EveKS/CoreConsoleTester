using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace GetFactorial
{
    class Program
    {
        IEnumerable<BigInteger> Fibonacci(int count)
        {
            BigInteger f1 = 1, f2 = 1, f3;
            while (count-- > 0)
            {
                yield return f1;
                f3 = f1 + f2;
                f1 = f2;
                f2 = f3;
            }
        }

        static void Main(string[] args)
        {
            var p = new Program();

            var sw = Stopwatch.StartNew();
            var fib = p.Fibonacci(500000).Last();
            sw.Stop();

            //Console.WriteLine(fib);
            Console.WriteLine(sw.Elapsed);

            Console.ReadKey(false);
        }
    }
}
