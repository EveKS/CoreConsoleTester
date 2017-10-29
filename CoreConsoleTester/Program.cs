using Birthdays.JsonModel;
using CsQuery;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PI_riad
{
    class Program
    {
        const string TOKEN = "344652520:AAE6zsadftMdDgalmz2H3vEMq52eAR5bjag";
        //static string chat_id = "273841531";
        static string chat_id = "396463464";

        public static void Main()
        {
            var pr = new Program();
            //var url = @"http://www.sunhome.ru/i/wallpapers/31/gomer-simpson-kartinka.orig.jpg";

            //var paths = new[]
            //{
            //    @"http://www.radionetplus.ru/uploads/posts/2013-05/1369460629_panda-1.jpeg",
            //    @"https://pp.vk.me/c9328/v9328479/12c4/JHkM6SAXU44.jpg",
            //    @"http://bm.img.com.ua/nxs/img/prikol/images/large/2/7/312072_912608.jpg",
            //    @"http://cdn.fishki.net/upload/post/2017/03/19/2245758/tn/07-cute-cat-wallpaper-hdcat-wallpaper.jpg",
            //    @"http://reading.com.ua/wp-content/uploads/2016/01/krasivye-vdoxnovlyayushhie-kartinki-750x498.jpg.pagespeed.ce.Xa_tYk_qBR.jpg",
            //    @"http://mirvkartinkah.ru/wp-content/uploads/osen-foto-peizazhi-03.jpg",
            //    @"http://www.cinemotionlab.com/content/Camera-Lens-Bokeh-Blurred-Artistic-Photo.jpg"
            //};

            var gif = @"https://vk.com/doc308809841_448156068?hash=0ebf4fbf2bbbf11e8d&dl=GQYDCMBRGA4DANY:1500816823:f40d07ebb5f735ca1b&api=1&no_preview=1";
            //DownloadAsync(gif, path).Wait();
            //GetFileth(path, paths).Wait();

            //Console.WriteLine(Path.GetExtension(gif));
            //Console.WriteLine(Path.GetFileName(gif));
            //Console.WriteLine(new Uri(gif).AbsolutePath);
            //Console.WriteLine(new Uri(gif).AbsoluteUri);
            //Console.WriteLine(new Uri(gif).LocalPath);
            //Console.WriteLine(new Uri(gif).IsFile);
            //Console.WriteLine(new Uri(gif).AbsolutePath);
            var path = @"E:\Video\";

            string youtube = @"https://www.youtube.com/v/w6aHuTzJSaE";

            new Program().GetUrls(youtube).Wait();

            //DownloadFile(youtube, path).Wait();

            Console.WriteLine("Is Write");
            Console.ReadKey();
        }

        private HttpClientHandler Handler()
        {
            return new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                MaxAutomaticRedirections = 15,
                AutomaticDecompression = DecompressionMethods.GZip
                    | DecompressionMethods.Deflate | DecompressionMethods.None
            };
        }

        async Task<string> PostAsync(string url, FormUrlEncodedContent formContent)
        {
            using (var hendler = Handler())
            {
                using (var httpClient = new HttpClient(hendler))
                {
                    SetClientHeaders(httpClient);

                    using (HttpResponseMessage response = await httpClient.PostAsync(url, formContent))
                    using (HttpContent content = response.Content)
                    {
                        return await content.ReadAsStringAsync();
                    }
                }
            }
        }

        async Task<string> PostStreamAsync(string url, MultipartFormDataContent formContent)
        {
            using (var hendler = Handler())
            {
                using (var httpClient = new HttpClient(hendler))
                {
                    SetClientHeaders(httpClient);

                    using (HttpResponseMessage response = await httpClient.PostAsync(url, formContent))
                    using (HttpContent content = response.Content)
                    {
                        return await content.ReadAsStringAsync();
                    }
                }
            }
        }

        async Task<string> GetAsync(string url)
        {
            using (var hendler = Handler())
            using (var httpClient = new HttpClient(hendler))
            {
                SetClientHeaders(httpClient);

                using (HttpResponseMessage response = await httpClient.GetAsync(new Uri(url)))
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (StreamReader sr = new StreamReader(stream))
                {
                    return await sr.ReadToEndAsync();
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

        async Task<Stream> GetStreamAsync(string url, HttpCompletionOption httpCompletionOption)
        {
            using (var hendler = Handler())
            {
                using (var httpClient = new HttpClient(hendler))
                {
                    SetClientHeaders(httpClient);
                    using (var responseContent = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                    {
                        if (responseContent.IsSuccessStatusCode)
                        {
                            return await responseContent.Content.ReadAsStreamAsync();
                        }
                        else
                        {
                            return (default(Stream));
                        }
                    }
                }
            }
        }

        private void SetClientHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content -Type", "application/json; charset=UTF-8");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, sdch");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");
        }

        private async Task GetUrls(string url)
        {
            var name = Path.GetFileName(url);

            string html = await GetAsync(url);

            CQ cq = CQ.Create(html);

            var json = cq["script"]
                .FirstOrDefault(js => js.TextContent.Contains("ytplayer"))
                ?
                .TextContent
                .Replace("var ytplayer = ytplayer || {};ytplayer.config =", string.Empty)
                //.Replace("window._sharedData =", string.Empty)
                //.Replace("window._sharedData =", string.Empty)
                .TrimEnd(';');


            //return Task.FromResult(0);
        }

        private static async Task DownloadAsync(string url, string path)
        {
            using (HttpClientHandler clientHandler = new HttpClientHandler())
            {
                clientHandler.UseDefaultCredentials = true;
                clientHandler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;

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



        private static async Task GetFileth(string path, string[] paths)
        {
            foreach (var p in paths)
            {
                await DownloadFile(p.Replace(@"\", ""), path);
            }
        }

        public static async Task DownloadFile(string url, string path)
        {
            var name = Path.GetFileName(url);
            byte[] data;

            using (var client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                data = await content.ReadAsByteArrayAsync();
                using (FileStream file = File.Create(path + name))
                    await file.WriteAsync(data, 0, data.Length);
            }
        }

        public async Task MainAsync()
        {
            await StartWork();
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp);
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }


        void TimerTest()
        {

        }

        async Task StartWork()
            => await Task.Run(async () =>
       {
           var ctoken = new CancellationTokenSource();

           await Task.Run(async () =>
           {
               while (true)
               {
                   await Task.Delay((1000), ctoken.Token);
                   await Testing().ContinueWith(async responce =>
                   {
                       var content = await responce.Result.Content.ReadAsStringAsync();

                       if (JsonConvertDeserializeObject<ResponseMessage>(content).Ok)
                       {
                           Console.WriteLine("true");
                       }
                   });
               }
           }, ctoken.Token);
       });

        static async Task Test()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Run(() => Console.WriteLine("Test"));
                await Task.Delay(1000);
            }
        }



        async Task<HttpResponseMessage> Testing()
        {
            MultipartFormDataContent multipartForm;
            var url = "https://api.telegram.org/bot" + TOKEN +
                "/sendMessage";

            using (var httpClient = new HttpClient())
            {
                var form = new MultipartFormDataContent
                {
                    { new StringContent(chat_id, Encoding.UTF8), "chat_id" },
                    { new StringContent("Ну что - нужен личный бот? Тут ответ не увижу.", Encoding.UTF8), "text" }
                };

                multipartForm = form;

                return await httpClient.PostAsync(url, multipartForm);
            }
        }

        static void Shuffle<T>(IList<T> list)
        {
            Random rnd = new Random();
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(0, i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        static async Task ReadAsync(List<string> data, string path)
        {
            var pattern = @"([a-z]+)";

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            using (var fs = new FileStream(path, FileMode.Open))
            using (var sr = new StreamReader(fs))
            {
                while (sr.Peek() >= 0)
                {
                    data.AddRange(regex.Matches(await sr.ReadLineAsync()).
                        OfType<Match>().Select(m => m.Value));
                }
            }
        }

        /// <summary>
        /// ЗапроC
        /// </summary>
        /// <param name="url">URL по которому нужно выполнить запроC</param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> GetResponce(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetAsync(url);
            }
        }

        private T JsonConvertDeserializeObject<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
        }

        /// <summary>
        /// Формирование URL запроCа к боту тулуграм
        /// </summary>
        /// <param name="chat_id">ID чата</param>
        /// <param name="text">Cообщение</param>
        /// <param name="requestParams">Дополнительные параметры запроCа</param>
        /// <returns></returns>
        private string SendMessageUrl(string chat_id, string text, params RequestParams[] requestParams)
        {
            var url = "https://api.telegram.org/bot" + TOKEN +
                "/sendMessage?" + "chat_id=" + chat_id +
                "&text=" + text;

            for (int i = 0; i < requestParams.Length; i++)
            {
                url += $"&{requestParams[i].Key}={requestParams[i].Value}";
            }

            return url;
        }

        private class RequestParams
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }

    class TelegramBtn
    {
        InlineKeyboardMarkup reply_markup;
        List<List<InlineKeyboardButton>> _btns;

        public TelegramBtn(int rows)
        {
            _btns = new List<List<InlineKeyboardButton>>();
            for (int i = 0; i < rows; i++)
            {
                _btns.Add(new List<InlineKeyboardButton>());
            }

            reply_markup = new InlineKeyboardMarkup()
            {
                InlineKeyboard = _btns
            };
        }

        public void AddCallbackDataBtn(int rowIndex, string text, string callbackData)
        {
            _btns[rowIndex].Add(new InlineKeyboardButton
            {
                Text = text,
                CallbackData = callbackData
            });
        }

        public string GetReplyMarkup()
        {
            return JsonConvert.SerializeObject(reply_markup,
                    Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
        }
    }

    public class ResponseMessage
    {

        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("result")]
        public object Result { get; set; }
    }
}