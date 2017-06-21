using System.Web.Mvc;
using Crawler.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Crawler.BLL.DTO;
using System.Collections.Generic;
using AutoMapper;
using SearchTest.Models;
using System;

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
            var sites = _searcher.Find(search);
            ViewBag.Sites = sites;
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