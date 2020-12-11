using System;
using System.Collections.Generic;
using System.Text;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;
using System.IO;
using HtmlAgilityPack;

namespace MetaterAPI
{
    public static class Utilities
    {
        public static class Server
        {
            public static void StartRestServer(RestServer server)
            {
                bool inputValid = false;
                char input;
                while (!inputValid)
                {
                    Console.WriteLine("Release or Debug? (R||D)");
                    input = Console.ReadKey().KeyChar;
                    if (input == 'R' || input == 'r')
                    {
                        server.Host = "+";
                        inputValid = true;
                        Console.WriteLine("");
                    }
                    if (input == 'D' || input == 'd')
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
        public static class IO
        {
            public static string FormatPath(string localPath) { return Directory.GetCurrentDirectory() + @"\" + localPath; }
            public static string GetFile(string localPath) { return File.ReadAllText(FormatPath(localPath)); }
            public static string[] GetFilesInDirectory(string localPath) { return Directory.GetFiles(FormatPath(localPath)); }
            public static string[] GetLines(string localPath) { return File.ReadAllLines(FormatPath(localPath)); }
            public static void AddLine(string localPath, string line)
            {
                string text = GetFile(localPath);
                text += line + "\n";
                File.WriteAllText(FormatPath(localPath), text);
            }
        }
        public static class HttpRequests
        {
            public static string GetRawStringFromURL(string url)
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                return doc.ToString();
            }
        }
    }
}
