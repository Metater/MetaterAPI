using System;
using System.Collections.Generic;
using System.Text;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;

namespace MetaterAPI.GeneralResources
{
    [RestResource]
    public class Home
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/")]
        public IHttpContext Response(IHttpContext context)
        {
            Console.WriteLine(context.Request.Url.UserInfo);
            //context.Response.AppendCookie(new System.Net.Cookie(new System.Random().Next(0, 20000).ToString(), "ME LOVE COOKIES"));
            context.Response.SendResponse(Utils.IO.GetFile("base.txt"));
            return context;
        }
    }
}
