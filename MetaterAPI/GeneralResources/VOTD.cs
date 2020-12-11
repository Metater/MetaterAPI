using System;
using System.Collections.Generic;
using System.Text;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;
using HtmlAgilityPack;
using System.Xml.XPath;

namespace MetaterAPI.GeneralResources
{
    [RestResource]
    public class VOTD
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/votd")]
        public IHttpContext Get(IHttpContext context)
        {
            HtmlDocument doc = Utils.HttpRequests.GetHtmlDocument("https://www.bible.com/verse-of-the-day");
            XPathNavigator nodeVerseText = doc.CreateNavigator().SelectSingleNode("/html/body/div[1]/div[1]/div/div/div[1]/div[1]/p[1]/text()");
            XPathNavigator nodeVerse = doc.CreateNavigator().SelectSingleNode("/html/body/div[1]/div[1]/div/div/div[1]/div[1]/p[2]/text()");
            context.Response.SendResponse(nodeVerseText.Value + "\n" + nodeVerse.Value);

            return context;
        }
    }
}
