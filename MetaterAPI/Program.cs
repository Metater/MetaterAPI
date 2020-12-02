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
    public class MCServerData
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/mc")]
        public IHttpContext MC(IHttpContext context)
        {
            context.Response.SendResponse("");
            return context;
        }
    }
    [RestResource]
    public class For
    {
        string chatData = Directory.GetCurrentDirectory() + @"\chat.txt";

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/")]
        public IHttpContext ForBase(IHttpContext context)
        {
            string deprecatedFor = Utilities.IfQueryStringMatchAndNoOtherQueryString(context, "for", "name");
            if (deprecatedFor != null)
            {
                AddChat(context.Request.QueryString["for"]);
                context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n");
                return context;
            }

            string noQueryArgs = Utilities.IfNoQueryArgs(context);
            if (noQueryArgs == null)
            {
                context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n");
                return context;
            }

            string forQuery = Utilities.IfQueryStringMatch(context, "for");
            string nameQuery = Utilities.IfQueryStringMatch(context, "name");
            if ((forQuery != null) && (nameQuery != null))
            {
                AddChat("<" + context.Request.QueryString["name"] + "> " + context.Request.QueryString["for"]);
                context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n");
                return context;
            }

            context.Response.SendResponse("If not intended, tell me it's broken.");
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/clear")]
        public IHttpContext Clear(IHttpContext context)
        {
            ClearChat();
            context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n");
            return context;
        }

        private string GetChat()
        {
            string chat = File.ReadAllText(chatData);
            return chat;
        }
        private void SaveChat(string chat)
        {
            File.WriteAllText(chatData, chat);
        }
        private void ClearChat()
        {
            File.WriteAllText(chatData, "");
        }
        private void AddChat(string line)
        {
            line = DateTime.Now.ToString("hh:mm tt") + " " + line;
            string chat = GetChat();
            chat += line + "\n";
            SaveChat(chat);
        }
    }
}