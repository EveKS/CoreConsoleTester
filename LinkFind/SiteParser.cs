using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LinkFind
{
    class SiteParser
    {
        public IEnumerable<string> GetEmail(string html)
        {
            Regex reEmail = new Regex(@"(?inx)
             \w+([-+.]\w+)
                *@\w+([-.]\w+)
            *\.\w+([-.]\w+)*");

            return reEmail.Matches(html).OfType<Match>()
                .Select(match =>
                {
                    var url = match.Value.ToString();

                    return url;
                });
        }

        public IEnumerable<String> GetLinks(string html, Uri uri)
        {
            Regex reHref = new Regex(@"(?inx)
                <a \s [^>]*
                    href \s* = \s*
                        (?<q> ['""] )
                            (?<url> [^""]+ )
                        \k<q>
                [^>]* >");

            return reHref.Matches(html).OfType<Match>()
                .Select(match =>
                {
                    var url = match.Groups["url"].ToString();

                    if (url.FirstOrDefault() == '/')
                    {
                        url = uri.GetLeftPart(UriPartial.Authority) + url;
                    }

                    return url;
                });
        }
    }
}