using System;
using System.Collections.Generic;
using System.Text;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;

namespace MetaterAPI.Resources
{
    [RestResource]
    public class FRCUsefulLinks
    {
        List<string> keys = new List<string>(Utils.IO.GetLines("keys.txt"));


        // May want to implement swap or rename method

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/frc")]
        public IHttpContext Response(IHttpContext context)
        {
            context.Response.SendResponse(Utils.IO.GetFile("frc.txt"));
            return context;
        }
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/frc/add")]
        public IHttpContext AddLink(IHttpContext context)
        {
            // Refactor with guard clauses for cleaner code, and less layers
            if (AddLinkValid(context))
            {
                if (keys.Contains(context.Request.QueryString["key"]))
                {
                    string data = Utils.IO.GetFileAbsolute(Utils.IO.GetFile("frchtmlpath.txt"));
                    Utils.HtmlLinksListMaker htmlLinksListMaker = new Utils.HtmlLinksListMaker(data);
                    string cate = context.Request.QueryString["cate"];
                    string link = context.Request.QueryString["link"];
                    string desc = context.Request.QueryString["desc"];
                    htmlLinksListMaker.AddLinkToLinkList(cate, (link, desc));
                    Utils.IO.SetFileAbsolute(Utils.IO.GetFile("frchtmlpath.txt"), htmlLinksListMaker.htmlLinksList.ToHtml());
                    context.Response.SendResponse("Success!");
                }
                else
                {
                    context.Response.SendResponse("Key is not valid!");
                }
            }
            else
            {
                context.Response.SendResponse("Not formatted correctly!\nExample: http://api.metater.tk:5000/frc/add?key=YOUR_API_KEY&cate=Example Category&link=http://example.com/&desc=Example Website");
            }
            return context;
        }
        private bool AddLinkValid(IHttpContext context)
        {
            bool key = Utils.QueryString.ContainsQueryString(context, "key");
            bool cate = Utils.QueryString.ContainsQueryString(context, "cate");
            bool link = Utils.QueryString.ContainsQueryString(context, "link");
            bool desc = Utils.QueryString.ContainsQueryString(context, "desc");
            return key && cate && link && desc;
        }
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/frc/remove")]
        public IHttpContext RemoveLink(IHttpContext context)
        {
            // Refactor with guard clauses for cleaner code, and less layers
            if (RemoveLinkValid(context))
            {
                if (keys.Contains(context.Request.QueryString["key"]))
                {
                    string data = Utils.IO.GetFileAbsolute(Utils.IO.GetFile("frchtmlpath.txt"));
                    Utils.HtmlLinksListMaker htmlLinksListMaker = new Utils.HtmlLinksListMaker(data);
                    string cate = context.Request.QueryString["cate"];
                    string desc = context.Request.QueryString["desc"];
                    htmlLinksListMaker.RemoveLinkFromLinkList(cate, desc);
                    Utils.IO.SetFileAbsolute(Utils.IO.GetFile("frchtmlpath.txt"), htmlLinksListMaker.htmlLinksList.ToHtml());
                    context.Response.SendResponse("Success!");
                }
                else
                {
                    context.Response.SendResponse("Key is not valid!");
                }
            }
            else
            {
                context.Response.SendResponse("Not formatted correctly!\nExample: http://api.metater.tk:5000/frc/add?key=YOUR_API_KEY&cate=Example Category&desc=Example Website");
            }
            return context;
        }
        private bool RemoveLinkValid(IHttpContext context)
        {
            bool key = Utils.QueryString.ContainsQueryString(context, "key");
            bool cate = Utils.QueryString.ContainsQueryString(context, "cate");
            bool desc = Utils.QueryString.ContainsQueryString(context, "desc");
            return key && cate && desc;
        }
    }
}