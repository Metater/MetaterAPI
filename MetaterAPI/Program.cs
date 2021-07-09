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
using System.Security.Cryptography;

namespace MetaterAPI
{
    class Program
    {
        static void Main(string[] args)
        { 
            //RSAParameters rsaParams = MetaMit.Cryptography.Asymmetric.Generation.GenRSAParams(2048);
            //Console.WriteLine(MetaMit.Cryptography.Asymmetric.Collection.GetPublicKey(rsaParams));
            //Console.WriteLine(MetaMit.Cryptography.Asymmetric.Collection.GetPrivateKey(rsaParams));
            using (RestServer server = new RestServer()) { Utils.Server.StartRestServer(server); }
        }
    }
}