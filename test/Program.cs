using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();

            Console.Read();
        }

        static async Task MainAsync()
        {
            Program p = new Program();
            var content = await p.HttpGetStringContentAsync(@"http://www.stoloto.ru/5x36/archive");
            await p.ParseAndWriteHtmlAsync(content, "");
        }

        private async Task ParseAndWriteHtmlAsync(string content, string path)
        {
            var parser = new HtmlParser();
            var document = parser.Parse(content);

            var container = ".month .elem";
            var drawContainer = ".main .draw";
            var numbersContainer = ".main .numbers .container.cleared:not(.sorted)";

            using (FileStream fileStream = new FileStream(path + "\\5-36.txt", FileMode.Create))
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                foreach(var item in document.QuerySelectorAll(container))
                {
                    await streamWriter.WriteLineAsync(string.Format("{0}_{2}",
                        item.QuerySelector(drawContainer).TextContent.Trim(),
                        Regex.Replace(item.QuerySelector(numbersContainer).TextContent, @"[\W\D]+", " ").Trim()));
                }
            }
        }

        private async Task<string> HttpGetStringContentAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                using (HttpResponseMessage response = await httpClient.GetAsync(new Uri(url)))
                {
                    var bytes = await response.Content.ReadAsByteArrayAsync();

                    Encoding encoding = Encoding.GetEncoding("utf-8");
                    return encoding.GetString(bytes, 0, bytes.Length);
                }

                //using (HttpResponseMessage response = await httpClient.GetAsync(new Uri(url)))
                //using (var responseStream = await response.Content.ReadAsStreamAsync())
                //using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                //using (var streamReader = new StreamReader(decompressedStream))
                //{
                //    return await streamReader.ReadToEndAsync();
                //}
            }
        }

        public class Stoloto
        {
            public string Date { get; set; }
            public string Draw { get; set; }
            public string Numbers { get; set; }
            public string Prize { get; set; }
        }
    }
}