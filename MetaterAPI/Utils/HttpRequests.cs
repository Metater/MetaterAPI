using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace MetaterAPI.Utils
{
    public static class HttpRequests
    {
        public static string GetRawString(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc.ToString();
        }
        public static HtmlDocument GetHtmlDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(url);
        }
    }
}
