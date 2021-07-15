using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;
using System;
using System.Text;
using MetaterAPI.Utils;
using System.IO;
using NetCoreServer;

namespace MetaterAPI
{
    [RestResource]
    public class Robot
    {
        string robotSpeedsDBPath => Directory.GetCurrentDirectory() + @"\robotSpeeds.db";
        string localRobotIPDBPath => Directory.GetCurrentDirectory() + @"\localRobotIP.db";

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/postlocalrobotip")]
        public IHttpContext PostLocalRobotIP(IHttpContext context)
        {
            if (QueryString.ContainsQueryString(context, "ip"))
            {
                SetLocalRobotIP(context.Request.QueryString["ip"]);
            }
            context.Response.SendResponse("200");
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/getlocalrobotip")]
        public IHttpContext GetLocalRobotIP(IHttpContext context)
        {
            context.Response.SendResponse(GetLocalRobotIP());
            return context;
        }


        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/getrobot")]
        public IHttpContext GetRobotSpeeds(IHttpContext context)
        {
            (int, int) robotSpeeds = GetRobotSpeeds();

            context.Response.SendResponse(robotSpeeds.Item1 + ":" + robotSpeeds.Item2);
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/setrobot")]
        public IHttpContext SetRobotSpeeds(IHttpContext context)
        {
            int leftSpeed = 90;
            int rightSpeed = 90;

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
                SetRobotSpeeds(leftSpeed, rightSpeed);
                context.Response.SendResponse($"Left Speed: {leftSpeed}\nRight Speed: {rightSpeed}");
            }
            return context;
        }

        private (int, int) GetRobotSpeeds()
        {
            if (!File.Exists(robotSpeedsDBPath))
            {
                File.WriteAllText(robotSpeedsDBPath, "90\n90");
            }

            string robotSpeeds = File.ReadAllText(robotSpeedsDBPath);
            string[] robotSpeedsSplit = robotSpeeds.Split('\n');
            return (int.Parse(robotSpeedsSplit[0]), int.Parse(robotSpeedsSplit[1]));
        }

        private void SetRobotSpeeds(int left, int right)
        {
            File.WriteAllText(robotSpeedsDBPath, left + "\n" + right);
        }

        private string GetLocalRobotIP()
        {
            if (!File.Exists(localRobotIPDBPath))
            {
                return null;
            }
            return File.ReadAllText(localRobotIPDBPath);
        }

        private void SetLocalRobotIP(string ip)
        {
            File.WriteAllText(robotSpeedsDBPath, ip);
        }
    }
}
