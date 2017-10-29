using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkFind
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            var str = "javascript:void(0)";

            Console.WriteLine(Uri.TryCreate(str, UriKind.Absolute, out Uri u));

            foreach (var task in p.MainAsync())
            {
                task.Wait();
            }

            Console.WriteLine("Ended");

            Console.ReadKey(false);
        }

        private List<Task> MainAsync()
        {
            var utls = new Uri[]
            {
                new Uri(@"https://www.facebook.com/"),
            };

            var tasks = new List<Task>(utls.Length);

            foreach (var url in utls)
            {
                var task = Task.Run(async () =>
                {
                    HtmlCollection htmlCollection = new HtmlCollection(url);

                    await htmlCollection.DeepAdd(url, 10);
                });

                tasks.Add(task);
            }

            return tasks;
        }
    }
}
