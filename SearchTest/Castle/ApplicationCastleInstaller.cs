using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Crawler.BLL.Implementation;
using Crawler.BLL.Interfaces;
using Crawler.DAL.Entities;
using Crawler.DAL.Interfaces;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Crawler.DAL.Repositories;
using Crawler.DAL.UoW;

namespace SearchTest.Castle
{
    public class ApplicationCastleInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISearcher>().ImplementedBy<Searcher>());
       
            //регистрируем каждый контроллер по отдельности
            var controllers = Assembly.GetExecutingAssembly()
                .GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();
            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
        }
    }
}