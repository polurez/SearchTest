using System.Data.Entity;
using Crawler.DAL.Entities;

namespace Crawler.DAL.Context
{
    public class SiteContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }
        public DbSet<Site> Sites { get; set; }
        static SiteContext()
        {
            Database.SetInitializer<SiteContext>(new StoreDbInitializer());
        }
        public SiteContext(string connectionString)
            : base(connectionString)
        {

        }
        class StoreDbInitializer : DropCreateDatabaseIfModelChanges<SiteContext>
        {
            protected override void Seed(SiteContext db)
            {
                db.Urls.Add(new Url { Link = "https://www.codeproject.com/", Status = "Wait" });
                db.Urls.Add(new Url { Link = "https://www.wikipedia.org/", Status = "Wait" });
                db.Urls.Add(new Url { Link = "http://www.spacex.com/", Status = "Wait" });
                db.Urls.Add(new Url { Link = "https://www.gordonramsay.com/", Status = "Wait" });
                db.Urls.Add(new Url { Link = " http://www.howdesign.com/", Status = "Wait" });

                db.SaveChanges();
            }
        }

    }
}
