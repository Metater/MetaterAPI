using System;
using System.Collections.Generic;
using System.Text;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;

namespace MetaterAPI.Notes
{
    [RestResource]
    public class Notes
    {
        /*
         * URL format: http://api.--------.tk:5000/notes?a=ACTION&id=ID
        */
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/notes")]
        public IHttpContext Router(IHttpContext context)
        {
            List<string> actions = new List<string>{ "get", "create", "delete", "list", "add", "remove" };
            string action = Utils.QueryString.GetMatchingQueryStringKeyFromNameList(context, "a", actions);
            Console.WriteLine(action);
            bool containsId = context.Request.QueryString["id"] != null;
            if (action != null && containsId)
            {
                switch(action)
                {
                    case "get":
                        context = Get(context);
                        break;
                    case "create":
                        context = Create(context);
                        break;
                    case "delete":
                        context = Delete(context);
                        break;
                    case "list":
                        context = List(context);
                        break;
                    case "add":
                        context = Add(context);
                        break;
                    case "remove":
                        context = Remove(context);
                        break;
                }
            }
            else
            {

            }
            return context;
        }
        public IHttpContext Get(IHttpContext context)
        {
            Console.WriteLine("Get");
            return context;
        }
        public IHttpContext Create(IHttpContext context)
        {
            Console.WriteLine("Create");
            return context;
        }
        public IHttpContext Delete(IHttpContext context)
        {
            Console.WriteLine("Delete");
            return context;
        }
        public IHttpContext List(IHttpContext context)
        {
            Console.WriteLine("List");
            return context;
        }
        public IHttpContext Add(IHttpContext context)
        {
            Console.WriteLine("Add");
            return context;
        }
        public IHttpContext Remove(IHttpContext context)
        {
            Console.WriteLine("Remove");
            return context;
        }
        public static void AddNotesFromURLToID(string url, string id)
        {
            Console.WriteLine(Utils.HttpRequests.GetRawString(url));
        }
        public static string GetNotesWithID(string id)
        {
            return "";
        }
    }
}
