using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sportlotoParse
{
    class Program
    {
        private static Random rnd = new Random();

        static void Main(string[] args)
        {
            var p = new Program();
            p.MainAsync().Wait();
        }

        const int DELAY = 1500;
        const string VERSION = "5.67";

        string message = "Привет\n сделай пожалуйста репост закрепленной записи на моей стене, этой:";
        string attachment = "wall401010807_10";
        string access_token = "e1f2c2ab84552786d9bed99fc9c7876ab90b731ac41cb3e3c499c6ee3e98dcf686fc6b294bf86f29dbd1d";
        string method = "messages.send";

        // https://oauth.vk.com/blank.html#access_token=b8634148a95ad58bd7273ba89499b6ae2e0c581eefa205a941aff120b1f71e76fcae105c124d24bc16a81&expires_in=0&user_id=48842595

        private async Task MainAsync()
        {
            var result = await FriendsGetAsync("48842595");
            var friend = JsonConvertDeserializeObject<Friends>(result);

            foreach (var id in friend.Response.Items.Skip(50))
            {
                var res = await MessagesSendAsync(id.ToString(), null, null, null);
                Console.WriteLine(res);
                while (res.Contains("error_code") && res.Contains("14"))
                {
                    var captcha = JsonConvertDeserializeObject<ErrorResult>(result);
                    Console.WriteLine(captcha.Error.CaptchaImg);
                    var captcha_key = Console.ReadLine();
                    var captcha_sid = captcha.Error.CaptchaImg;
                    res = await MessagesSendAsync(id.ToString(), null, captcha_key, captcha_sid);
                }
            }
        }

        private async Task<string> FriendsGetAsync(string user_id)
        {
            string result = string.Empty;
            var content = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("access_token", access_token),
                    new KeyValuePair<string, string>("user_id", user_id),
                    new KeyValuePair<string, string>("v", VERSION),
                };

            var url = $"https://api.vk.com/method/friends.get";
            using (var formContent = new FormUrlEncodedContent(content))
            {
                result = await HttpPostAsync(url, formContent);
            }

            return result;
        }

        private async Task<string> MessagesSendAsync(string user_id, string domain, string captcha_key, string captcha_sid)
        {
            var content = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("access_token", access_token),
                    new KeyValuePair<string, string>("attachment", attachment),
                    new KeyValuePair<string, string>("message", message),
                    new KeyValuePair<string, string>("method", method),
                    new KeyValuePair<string, string>("v", VERSION),
                };

            if (string.IsNullOrWhiteSpace(domain))
            {
                content.Add(new KeyValuePair<string, string>("user_id", user_id));
            }
            else
            {
                content.Add(new KeyValuePair<string, string>("domain", domain));
            }

            if (!string.IsNullOrWhiteSpace(captcha_key))
            {
                content.Add(new KeyValuePair<string, string>("captcha_key", captcha_key));
                content.Add(new KeyValuePair<string, string>("captcha_sid", captcha_sid));
            }

            var url = $"https://api.vk.com/method/messages.send";
            using (var formContent = new FormUrlEncodedContent(content))
            {
                return await HttpPostAsync(url, formContent);
            }
        }

        private async Task<string> HttpPostAsync(string url, FormUrlEncodedContent formContent)
        {
            string result = string.Empty;

            Stopwatch delay = Stopwatch.StartNew();
            using (HttpClientHandler clientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                MaxAutomaticRedirections = 15,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.None,
                ClientCertificateOptions = ClientCertificateOption.Manual,
            })
            {
                using (var httpClient = new HttpClient(clientHandler))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, sdch");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                    using (HttpResponseMessage response = await httpClient.PostAsync(url, formContent))
                    using (HttpContent content = response.Content)
                    {
                        result = await content.ReadAsStringAsync();
                    }
                }
            }

            delay.Stop();

            var delayTime = DELAY - delay.Elapsed.TotalMilliseconds;
            if (delayTime > 0)
            {
                await Task.Delay((int)delayTime);
            }

            return result;
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

        private static string[] GetKey(string text)
             => Regex.Split(text, @"[.!?()\[\]{}\n]")
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();

        private static int NotIntered(int[] a, int[] b)
        {
            int length = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (!b.Contains(a[i]))
                {
                    length++;
                }
            }

            return length;
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
    }

    public class RequestParam
    {

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Error
    {

        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }

        [JsonProperty("error_msg")]
        public string ErrorMsg { get; set; }

        [JsonProperty("request_params")]
        public IList<RequestParam> RequestParams { get; set; }

        [JsonProperty("captcha_sid")]
        public string CaptchaSid { get; set; }

        [JsonProperty("captcha_img")]
        public string CaptchaImg { get; set; }
    }

    public class ErrorResult
    {

        [JsonProperty("error")]
        public Error Error { get; set; }
    }


    public class Response
    {

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("items")]
        public IList<int> Items { get; set; }
    }

    public class Friends
    {

        [JsonProperty("response")]
        public Response Response { get; set; }
    }
}
