using System.Collections.Generic;

namespace Crawler.BLL.Interfaces
{
    public interface IWorker
    {
        List<string> Start(List<string> globalList, int deep, int width);
    }
}
