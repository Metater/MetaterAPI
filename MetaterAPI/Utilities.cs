using System;
using System.Collections.Generic;
using System.Text;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;

namespace MetaterAPI
{
    public static class Utilities
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
}
