using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SettlementApp.Controllers
{
    public class ChangelogController : Controller
    {
        // GET: Changelog
        public ActionResult Index()
        {
            return View();
        }
    }
}