using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            long pass = 1000000000;
            int a = GetA();
            int b = GetB();
            int c = 0;

            Stopwatch sw1 = Stopwatch.StartNew();
            for (long i = 0; i < pass; ++i)
            {
                c = (a * 2 / b + 1) / 2;
            }
            sw1.Stop();

            Stopwatch sw2 = null;
            unsafe
            {
                sw2 = Stopwatch.StartNew();
                for (long i = 0; i < pass; ++i)
                {
                    c = (int)Math.Round((double)a / (double)b, MidpointRounding.AwayFromZero);
                }
                sw2.Stop();
            }

            Console.WriteLine($"зайчик: {sw1.Elapsed}");
            Console.WriteLine($"черепашка: {sw2.Elapsed}");
            Console.ReadKey();
            GC.KeepAlive(c);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int GetA() => 500;

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int GetB() => 1000;
    }
}