using Crawler.BLL.Implementation;
using Crawler.BLL.Interfaces;
using Crawler.DAL.Interfaces;
using Crawler.DAL.UoW;
using System;
using System.Collections.Generic;

namespace Crawler.BLL.Locator
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IDictionary<object, object> _services;

        public ServiceLocator()
        {
            _services = new Dictionary<object, object>();

            // fill the map
            _services.Add(typeof(IParser), new Parser());
            _services.Add(typeof(IWorker), new Worker());
            _services.Add(typeof(IGetter), new Getter());
        }
        public T GetService<T>()
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("The requested service is not registered");
            }
        }
    }
}
