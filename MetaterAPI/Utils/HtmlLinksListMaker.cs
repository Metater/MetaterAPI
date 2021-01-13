using System;
using System.Collections.Generic;
using System.Text;

namespace MetaterAPI.Utils
{
    public class HtmlLinksListMaker
    {
        public HtmlLinksList htmlLinksList = new HtmlLinksList();
        #region Constructors
        /// <summary>
        /// To make a brand new HtmlLinksList from nothing
        /// </summary>
        public HtmlLinksListMaker()
        {
            
        }
        /// <summary>
        /// To input an existing HtmlLinksList, for modification
        /// </summary>
        public HtmlLinksListMaker(string data)
        {
            htmlLinksList.FromHtml(data);
        }
        #endregion Constructors
        public void AddLinkToLinkList(string list, (string, string) linkAndLinkName)
        {
            bool listMatch = false;
            foreach(HtmlLinks htmlLinks in htmlLinksList.htmlLinksList)
            {
                if (htmlLinks.name == list)
                {
                    htmlLinks.htmlLinkList.Add(linkAndLinkName);
                    listMatch = true;
                }
            }
            if (!listMatch)
            {
                HtmlLinks htmlLinks = new HtmlLinks();
                htmlLinks.name = list;
                htmlLinks.htmlLinkList.Add(linkAndLinkName);
                htmlLinksList.htmlLinksList.Add(htmlLinks);
            }
        }
        public void RemoveLinkFromLinkList(string list, string linkName)
        {
            foreach (HtmlLinks htmlLinks in htmlLinksList.htmlLinksList)
            {
                if (htmlLinks.name == list)
                {
                    foreach ((string, string) linkAndLinkName in htmlLinks.htmlLinkList)
                    {
                        if (linkAndLinkName.Item2 == linkName)
                        {
                            htmlLinks.htmlLinkList.Remove(linkAndLinkName);
                            if (htmlLinks.htmlLinkList.Count.Equals(0))
                                htmlLinksList.htmlLinksList.Remove(htmlLinks);
                            return;
                        }
                    }
                }
            }
        }
    }
    public class HtmlLinksList
    {
        public string title;
        public List<HtmlLinks> htmlLinksList = new List<HtmlLinks>();
        public HtmlLinksList()
        {

        }
        public HtmlLinksList(string title, List<HtmlLinks> htmlLinksList)
        {
            this.title = title;
            this.htmlLinksList = htmlLinksList;
        }
        public void FromHtml(string data)
        {
            string[] dataLines = data.Split("\n");

            title = dataLines[5].Split("\"")[3];
            string[] choppedDataLines = Utils.ArrayHelper.ChopEnds(dataLines, 9, 2);
            for (int i = 0; i < choppedDataLines.Length; i++)
            {
                if (choppedDataLines[i].Contains("<h3>"))
                {
                    string htmlLinksRaw = "";
                    while (!choppedDataLines[i].Contains("</ul>"))
                    {
                        htmlLinksRaw += choppedDataLines[i] + "\n";
                        i++;
                    }
                    htmlLinksRaw += choppedDataLines[i];
                    HtmlLinks htmlLinks = new HtmlLinks();
                    htmlLinks.FromHtml(htmlLinksRaw);
                    htmlLinksList.Add(htmlLinks);
                }
            }
        }
        public string ToHtml()
        {
            string html = "";
            html += "<!doctype html>\n";
            html += "<html lang=\"en\">\n";
            html += "<head>\n";
            html += "<meta charset=\"utf-8\">\n";
            html += $"<title>{title}</title>\n";
            html += $"<meta name=\"description\" content=\"{title}\">\n";
            html += "</head>\n";
            html += "<body>\n";
            html += $"<h1>{title}</h1>\n";
            foreach(HtmlLinks htmlLinks in htmlLinksList)
            {
                html += htmlLinks.ToHtml();
            }
            html += "</body>\n";
            html += "</html>";
            return html;
        }
    }
    public class HtmlLinks
    {
        public string name;
        // link, name
        //public Dictionary<string, string> htmlLinkList = new Dictionary<string, string>();
        public List<(string, string)> htmlLinkList = new List<(string, string)>();
        public void FromHtml(string data)
        {
            GetLinksAndListName(data);
        }
        private void GetLinksAndListName(string data)
        {
            string[] dataLines = data.Split("\n");
            foreach(string dataLine in dataLines)
            {
                if (dataLine.Contains("<li>"))
                {
                    string link = dataLine.Split("\"")[3];
                    string name = GetLinkName(dataLine);
                    htmlLinkList.Add((link, name));
                }
                else if (dataLine.Contains("<h3>"))
                {
                    name = dataLine.Substring(4, dataLine.Length - 9);
                }
            }
        }
        private static string GetLinkName(string dataLine)
        {
            string uncroppedLinkName = dataLine.Split(">")[2];
            return uncroppedLinkName.Substring(0, uncroppedLinkName.Length - 3);
        }
        public string ToHtml()
        {
            string html = "";
            html += "<h3>" + name + "</h3>\n";
            html += "<ul>\n";
            foreach ((string, string) linkAndNameTuple in htmlLinkList)
            {
                html += "<li><a target=\"_blank\" href=\"" + linkAndNameTuple.Item1 + "\">" + linkAndNameTuple.Item2 + "</a></li>\n";
            }
            html += "</ul>\n";
            return html;
        }
    }
}
