using System;
using System.Collections.Generic;
using System.Text;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Interfaces.Server;
using Grapevine.Shared;
using HtmlAgilityPack;
using System.Xml.XPath;

namespace MetaterAPI.Resources
{
    //[RestResource]
    public class Zaid
    {
        //[RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/zaid/add")]
        public IHttpContext Add(IHttpContext context)
        {
            string msg = Utils.QueryString.IfOneQueryStringAndMatch(context, "msg");
            if (msg != null)
            {
                List<Block> blockchain = GetChain();
                Console.WriteLine("Adding new block!");
                string prevHash = "";
                if (blockchain.Count != 0) prevHash = blockchain[blockchain.Count-1].hash;
                Block block = new Block(msg, blockchain.Count, prevHash);
                AddBlock(block);
                Console.WriteLine(blockchain.Count);
                context.Response.SendResponse("200");
            }
            else context.Response.SendResponse("400");
            return context;
        }
        //[RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/zaid/get")]
        public IHttpContext Get(IHttpContext context)
        {
            string response = "";
            foreach(Block block in GetChain())
            {
                response += "--------------------------------\n";
                response += "ID: " + block.id + "\n";
                response += "Birth: " + block.birth + "\n";
                response += "Msg: " + block.msg + "\n";
                response += "BirthHash: " + block.birthHash + "\n";
                response += "MsgHash: " + block.msgHash + "\n";
                response += "PrevHash: " + block.prevHash + "\n";
                response += "Hash: " + block.hash + "\n";
            }
            context.Response.SendResponse(response);
            return context;
        }
        public class Block
        {
            public int id;
            public long birth;
            public string msg;

            public string idHash;
            public string birthHash;
            public string msgHash;

            public string prevHash;
            public string hash;

            public Block(string msg, int blockCount, string prevHash)
            {
                Console.WriteLine(id);
                id = blockCount;
                birth = DateTimeOffset.Now.ToUnixTimeSeconds();
                this.msg = msg;

                idHash = Utils.Hash.GetHashString(id.ToString());
                birthHash = Utils.Hash.GetHashString(birth.ToString());
                msgHash = Utils.Hash.GetHashString(msg);

                this.prevHash = prevHash;
                hash = Utils.Hash.GetHashString(idHash + birthHash + msgHash + prevHash);
            }
            public Block(int id, long birth, string msg, string idHash, string birthHash, string msgHash, string prevHash, string hash)
            {
                this.id = id;
                this.birth = birth;
                this.msg = msg;
                this.idHash = idHash;
                this.birthHash = birthHash;
                this.msgHash = msgHash;
                this.prevHash = prevHash;
                this.hash = hash;
            }
        }
        public List<Block> GetChain()
        {
            List<Block> blockchain = new List<Block>();
            foreach(string block in Utils.IO.GetLines("chain.zaid"))
            {
                blockchain.Add(GetBlock(block));
            }
            return blockchain;
        }
        public void AddBlock(Block block)
        {
            Utils.IO.AddLine("chain.zaid", ToCSVBlock(block));
        }
        public string ToCSVBlock(Block block)
        {
            string csv = "";
            csv += block.id.ToString() + ",";
            csv += block.birth.ToString() + ",";
            csv += block.msg + ",";
            csv += block.idHash + ",";
            csv += block.birthHash + ",";
            csv += block.msgHash + ",";
            csv += block.prevHash + ",";
            csv += block.hash;
            return csv;
        }
        public Block GetBlock(string block)
        {
            string[] blockData = block.Split(",");
            return new Block(Int32.Parse(blockData[0]), Int64.Parse(blockData[1]), blockData[2], blockData[3], blockData[4], blockData[5], blockData[6], blockData[7]);
        }
    }
}
