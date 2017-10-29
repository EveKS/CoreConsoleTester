using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace fibo
{
    class Program
    {
        private IEnumerable<BigInteger> Fibonacci(int count)
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

        public static void Main()
        {
            Console.WriteLine(string.Join(Environment.NewLine,
                new BigInteger(100).Prime()));

            Console.ReadKey(false);
        }
    }

    public static class Primes
    {
        public static IEnumerable<BigInteger> Prime(this BigInteger number)
        {
            var cache = new Dictionary<BigInteger, BigInteger>();

            for (BigInteger i = 1; i <= number / 2; i++)
            {
                bool isPrime = true;
                for (int j = 0; j < cache.Count; j++)
                {
                    if (i % cache[cache.Keys.ElementAt(j)] == 0)
                    {
                        isPrime = false;
                    }
                }

                if(isPrime)
                    yield return number;
            }
        }
    }
}
