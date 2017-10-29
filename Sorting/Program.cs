using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            object[,] obj =
                {
                    {1, 2, 3, 4, 5 },
                    {2, 3, 4, 5, 6.3 }
                };

            var result = obj.OfType<double>()
                .Select((o, i) => new { d = o, group = i % obj.GetLength(0) })
                .GroupBy(g => g.group)
                .Select(g => g.Select(o => o.d).ToArray()).ToArray();


            Console.ReadKey();
        }
    }
}