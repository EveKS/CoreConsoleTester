using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkFind
{
    public sealed class Singleton
    {
        #region Singlton
        private static readonly Object s_lock = new Object();

        private static Singleton instance = null;


        public static Singleton Instance
        {
            get
            {
                if (instance != null) return instance;

                Monitor.Enter(s_lock);

                Singleton temp = new Singleton();

                Interlocked.Exchange(ref instance, temp);

                Monitor.Exit(s_lock);

                return instance;
            }
        }
        #endregion

        private volatile ConcurrentStack<Uri> _htmls;

        private volatile ConcurrentStack<string> _emails;

        private Singleton()
        {
            this._htmls = new ConcurrentStack<Uri>();

            this._emails = new ConcurrentStack<string>();
        }

        public ConcurrentStack<Uri> Htmls { get => this._htmls; set => this._htmls = value; }

        public ConcurrentStack<string> Emails { get => this._emails; set => this._emails = value; }
    }

    class HtmlCollection
    {
        private volatile static Singleton _collectionHtml;

        private SiteParser _siteParser;

        private HtmlGetter _htmlGetter;

        private Uri _uri;

        public HtmlCollection()
        {
            HtmlCollection._collectionHtml = Singleton.Instance;

            this._siteParser = new SiteParser();

            this._htmlGetter = new HtmlGetter();
        }

        public HtmlCollection(Uri uri) : this()
        {
            this._uri = uri;
        }

        public async Task DeepAdd(Uri uri, int count)
        {
            if (this._uri == null || this.UriHaveCircle(uri)) return;

            var baseDomain = this._uri.Host.Replace("www.", string.Empty);

            var thisDomain = uri.Host.Replace("www.", string.Empty);

            var isbaseDomain = baseDomain.Contains(thisDomain) || thisDomain.Contains(baseDomain);

            if (isbaseDomain
                && !_collectionHtml.Htmls.Contains(uri) && _collectionHtml.Emails.Count < count)
            {
                Console.WriteLine(uri);

                _collectionHtml.Htmls.Push(uri);

                var html = await _htmlGetter.GetAsync(uri);

                var emails = _siteParser.GetEmail(html);

                foreach (var email in emails)
                {
                    this.AddEmail(email);
                }

                var urls = _siteParser.GetLinks(html, uri);

                foreach (var url in urls)
                {
                    if (Uri.TryCreate(url, UriKind.Absolute, out Uri u))
                    {
                        if (u.Scheme == Uri.UriSchemeHttp || u.Scheme == Uri.UriSchemeHttps)
                        {
                            await this.DeepAdd(u, count);
                        }
                        else if (u.Scheme == Uri.UriSchemeMailto)
                        {
                            var email = _siteParser.GetEmail(url).FirstOrDefault();

                            if (!string.IsNullOrWhiteSpace(email))
                            {
                                this.AddEmail(email);
                            }
                        }
                    }
                }
            }
        }

        private bool UriHaveCircle(Uri uri)
        {
            var haveCircle = uri.Query.Split('+')
                .GroupBy(query => query)
                .Any(group => group.Count() > 1);

            haveCircle |= uri.Query.Split('%')
                .GroupBy(query => query)
                .Any(group => group.Count() > 1);

            return haveCircle;
        }

        private void AddEmail(string email)
        {
            if (!_collectionHtml.Emails.Contains(email.ToLower()))
            {
                _collectionHtml.Emails.Push(email.ToLower());

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"{email} : {_collectionHtml.Emails.Count}");

                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}