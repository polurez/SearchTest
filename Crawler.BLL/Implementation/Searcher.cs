using System.Collections.Generic;
using Crawler.BLL.Interfaces;
using Crawler.DAL.Interfaces;
using Crawler.DAL.UoW;
using Crawler.BLL.DTO;
using Crawler.DAL.Entities;
using AutoMapper;

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

        public List<SiteDTO> Find(string searchString)
        {
            
            Mapper.Initialize(cfg => cfg.CreateMap<Site, SiteDTO>());
            return Mapper.Map<List<Site>, List<SiteDTO>>(_unitOfWork.Sites.Get(searchString));
        }

      
    }
}
