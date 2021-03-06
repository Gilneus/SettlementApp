﻿using SettlementLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SettlementApp.Controllers
{
    public class SettlementController : Controller
    {
        // GET: Settlement
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else 
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult getTimeperiodRelative()
        {
            List<TimeperiodRelative> obj = SettlementMgmt.getAllTimeperiodRelative("");
            
            String str = JsonConvert.SerializeObject(obj);
            return Json(new { success = 1, data =  str}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getLiteratures()
        {
            List<Literature> obj = LiteratureMgmt.getAllLiterature("");

            String str = JsonConvert.SerializeObject(obj);
            return Json(new { success = 1, data = str }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult getAllSettelemets()
        {
            var obj = SettlementMgmt.getAllSettlement().Select(x => new { Name = x.Name, Country = x.Country, TimeperiodRelativeName = x.TimeperiodRelativeName, SurfaceInHectars = x.SurfaceInHectars.ToString(), Id = x.Id, CanDelete = (x.AuthorId == ((User) Session["User"]).Id.ToString() || ((User)Session["User"]).IsSuperuser), Latitude = x.latitude, Longitude = x.longitude, NumberBuilding = x.NumberBuildings, AcivityInYears = x.ActivityYears, TimeperiodAbsolute = x.TimeperiodAbsolute, DocumentationType = x.DocumentationType, Description = x.Description });

            String str = JsonConvert.SerializeObject(obj);
            return Json(new { success = 1, data = str }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteSettlement(string id)
        {
            User objUser = (User) Session["User"];
            if (SettlementMgmt.DeleteSettlement(Convert.ToInt32(id), objUser?.Id.ToString()))
            {
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }
        }

        [HttpPost]
        public ActionResult AddSettlement(string Name, string Longitude, string Latitude, string ABS, string REL, string Number, string Years, string Description, string Id, string Surface, string DocumentationType, string Country, string token)
        {
            User objUser = (User)Session["User"];
            int id = SettlementMgmt.AddSettlement(Convert.ToInt32(Id), Name, Description, Latitude, Longitude, ABS, string.IsNullOrEmpty(REL) ? 0 : Convert.ToInt32(REL), string.IsNullOrEmpty(Number) ? 0 : Convert.ToInt32(Number), string.IsNullOrEmpty(Years) ? 0 : Convert.ToInt32(Years), string.IsNullOrEmpty(Surface) ? 0 : Convert.ToInt32(Surface), DocumentationType, Country, objUser?.Id.ToString(), token);
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
        public ActionResult getSettlementDetails(string Id)
        {
            Settlement obj = SettlementMgmt.getSettlementById(Convert.ToInt32(Id));

            String str = JsonConvert.SerializeObject(obj);
            return Json(new { success = 1, data = str }, JsonRequestBehavior.AllowGet);
        }
    }

}