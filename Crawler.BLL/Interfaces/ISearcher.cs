using Crawler.BLL.DTO;
using System.Collections.Generic;

namespace Crawler.BLL.Interfaces
{
    public interface ISearcher
    {
     List<SiteDTO> Find(string searchString);
        

        string Add(string url, string userId);
    }
}
