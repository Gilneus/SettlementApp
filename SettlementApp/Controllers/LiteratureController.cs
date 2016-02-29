using SettlementLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json;

namespace SettlementApp.Controllers
{
    public class LiteratureController : Controller
    {
        // GET: Literature
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddLiterature(string Literature)
        {
            int id = LiteratureMgmt.AddLiterature(Literature);
            if (id > 0)
            {
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }
        }

        [HttpPost]
        public ActionResult AddLiteratureReference(string Id, string Reference, string Page, string Random)
        {
            int id = LiteratureMgmt.AddLiteratureReference(Convert.ToInt32(Reference), Convert.ToInt32(Id), Page, Random);
            if (id > 0)
            {
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }
        }

        [HttpGet]
        public ActionResult getAllLiteratureReference(string token)
        {
            List<LiteratureReference> obj = LiteratureMgmt.getAllLiteratureReferenceByToken(token);

            String str = JsonConvert.SerializeObject(obj);
            return Json(new { success = 1, data = str }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getAllLiteratureReferenceBySattlement(string Id)
        {
            List<LiteratureReference> obj = LiteratureMgmt.getAllLiteratureReferenceById(Convert.ToInt32(Id));

            String str = JsonConvert.SerializeObject(obj);
            return Json(new { success = 1, data = str }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteLiteratureReference(string id)
        {
            if (LiteratureMgmt.DeleteLiteratureReference(Convert.ToInt32(id)))
            {
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }
        }

    }
}