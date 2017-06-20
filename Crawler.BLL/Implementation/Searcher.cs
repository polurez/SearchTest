using System.Collections.Generic;
using Crawler.BLL.Interfaces;
using Crawler.DAL.Interfaces;
using Crawler.DAL.UoW;

namespace Crawler.BLL.Implementation
{
    public class Searcher : ISearcher
    {
        private readonly IUnitOfWork _unitOfWork;

        public Searcher()
        {
            _unitOfWork = new UnitOfWork("DefaultConnection");
        }

        public string Add(string url, string userId)
        {
            if ((url == "") || (url == null)) return null;
            if (userId == null) return "Only authorized users can add a link";
            _unitOfWork.Urls.Create(url);
            return "Link : " + url + " successfuly added";
        }

        public Dictionary<string, List<string>> Find(string searchString)
        {
            var temp = _unitOfWork.Sites.Get(searchString);

            var lists = new Dictionary<string, List<string>>()
            { {"Link", new List<string>() },
              {"Title", new List<string>() },
              {"Description", new List<string>() },
              {"Image", new List<string>() } };

            foreach (var site in temp)
            {
                lists["Link"].Add(site.Link);
                lists["Title"].Add(site.Title);
                lists["Description"].Add(site.Description);
                lists["Image"].Add(site.Image);
            }           
            return lists;
        }

      
    }
}
