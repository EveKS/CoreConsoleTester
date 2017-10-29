using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();

            var bb = $"card-incoming&554998260431024012&{(40 - 40 * 0.02).ToString().Replace(',','.')}&643&2017-08-02T14:11:00Z&&false&q+YzWEJ7ZvV14cc679eqsjhB&c3ca6447-8448-4a1d-bbf6-43ad7094e071";

            System.Console.WriteLine(p.GetHash(bb));
            System.Console.WriteLine(p.GetHashSHA1(bb));
            System.Console.WriteLine(Hash(bb));

            System.Console.WriteLine("Мой 3b85838f27956b26681ef3843eee145b7a767138");
            System.Console.WriteLine("ЯД 3b327288afc9c96bf2b7e85f3dafd2922195e3d5");
            Test(p.GetHash(bb)).Wait();
        }

        async static Task Test(string hash)
        {
            await MaynAsync(hash);
        }

        async static Task MaynAsync(string hash)
        {
            decimal amount = (decimal)(40 - 40 * 0.02);
            decimal withdraw_amount = 40M;
            var notification_type = "card-incoming";
            var operation_id = "554998260431024012";
            var label = "c3ca6447-8448-4a1d-bbf6-43ad7094e071";
            var datetime = "2017-08-02T14:11:00Z";
            var sender = string.Empty;
            var sha1_hash = "3b327288afc9c96bf2b7e85f3dafd2922195e3d5";
            var currency = "643";
            var codepro = "False";

            var url = "https://vkgraber.ru/Order/Paid";
            var formContent = new FormUrlEncodedContent(new[]
            {
                //notification_type card-incoming
                //operation_id 554998260431024012
                //label c3ca6447-8448-4a1d-bbf6-43ad7094e071
                //datetime 2017-08-02T14:11:00Z
                //amount 1.96 1,96
                //withdraw_amount 2.00 2,00
                //sender
                //sha1_hash 3b327288afc9c96bf2b7e85f3dafd2922195e3d5
                //currency 643
                //codepro False

                new KeyValuePair<string, string>("withdraw_amount", withdraw_amount.ToString()),
                new KeyValuePair<string, string>("amount", amount.ToString()),
                new KeyValuePair<string, string>("notification_type", notification_type),
                new KeyValuePair<string, string>("operation_id", operation_id),
                new KeyValuePair<string, string>("label", label),
                new KeyValuePair<string, string>("datetime", datetime),
                new KeyValuePair<string, string>("sha1_hash", hash),
                new KeyValuePair<string, string>("currency", currency),
                new KeyValuePair<string, string>("codepro", codepro)
            });

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
                        var result = await content.ReadAsStringAsync();
                        System.Console.WriteLine(result);
                    }
                }
            }
        }

        static string Hash(string input)
        {
            using (var sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        private string GetHashSHA1(string text)
        {
            var enc = Encoding.ASCII;

            byte[] buffer = enc.GetBytes(text);
            using (var sha1 = SHA1.Create())
            {
                var hash = BitConverter.ToString(sha1.ComputeHash(buffer)).Replace("-", "");
                return hash;
            }
        }

        private string GetHashSHA1v2(string text)
        {
            var enc = Encoding.GetEncoding(0);

            byte[] buffer = enc.GetBytes(text);
            using (var sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(buffer);
                return Encoding.GetEncoding(0).GetString(hash);
            }
        }

        private string GetHash(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            using (var sha1 = SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(bytes);

                return HexStringFromBytes(hashBytes);
            }
        }

        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}