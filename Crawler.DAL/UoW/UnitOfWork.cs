using Crawler.DAL.Context;
using Crawler.DAL.Interfaces;
using Crawler.DAL.Repositories;

namespace Crawler.DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private  SiteContext _db;
        private UrlRepository _urlRepository;
        private SiteRepository _siteRepository;

        public UnitOfWork(string connectionString)
        {
            _db = new SiteContext(connectionString);
        }

        public IUrlRepository Urls => _urlRepository ?? (_urlRepository = new UrlRepository(_db));

        public ISiteRepository Sites => _siteRepository ?? (_siteRepository = new SiteRepository(_db));

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
