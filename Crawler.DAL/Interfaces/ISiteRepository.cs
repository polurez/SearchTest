using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.DAL.Entities;

namespace Crawler.DAL.Interfaces
{
    public interface ISiteRepository
    {
        //Dictionary<string, List<string>> Get(string statusUrlOrSearchSite);
        List<Site> Get(string searchString);

        void Create(string link, string content, string title, string description, string image);
    }
}
