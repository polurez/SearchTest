using Crawler.BLL.Interfaces;
using Crawler.BLL.Locator;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Timers;

namespace Crawler.Service
{
    public partial class Service1 : ServiceBase
    {
        public static bool Completed { get; set; }
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // For debug
            //System.Diagnostics.Debugger.Launch();
            Completed = true;
            var timer = new Timer();
            timer.Elapsed += Crawler;
            timer.Interval = 1000;
            timer.Enabled = true;

        }

        protected override void OnStop()
        {
        }
        public void Crawler(object source, ElapsedEventArgs e)
        {
            if (Completed)
            {
                Completed = false;

                IServiceLocator locator = new ServiceLocator();
                var getter = locator.GetService<IGetter>();
                var trueList = getter.Start();
                if (trueList.Count!=0)
                {
                    var parser = locator.GetService<IParser>();
                    var worker = locator.GetService<IWorker>();

                    var deep = int.Parse(ConfigurationManager.AppSettings["deep"]);
                    var width = int.Parse(ConfigurationManager.AppSettings["width"]);

                    trueList.AddRange(worker.Start(trueList, deep, width));
                    trueList = trueList.Distinct().ToList();
                    parser.Start(trueList);
                }
                Completed = true;
            }
        }
    }
}
