using System;
using System.Collections.Generic;
using System.Text;
using Grapevine.Server;

namespace MetaterAPI.Utils
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
}
