using AngleSharp.Parser.Html;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwoString
{
    class Program
    {
        static void Main(string[] args)
        {
            var find = Console.ReadLine();
            //var url = @"https://www.google.ru/search?q=rfhnbyrbb&newwindow=1&biw=1024&bih=240&source=lnms&tbm=isch&sa=X&ved=";
            string url = "https://www.google.ru/search?q=" + find + "&tbm=isch&tbs=isz:l";
            //string youtube = @"https://youtu.be/w6aHuTzJSaE";

            var path = @"E:\video\";

            var p = new Program();
            p.MainAsync(url, path).Wait();

            Console.WriteLine("Downloaded all");
            Console.ReadKey();
        }

        async Task MainAsync(string url, string path)
        {
            var filesNames = new DirectoryInfo(path)
                .EnumerateFiles()
                .Select(f => f.Name)
                .ToList();

            var content = await HttpGetStringContentAsync(url);
            var fileUrls = ParseHtmlToUrl(content);
            var sorted = fileUrls.OrderBy(f => f.Count).ToList();

            foreach (var fileUrl in fileUrls)
            {
                var name = CreateName(filesNames, fileUrl);
                await DownloadFile(fileUrl, path, name);
                Console.WriteLine($"{Path.GetFileName(fileUrl)} is download");
                filesNames.Add(name);
            }
        }

        private async Task DownloadFile(string fileUrl, string path, string name)
        {
            byte[] data;

            using (var client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(fileUrl))
            using (HttpContent content = response.Content)
            {
                data = await content.ReadAsByteArrayAsync();
                using (FileStream file = File.Create(path + name))
                    await file.WriteAsync(data, 0, data.Length);
            }
        }

        private string CreateName(List<string> filesNames, string addedFile)
        {
            var fileNameToChar = Enumerable.Range('a', 'z' - 'a')
                .Select(Convert.ToChar)
                .ToArray();

            var fileExtension = Path.GetExtension(addedFile);

            var fileName = "";
            while (filesNames.Contains(fileName = new string(fileNameToChar) + fileExtension))
            {
                Sorting(fileNameToChar);
            }

            return fileName;
        }

        private void Sorting(IList<char> list)
        {
            Random rnd = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(0, i + 1);
                char temp = list[i];
                list[i] = list[j];
                list[j] = temp;
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
                    string responseString = encoding.GetString(bytes, 0, bytes.Length);
                    return responseString;
                }
            }
        }

        private List<JObject> ParseHtmlToUrl(string content)
        {
            var parser = new HtmlParser();
            var document = parser.Parse(content);

            var imgContainers = "div[class='rg_meta notranslate']";
            var containers = document.QuerySelectorAll(imgContainers)
                .Select(doc => doc.TextContent)
                .ToList();

            var result = new List<JObject>(containers.Count);
            try
            {
                foreach (var item in containers)
                {
                    result.Add(JObject.Parse(item));
                }
            }
            catch
            {

            }

            return result;
        }
    }
}