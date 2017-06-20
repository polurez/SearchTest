using System.Collections.Generic;


namespace Crawler.DAL.Interfaces
{
    public interface IUrlRepository
    {
        List<string> Get(string statusUrl);
        void Update(string oldValue, string newValue);
        void Create(string link);

    }
}
