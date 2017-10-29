using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace LinkFind
{
    class HtmlGetter
    {
        public async Task<string> GetAsync(Uri url)
        {
            try
            {
                using (var hendler = Handler())
                {
                    using (var httpClient = new HttpClient(hendler))
                    {
                        SetClientHeaders(httpClient);

                        using (HttpResponseMessage response = await httpClient.GetAsync(url))
                        using (HttpContent content = response.Content)
                        {
                            return await content.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        private void SetClientHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, sdch");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");
        }

        private HttpClientHandler Handler()
        => new HttpClientHandler()
        {
            AllowAutoRedirect = true,
            MaxAutomaticRedirections = 15,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.None,
            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
        };
    }
}