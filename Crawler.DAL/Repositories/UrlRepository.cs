using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Crawler.DAL.Context;
using Crawler.DAL.Entities;
using Crawler.DAL.Interfaces;

namespace Crawler.DAL.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private SiteContext _db;

        public UrlRepository(SiteContext context)
        {
            _db = context;
        }

        public List<string> Get(string status)
        {
            return _db.Urls.Where(u => u.Status == status).Select(n => n.Link).ToList();

        }

        public void Create(string link)
        {
            var url = new Url
            {
                Link = link,
                Status = "Wait"
            };
            _db.Urls.Add(url);
            _db.SaveChanges();
        }

        public void Update(string old, string now)
        {
            var oldUrl = _db.Urls.Where(s => s.Status == old).AsEnumerable().Select(s =>
            {
                s.Status = now;
                return s;
            });
            foreach (var link in oldUrl)
            {
                _db.Entry(link).State = EntityState.Modified;
            }
            _db.SaveChanges();
        }

        
    }
}
