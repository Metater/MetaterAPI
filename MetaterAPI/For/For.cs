using System;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace MetaterAPI.For
{
    //[RestResource]
    public class For
    {
        string chatDataPath = Directory.GetCurrentDirectory() + @"\chat.txt";
        List<string> fclearKeys = new List<string>(Utils.IO.GetLines("fclearKeys.txt"));

        //[RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/f")]
        public IHttpContext Router(IHttpContext context)
        {
            Console.WriteLine(context.Request.RawUrl);

            string noQueryArgs = Utils.QueryString.IfNoQueryArgs(context);
            if (noQueryArgs == null)
            {
                context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n");
                return context;
            }

            string fQuery = Utils.QueryString.IfQueryStringMatch(context, "f");
            string nQuery = Utils.QueryString.IfQueryStringMatch(context, "n");
            if ((fQuery != null) && (nQuery != null))
            {
                if (context.Request.QueryString["f"] != "")
                    AddChat("<" + context.Request.QueryString["n"] + "> " + context.Request.QueryString["f"]);
                context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n");
                return context;
            }
            string forQuery = Utils.QueryString.IfQueryStringMatch(context, "for");
            string nameQuery = Utils.QueryString.IfQueryStringMatch(context, "name");
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

        //[RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/fclear")]
        public IHttpContext Clear(IHttpContext context)
        {
            string fclearKey = Utils.QueryString.IfOneQueryStringAndMatch(context, "key");
            if (fclearKey != null && fclearKeys.Contains(fclearKey))
            {
                ClearChat();
                context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n" + "Clear success!");
            }
            else
            {
                context.Response.SendResponse(GetChat() + "Last loaded at: " + DateTime.Now.ToString("hh:mm:ss tt") + "\n" + "Clear failed, no permission!");
            }
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
