using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssetManager.Model;
using AssetManager.Technical;
using Raven.Client;

namespace AssetManager.Controllers
{
    [LanguageFilter]
    public class HomeController : RavenController
    {
        
        public HomeController()
        {

        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View("About");
        }

        public ActionResult Test()
        {
            return View("Test");
        }

        [Authorize]
        public ActionResult DataImport()
        {
            return View("DataImport");
        }
    }
}
