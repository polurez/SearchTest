using System.Collections.Generic;

namespace Crawler.BLL.Interfaces
{
    public interface ISearcher
    {
       Dictionary<string, List<string>> Find(string searchString);
        //List<string> Find(string searchString);



        string Add(string url, string userId);
    }
}
