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
            using (RestServer server = new RestServer()) { Utilities.Server.StartRestServer(server); }
        }
    }
    [RestResource]
    public class Notes
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/notescreate")]
        public IHttpContext Create(IHttpContext context)
        {
            return context;
        }
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/noteslist")]
        public IHttpContext List(IHttpContext context)
        {
            return context;
        }
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/notesget")]
        public IHttpContext Get(IHttpContext context)
        {
            return context;
        }
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/notesadd")]
        public IHttpContext Add(IHttpContext context)
        {
            
            AddNotesFromURLToID()
            return context;
        }
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/notesdelete")]
        public IHttpContext Delete(IHttpContext context)
        {
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