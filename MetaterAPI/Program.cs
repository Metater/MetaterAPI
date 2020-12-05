using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;
using Grapevine.Client;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MetaterAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server = new RestServer())
            {
                server.Host = "+";
                //server.Host = "localhost";
                server.Port = "5000";
                server.LogToConsole().Start();
                Console.ReadLine();
                server.Stop();
            }
        }
    }
    [RestResource]
    public class Main
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/")]
        public IHttpContext Base(IHttpContext context)
        {
            context.Response.SendResponse(GetFile("base.txt"));
            return context;
        }
        public string GetFile(string localPath)
        {
            return File.ReadAllText(Directory.GetCurrentDirectory() + @"\" + localPath);
        }
    }
    [RestResource]
    public class MCServerData
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/mc")]
        public IHttpContext MC(IHttpContext context)
        {
            Console.WriteLine(context.Request.RawUrl);
            context.Response.SendResponse("Welcome!");
            return context;
        }
    }
}