using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementLibrary
{
    public static class SettlementMgmt
    {
        public static List<Settlement> getAllSettlement()
        {
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    List<Settlement> lstitem = new List<Settlement>();
                    //lstitem = db.Settlements
                    //            .Select(s => new Settlement {
                    //                Id = s.Id,
                    //                Name = s.Name,
                    //                Description = s.Description,
                    //                latitude = s.latitude,
                    //                longitude = s.longitude,
                    //                TimeperiodAbsolute = s.TimeperiodAbsolute,
                    //                TimeperiodRelative = s.TimeperiodRelative,
                    //                NumberBuildings = s.NumberBuildings,
                    //                ActivityYears = s.ActivityYears
                    //            }).ToList();

                    lstitem = db.Settlements.OrderByDescending(s=>s.Id).ToList();

                    foreach (Settlement obj in lstitem)
                    {
                        obj.TimeperiodRelativeName = db.TimeperiodRelatives.Where(t => t.Id == obj.TimeperiodRelative).ToList().SingleOrDefault().Name;
                    }

                    return lstitem;
                }
            }
            catch (Exception ex)
            {
                ErrorLogsMgmt.CreateErrorLog(System.Reflection.MethodBase.GetCurrentMethod(), ex);
                return null;
            }
        }
        public static Settlement getSettlementById(int id)
        {
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    Settlement obj = db.Settlements.Where(s => s.Id == id).SingleOrDefault();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                ErrorLogsMgmt.CreateErrorLog(System.Reflection.MethodBase.GetCurrentMethod(), ex);
                return null;
            }
        }
        public static bool DeleteSettlement(int id, string userId)
        {
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    List<LiteratureReference> lst = db.LiteratureReferences.Where(s => s.Settlement == id).ToList();
                    db.LiteratureReferences.RemoveRange(lst);
                    Settlement obj = db.Settlements.Where(l => l.Id == id && l.AuthorId == userId).SingleOrDefault();
                    db.Settlements.Remove(obj);
                    id = db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorLogsMgmt.CreateErrorLog(System.Reflection.MethodBase.GetCurrentMethod(), ex);
                return false;
            }
        }
        public static int AddSettlement(int Id, string Name, string Description, string latitude, string longitude, string TimeperiodAbsolute, int TimeperiodRelative, int NumberBuildings, int ActivityYears, int Surface, string DocumentationType, string Country, string UserId, string temptoken)
        {
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    Settlement obj = new Settlement();
                    if (Id <= 0)
                    {
                        obj.Name = Name;
                        obj.Description = Description;
                        obj.latitude = latitude;
                        obj.longitude = longitude;
                        obj.TimeperiodAbsolute = TimeperiodAbsolute;
                        obj.TimeperiodRelative = TimeperiodRelative;
                        obj.NumberBuildings = NumberBuildings;
                        obj.ActivityYears = ActivityYears;
                        obj.SurfaceInHectars = Surface;
                        obj.DocumentationType = DocumentationType;
                        obj.Country = Country;
                        obj.AuthorId = UserId;

                        db.Settlements.Add(obj);
                        db.Entry(obj).State = EntityState.Added;
                        Id = db.SaveChanges();

                        List<LiteratureReference> listLR = db.LiteratureReferences.Where(l=>l.TempToken == temptoken).ToList();
                        foreach (LiteratureReference item in listLR)
                        {
                            item.Settlement = obj.Id;
                            //item.TempToken = "";
                            db.Entry(item).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        obj = db.Settlements.Where(s => s.Id == Id).SingleOrDefault();
                        obj.Name = Name;
                        obj.Description = Description;
                        obj.latitude = latitude;
                        obj.longitude = longitude;
                        obj.TimeperiodAbsolute = TimeperiodAbsolute;
                        obj.TimeperiodRelative = TimeperiodRelative;
                        obj.NumberBuildings = NumberBuildings;
                        obj.ActivityYears = ActivityYears;
                        obj.SurfaceInHectars = Surface;
                        obj.DocumentationType = DocumentationType;
                        obj.Country = Country;
                        db.Entry(obj).State = EntityState.Modified;
                        Id = db.SaveChanges();

                    }

                    return Id;
                }
            }
            catch (Exception ex)
            {
                ErrorLogsMgmt.CreateErrorLog(System.Reflection.MethodBase.GetCurrentMethod(), ex);
                return Id;
            }
        }

        public static List<TimeperiodRelative> getAllTimeperiodRelative(string searchText = "")
        {
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    List<TimeperiodRelative> lstitem = new List<TimeperiodRelative>();
                    lstitem = db.TimeperiodRelatives.OrderBy(x => x.Name).ToList();
                    //lstitem = db.TimeperiodRelatives.OrderBy(x => x.Name).ToList();

                    if (!string.IsNullOrEmpty(searchText))
                        lstitem = lstitem.Where(x => x.Name.ToLower().StartsWith(searchText.ToLower())).ToList();

                    return lstitem;
                }
            }
            catch (Exception ex)
            {
                ErrorLogsMgmt.CreateErrorLog(System.Reflection.MethodBase.GetCurrentMethod(), ex);
                return null;
            }
        }
    }
}
