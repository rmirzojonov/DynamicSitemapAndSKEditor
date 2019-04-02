using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DynamicSitemapAndSKEditor.Models;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DynamicSitemapAndSKEditor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }


        [Route("sitemap.xml")]
        public IActionResult Sitemap()
        {
            var generator = new SitemapGenerator();
            var sitemapNodes = generator.GetSitemapNodes(new Microsoft.AspNetCore.Mvc.Routing.UrlHelper(this.Url.ActionContext));
            string xml = generator.GetSitemapDocument(sitemapNodes);
            return this.Content(xml, "text/xml", Encoding.UTF8);
        }

        public IActionResult Post(int id)
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
