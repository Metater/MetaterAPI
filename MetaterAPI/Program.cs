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
using HtmlAgilityPack;
using System.Xml.XPath;

namespace MetaterAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server = new RestServer())
            {
                bool inputValid = false;
                char input;
                while(!inputValid)
                {
                    Console.WriteLine("Release or Debug? (R||D)");
                    input = Console.ReadKey().KeyChar;
                    if (input == 'R')
                    {
                        server.Host = "+";
                        inputValid = true;
                        Console.WriteLine("");
                    }
                    if (input == 'D')
                    {
                        server.Host = "localhost";
                        inputValid = true;
                        Console.WriteLine("");
                    }
                }
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
        public static string GetFile(string localPath)
        {
            return File.ReadAllText(Directory.GetCurrentDirectory() + @"\" + localPath);
        }
        public static string[] GetLines(string localPath)
        {
            return File.ReadAllLines(Directory.GetCurrentDirectory() + @"\" + localPath);
        }
        public static void AddLine(string localPath, string line)
        {
            string text = GetFile(localPath);
            text += line + "\n";
            File.WriteAllText(Directory.GetCurrentDirectory() + @"\" + localPath, text);
        }
    }
    [RestResource]
    public class GetMCData
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/mc")]
        public IHttpContext Get(IHttpContext context)
        {
            context.Response.SendResponse(Main.GetFile("dataLog.txt"));
            return context;
        }
    }
    [RestResource]
    public class DataRecieving
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/data")]
        public IHttpContext Data(IHttpContext context)
        {
            if (context.Request.QueryString["data"] != null)
            {
                Main.AddLine("dataLog.txt", context.Request.QueryString["data"]);
            }
            context.Response.SendResponse("200");
            return context;
        }
    }
    [RestResource]
    public class VerseOfTheDay
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/votd")]
        public IHttpContext Get(IHttpContext context)
        {
            var url = "https://www.bible.com/verse-of-the-day";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            XPathNavigator nodeVerseText = doc.CreateNavigator().SelectSingleNode("/html/body/div[1]/div[1]/div/div/div[1]/div[1]/p[1]/text()");
            XPathNavigator nodeVerse = doc.CreateNavigator().SelectSingleNode("/html/body/div[1]/div[1]/div/div/div[1]/div[1]/p[2]/text()");
            context.Response.SendResponse(nodeVerseText.Value + "\n" + nodeVerse.Value);

            return context;
        }
    }
}