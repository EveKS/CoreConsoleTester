using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace addStringClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri uri = new Uri("http://www.microsoft.com");
            HttpClientHandler handler = new HttpClientHandler()
            {
                CookieContainer = new CookieContainer()
            };
            handler.CookieContainer.Add(uri, new Cookie("name", "value")); // Adding a Cookie
            HttpClient client = new HttpClient(handler);
            HttpResponseMessage response = client.GetAsync(uri).Result;
            CookieCollection collection = handler.CookieContainer.GetCookies(uri); // Retrieving a Cookie
            IEnumerable<Cookie> responseCookies = collection.Cast<Cookie>();
            foreach (Cookie cookie in responseCookies)
                Console.WriteLine(cookie.Name + ": " + cookie.Value);


            var cookies = new HttpCookie("name", ".example.com", "/") { Value = "value" };

            Console.ReadLine();
        }
    }

    public class Class
    {
        private string _text = string.Empty;

        public void AddToString() => _text += $" {Console.ReadLine()}";
        public void PrintWords() => Console.WriteLine(string.Join(Environment.NewLine, Regex.Split(_text, @"\W+").Where(s => !string.IsNullOrWhiteSpace(s))));
    }

}