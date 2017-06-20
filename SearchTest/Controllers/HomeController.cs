using System.Web.Mvc;
using Crawler.BLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace SearchTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISearcher _searcher;
        public HomeController(ISearcher searcher)
        {
            _searcher = searcher;
        }
        public ActionResult Index()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Find(string search)
        {
            var lists = _searcher.Find(search);
            ViewBag.List = lists;
            ViewBag.Links = lists["Link"];
            ViewBag.Titles = lists["Title"];
            ViewBag.Descriptions = lists["Description"];
            ViewBag.Images = lists["Image"];
            ViewBag.Search = search;
            return View();
        }
        public ActionResult AddUrl(string url)
        {            
             ViewBag.Message = _searcher.Add(url, HttpContext.User.Identity.GetUserId());
            return View();
        }
    }
}