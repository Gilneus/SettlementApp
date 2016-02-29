using SettlementLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeoJson.net2;
using Newtonsoft;
using Newtonsoft.Json;

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
            List<Settlement> obj = SettlementMgmt.getAllSettlement();

            String str = JsonConvert.SerializeObject(obj);
            return Json(new { success = 1, data = str }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteSettlement(string id)
        {
            if (SettlementMgmt.DeleteSettlement(Convert.ToInt32(id)))
            {
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }
        }

        [HttpPost]
        public ActionResult AddSettlement(string Name, string Longitude, string Latitude, string ABS, string REL, string Number, string Years, string Description, string Id, string Surface, string StrayFinds, string Prospection, string Excavation, string Country, string token)
        {
            int id = SettlementMgmt.AddSettlement(Convert.ToInt32(Id), Name, Description, Latitude, Longitude, ABS, string.IsNullOrEmpty(REL) ? 0 : Convert.ToInt32(REL), string.IsNullOrEmpty(Number) ? 0 : Convert.ToInt32(Number), string.IsNullOrEmpty(Years) ? 0 : Convert.ToInt32(Years), string.IsNullOrEmpty(Surface) ? 0 : Convert.ToInt32(Surface), Convert.ToBoolean(StrayFinds), Convert.ToBoolean(Prospection), Convert.ToBoolean(Excavation), Country, token);
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

        [HttpGet]
        public ActionResult GetGeoJson()
        {
            var allSettlements = SettlementMgmt.getAllSettlement();
            List<Point> points = new List<Point>();
            List<LatLng> coordinates = new List<LatLng>();

            foreach (var settlement in allSettlements)
            {
                coordinates = new List<LatLng>();
                coordinates.Add(new LatLng(string.IsNullOrEmpty(settlement.latitude) ? 0 : Convert.ToDouble(settlement.latitude), string.IsNullOrEmpty(settlement.longitude) ? 0 : Convert.ToDouble(settlement.longitude)));
                points.Add(new Point(coordinates, new { Name = settlement.Name, Surface = settlement.SurfaceInHectars, Buildings = settlement.NumberBuildings, Activity = settlement.ActivityYears }));
                
            }

            return Json(points);

        }

    }

}