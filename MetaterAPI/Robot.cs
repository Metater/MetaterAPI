using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;
using System;
using System.Text;
using MetaterAPI.Utils;

namespace MetaterAPI
{
    [RestResource]
    public class Robot
    {
        int leftSpeed = 90;
        int rightSpeed = 90;

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/getrobot")]
        public IHttpContext GetRobotSpeeds(IHttpContext context)
        {
            context.Response.SendResponse(leftSpeed + ":" + rightSpeed);
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/setrobot")]
        public IHttpContext SetRobotSpeeds(IHttpContext context)
        {
            bool valid = false;

            if (QueryString.ContainsQueryString(context, "left") && QueryString.ContainsQueryString(context, "right"))
            {
                if (int.TryParse(context.Request.QueryString["left"], out leftSpeed))
                {
                    if (int.TryParse(context.Request.QueryString["right"], out rightSpeed))
                    {
                        valid = true;
                    }
                }
            }
            if (!valid)
            {
                context.Response.SendResponse("Invalid, Example: http://api.metater.tk:5000/setweather?left=90&right=90");
            }
            else
            {
                context.Response.SendResponse($"Left Speed: {leftSpeed}\nRight Speed: {rightSpeed}");
            }
            return context;
        }
    }
}
