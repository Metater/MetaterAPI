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
    public class DataRecieving
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/data")]
        public IHttpContext Data(IHttpContext context)
        {
            if (context.Request.QueryString["data"] != null)
            {
                Utils.IO.AddLine("dataLog.txt", context.Request.QueryString["data"]);
            }
            context.Response.SendResponse("200");
            return context;
        }
    }
}
