using System.Collections.Generic;
using Crawler.BLL.Interfaces;
using Crawler.DAL.Interfaces;
using Crawler.DAL.UoW;
using HtmlAgilityPack;
using System.Linq;
using System.Text.RegularExpressions;

namespace Crawler.BLL.Implementation
{
    public class Parser : IParser
    {
        private readonly IUnitOfWork _unitOfWork;

        public Parser()
        {
            _unitOfWork = new UnitOfWork("DefaultConnection");
        }
        public void Start(List<string> globalList)
        {

            foreach (var link in globalList)
            {
                try
                {

                    var site = GetSiteComponents(link);

                    _unitOfWork.Sites.Create(link, site[0], site[1], site[2], site[3]);

                }
                catch
                {
                    // ignored
                }
            }
            _unitOfWork.Urls.Update("Working", "Complete");
        }

        public List<string> GetSiteComponents(string link)
        {
            var siteList = new List<string> { "", "", "", "" };
            const string regularImage = @"(https?:\/\/.*\.(?:png|jpg|gif|webp))";
            const string regularJank = @"<.*?>|&.*?;";
            const string regularDomen = @"http(s)*:\/\/(www.)*|\.com.*";

            var doc = new HtmlWeb().Load(link);
            foreach (var script in doc.DocumentNode.Descendants("script").ToArray())
                script.Remove();
            foreach (var style in doc.DocumentNode.Descendants("style").ToArray())
                style.Remove();
            var temp = Regex.Replace(doc.DocumentNode.InnerText, regularJank, string.Empty);
            siteList[0] = temp.Substring(1);

            temp = (from x in doc.DocumentNode.Descendants().AsParallel()
                    where x.Name.ToLower() == "title"
                    select x.InnerText).FirstOrDefault();
            if (temp != null)
                siteList[1] = Regex.Replace(temp, regularJank, string.Empty);

            temp = (from x in doc.DocumentNode.Descendants().AsParallel()
                    where x.Name.ToLower() == "meta"
                          && x.Attributes["name"] != null
                          && x.Attributes["name"].Value.ToLower() == "description"
                    select x.Attributes["content"].Value).FirstOrDefault();
            if (temp != null)
                siteList[2] = Regex.Replace(temp, regularJank, string.Empty);

            temp = (from x in doc.DocumentNode.Descendants().AsParallel()
                    where x.Name.ToLower() == "meta" && x.Attributes["property"] != null
                          && x.Attributes["property"].Value.ToLower() == "og:image"
                    select x.Attributes["content"].Value).FirstOrDefault();
            if (temp != null)
                siteList[3] = Regex.Match(temp, regularImage).Groups[1].Value;

            temp = link;
            var domen = Regex.Replace(temp, regularDomen, string.Empty);
            switch (domen)
            {
                case "facebook": siteList[3] = "/Content/Images/fb_logo.jpg"; break;
                case "twitter": siteList[3] = "/Content/Images/tw_logo.jpg"; break;
                case "instagram": siteList[3] = "/Content/Images/inst_logo.jpg"; break;
                default: break;
            }

            if ((siteList[3].Contains(".svg")) || (siteList[3] == ""))
            {
                siteList[3] = "/Content/Images/Default.jpg";
            }


            return siteList;
        }
    }
}
