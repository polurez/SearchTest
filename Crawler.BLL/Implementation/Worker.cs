using Crawler.BLL.Interfaces;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Crawler.BLL.Implementation
{
    public class Worker : IWorker
    {
        public List<string> Start(List<string> globalList, int deep, int width)
        {
            var uberList = new List<string>();
            var localList = new List<string>();
            foreach (var globalLink in globalList)
            {
                try
                {
                    WebClient wc = new WebClient();
                    string str = wc.DownloadString(globalLink);
                    HtmlDocument htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(str);
                    localList = htmlDocument.DocumentNode.Descendants("a")
                        .Take(width)
                        .AsParallel()
                        .Where(link => link.Attributes.Contains("href")
                                       && link.Attributes["href"].Value != null
                                       && ((link.Attributes["href"].Value.Contains("http")
                                            || (link.Attributes["href"].Value.Contains("https")))))
                        .Select(link => link.Attributes["href"].Value)
                        .ToList();
                    uberList.AddRange(localList);
                }
                catch
                {
                    // ignored
                }
                if (deep > 1)
                {
                    deep--;
                    foreach (var link in Start(localList, deep, width))
                    {
                        if (!(uberList.Contains(link)))
                            uberList.Add(link);
                    }
                }
            }
            return uberList;
        }
    }
}
