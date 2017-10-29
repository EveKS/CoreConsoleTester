using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace GetProcesses
{
    class Program
    {

        static void Main(string[] args)
        {
            List<string> list1 = new List<string>() { "Петров", "Иванов", "Сидоров", "Путин" };
            List<string> list2 = new List<string>() { "Медведев", "Лебедев", "Зайцев", "Петров" };

            Console.WriteLine("Этих фамилий нет в списке A, но есть в списке B");
            foreach (string s in list2.Except(list1))
            {
                Console.WriteLine(s);
            }

            Console.ReadKey();

        }
    }
}
