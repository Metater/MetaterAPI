using System;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;
using System.IO;

namespace MetaterAPI.For
{
    [RestResource]
    public class Chat
    {
        string chatDataPath = Directory.GetCurrentDirectory() + @"\chat.txt";

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/f")]
        public IHttpContext Router(IHttpContext context)
        {
            Console.WriteLine(context.Request.RawUrl);

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
                if (context.Request.QueryString["for"] != "")
                    AddChat("<" + context.Request.QueryString["name"] + "> " + context.Request.QueryString["for"]);
                context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n");
                return context;
            }

            context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n");
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
            string chat = File.ReadAllText(chatDataPath);
            return chat;
        }
        private void SaveChat(string chat)
        {
            File.WriteAllText(chatDataPath, chat);
        }
        private void ClearChat()
        {
            File.WriteAllText(chatDataPath, "");
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
