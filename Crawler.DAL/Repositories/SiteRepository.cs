using Crawler.DAL.Context;
using System.Collections.Generic;
using System.Linq;
using Crawler.DAL.Entities;
using System.Data.SqlClient;
using Crawler.DAL.Interfaces;
using System.Text;

namespace Crawler.DAL.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        private SiteContext _db;

        public SiteRepository(SiteContext context)
        {
            _db = context;
        }

        public List<Site> Get(string searchString)
        {
            var site = _db.Sites;
            string query = "USE [Test] SELECT * from Sites where freetext(*,@Search)";
            var siteList = site.SqlQuery(query,new SqlParameter("Search", searchString)).ToList();
            return siteList;
        }

     
        public void Create(string link, string content, string title, string description,string image)
        {
            var site = new Site
            {
                Link = link,
                Title = title,
                Description = description,
                Image=image,
                Content = Encoding.GetEncoding(1251).GetBytes(content),
                ContentType = "htm"
            };
            _db.Sites.Add(site);
            _db.SaveChanges();

        }



    }
}
