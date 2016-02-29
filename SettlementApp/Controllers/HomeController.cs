using SettlementLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SettlementApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
            return View();
        }


        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            User obj = UserMgmt.VerifyUser(Email, Password);
            if (obj != null && obj.Id != null)
            {
                Session["User"] = obj;
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("User");
            return RedirectToAction("Index");
        }


    }
}