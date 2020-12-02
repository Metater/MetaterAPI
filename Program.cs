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
        public IHttpContext Home(IHttpContext context)
        {
            if ((context.Request.QueryString["for"] != null) && (context.Request.QueryString["name"] == null))
            {
                AddChat(context.Request.QueryString["for"]);
                context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n");
                return context;
            }
            else if (context.Request.QueryString["aud"] != null)
            {
                context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n");
                return context;
            }
            else if ((context.Request.QueryString["name"] != null) && (context.Request.QueryString["for"] != null))
            {
                AddChat("<" + context.Request.QueryString["name"] + "> " + context.Request.QueryString["for"]);
                context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n");
            }
            else if (context.Request.QueryString["for"] == null)
            {
                context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n");
                return context;
            }
            context.Response.SendResponse("400");
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/clear")]
        public IHttpContext Clear(IHttpContext context)
        {
            ClearChat();
            context.Response.SendResponse("200");
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