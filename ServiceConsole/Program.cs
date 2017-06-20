using Crawler.BLL.Implementation;
using Crawler.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ServiceConsole
{
    class Program
    {
        public static bool Completed { get; set; }

        static void Main(string[] args)
        {
        }
        public void Crawler(object source, ElapsedEventArgs e)
        {

            if (Completed)
            {
                Parser parser = new Parser();
                Worker worker = new Worker();
                Completed = false;
                int deep = Int32.Parse(ConfigurationManager.AppSettings["deep"]);
                int width = Int32.Parse(ConfigurationManager.AppSettings["width"]);
                var urlContext = new SiteContext("Data Source=IGOR-ПК;Initial Catalog=TestMVC;Integrated Security=True");
                var globalList = urlContext.Urls
                    .SqlQuery("USE [TestMVC] SELECT * FROM[dbo].[Urls] where status = 'Wait' ").ToList();
                urlContext.Database.ExecuteSqlCommand(
                    "USE [TestMVC] GO UPDATE[dbo].[Urls] SET[Status] = 'Working' WHERE[Status] = 'Wait' GO");
                var tempList = new List<string>();
                foreach (var step in globalList)
                {
                    tempList.Add(step.Link);
                }
                tempList.AddRange(worker.Start(tempList, deep, width));

                tempList = tempList.Distinct().ToList();
                parser.Parse(tempList);
                urlContext.Database.ExecuteSqlCommand(
                    "USE [TestMVC] GO UPDATE[dbo].[Urls] SET[Status] = 'Complete' WHERE[Status] = 'Working' GO");

                Completed = true;
            }


        }

    }
}
