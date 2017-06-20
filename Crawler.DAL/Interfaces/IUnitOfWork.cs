using Crawler.DAL.Entities;

namespace Crawler.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUrlRepository Urls { get; }
        ISiteRepository Sites { get; }
        void Save();
    }
}
