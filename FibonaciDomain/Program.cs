using System;

namespace FibonaciDomain
{
    class Program
    {
        static void Main(string[] args)
        {
            // создаем вторичный домен
            AppDomain factorialDomain = AppDomain.CreateDomain("Factorial Domain");
            factorialDomain.DomainUnload += SecondaryDomain_DomainUnload;

            int number = 6;
            // определяем аргументы для программы
            string[] arguments = new string[] { number.ToString() };
            // полный путь к файлу программы - bin/Debug/FactorialApp.exe
            string assemblyPath = factorialDomain.BaseDirectory + "FactorialApp.exe";
            // загрузка и выполнение программы
            factorialDomain.ExecuteAssembly(assemblyPath, arguments);
            // выгрузка домена
            AppDomain.Unload(factorialDomain);

            Console.Read();
        }

        private static void SecondaryDomain_DomainUnload(object sender, EventArgs e)
        {
            Console.WriteLine("Домен Factorial Domain выгружен из процесса");
        }
    }
}
