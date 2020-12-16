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
    public class MCData
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/mc")]
        public IHttpContext Get(IHttpContext context)
        {
            context.Response.SendResponse(Utils.IO.GetFile("dataLog.txt"));
            return context;
        }
    }
}
