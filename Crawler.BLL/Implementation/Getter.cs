using System.Collections.Generic;
using Crawler.BLL.Interfaces;
using Crawler.DAL.Interfaces;
using Crawler.DAL.UoW;

namespace Crawler.BLL.Implementation
{
    public class Getter : IGetter
    {
        private readonly IUnitOfWork _unitOfWork;

        public Getter()
        {
            _unitOfWork = new UnitOfWork("DefaultConnection");
        }
        public List<string> Start()
        {
            var temp=_unitOfWork.Urls.Get("Wait");
            if (temp == null) return null;
            _unitOfWork.Urls.Update("Wait","Working");
            return temp;
        }
    }
}
