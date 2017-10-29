using CsQuery;
using InstagramParser.JsonModel.Instagram;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VkGroupManager.JsonModel.InstagramNext;

namespace InstagramParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            var url = "https://www.instagram.com/graphql/query/?query_id=17888483320059182&variables={\"id\":\"1187237132\",\"first\":100,\"after\":\"AQAuVsBTXbqB0J2eDJholKSRf09-4yPON6WHTbM-0_UN9xSSEB67yHZwaCOPhQcTEYHXx6XmIljj3xUJaV-WnlV9EkFTzhXz0bBWCj2O45nMoA\"}";

            var json = p.DownloadPageAsync(url).Result;

            //var result = p.JsonConvertDeserializeObject<InstagramNext>(json);

            //var media = result.Data.User.EdgeOwnerToTimelineMedia;

            p.MainAsync().Wait();
        }

        private async Task MainAsync()
        {
            //string url = @"https://www.instagram.com/buzova86/?hl=ru";
            // https://www.instagram.com/graphql/query/?query_id=17888483320059182&variables={"id":"267685466","first":12,"after":"AQBc5OWrVBoSPUBOfnvpq-dKzkMLJi-yJbjsV5pdeOxK4dx5SQapOQJiajAiTn6j_7NNBbgWYDRRFR3P2eojctUaj34cVR18f3akFrBydmnHEA"}
            // https://www.instagram.com/graphql/query/?query_id=17888483320059182&variables={"id":"1187237132","first":100,"after":"AQAuVsBTXbqB0J2eDJholKSRf09-4yPON6WHTbM-0_UN9xSSEB67yHZwaCOPhQcTEYHXx6XmIljj3xUJaV-WnlV9EkFTzhXz0bBWCj2O45nMoA"}
            string[] urls =
                {
                    @"https://www.instagram.com/borodylia/?hl=ru",
                    @"https://www.instagram.com/xenia_sobchak/?hl=ru",
                    @"https://www.instagram.com/navalny4/?hl=ru",
                    @"https://www.instagram.com/maxgalkinru/?hl=ru",
                    @"https://www.instagram.com/anilorak/?hl=ru"
                };

            var path = @"D:\Instagram\";

            foreach (var url in urls)
            {
                var result = await DownloadPageAsync(url);
                CQ cq = CQ.Create(result);
                var json = cq["body script"]
                    .FirstOrDefault(js => js.TextContent.Contains("_sharedData"))
                    .TextContent.Replace("window._sharedData =", string.Empty).TrimEnd(';');

                var parsing = JsonConvertDeserializeObject<Instagram>(json)?
                    .EntryData?
                    .ProfilePage?
                    .SelectMany(prof => prof.User?.Media?.Nodes?.Select(node => node.DisplaySrc))
                    .ToList();

                if (parsing != null)
                {
                    foreach (var src in parsing)
                    {
                        await DownloadAsync(src, path);
                    }
                }
            }

            Console.WriteLine("Downloaded");
        }

        private static async Task DownloadAsync(string url, string path)
        {
            using (HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                MaxAutomaticRedirections = 15,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.None,
                UseDefaultCredentials = true,
                ClientCertificateOptions = ClientCertificateOption.Manual,
                SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
            })
            {
                clientHandler.ClientCertificates.Add(new X509Certificate2("C:\\Users\\eveks\\Desktop\\Новая папка (2)\\1_Intermediate.crt"));
                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                    using (Stream stream = await client.GetStreamAsync(url))
                    using (FileStream file = File.Create(path + Path.GetFileName(url)))
                    {
                        await stream.CopyToAsync(file);
                    }
                }
            }
        }

        private async Task<string> DownloadPageAsync(string url)
        {
            var result = string.Empty;

            using (HttpClientHandler clientHandler = new HttpClientHandler())
            {
                clientHandler.UseDefaultCredentials = true;
                clientHandler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;

                using (var httpClient = new HttpClient(clientHandler))
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
        }

        private T JsonConvertDeserializeObjectWithNull<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
            }
            catch
            {
                return default(T);
            }
        }

        private T JsonConvertDeserializeObject<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }
    }
}