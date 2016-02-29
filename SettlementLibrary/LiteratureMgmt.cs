using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementLibrary
{
    public static class LiteratureMgmt
    {
        public static int AddLiterature(string name)
        {
            int id = 0;
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    Literature obj = new Literature();
                    obj.Name = name;

                    db.Literatures.Add(obj);
                    db.Entry(obj).State = EntityState.Added;
                    id = db.SaveChanges();

                    return id;
                }
            }
            catch (Exception ex)
            {
                ErrorLogsMgmt.CreateErrorLog(System.Reflection.MethodBase.GetCurrentMethod(), ex);
                return id;
            }
        }
        public static List<Literature> getAllLiterature(string searchText = "")
        {
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    List<Literature> lstitem = new List<Literature>();
                    lstitem = db.Literatures.OrderBy(x => x.Name).ToList();

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
        public static int AddLiteratureReference(int literature, int settlement, string pages, string temptoken)
        {
            int id = 0;
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    LiteratureReference obj = new LiteratureReference();
                    obj.Literature = literature;
                    obj.Settlement = settlement;
                    obj.Pages = pages;
                    obj.TempToken = temptoken;

                    db.LiteratureReferences.Add(obj);
                    db.Entry(obj).State = EntityState.Added;
                    id = db.SaveChanges();

                    return id;
                }
            }
            catch (Exception ex)
            {
                ErrorLogsMgmt.CreateErrorLog(System.Reflection.MethodBase.GetCurrentMethod(), ex);
                return id;
            }
        }
        public static bool DeleteLiteratureReference(int id)
        {
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    LiteratureReference obj = db.LiteratureReferences.Where(l=>l.Id==id).SingleOrDefault();

                    db.LiteratureReferences.Remove(obj);
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
        public static List<LiteratureReference> getAllLiteratureReferenceByToken(string token)
        {
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    List<LiteratureReference> lstitem = new List<LiteratureReference>();
                    lstitem = db.LiteratureReferences.Where(x => x.TempToken == token).OrderBy(x => x.Id).ToList();

                    foreach (LiteratureReference obj in lstitem)
                    {
                        obj.LiteratureName = db.Literatures.Where(l => l.Id == obj.Literature).ToList().SingleOrDefault().Name;
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

        public static List<LiteratureReference> getAllLiteratureReferenceById(int Id)
        {
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    List<LiteratureReference> lstitem = new List<LiteratureReference>();
                    lstitem = db.LiteratureReferences.Where(x => x.Settlement == Id).OrderBy(x => x.Id).ToList();

                    foreach (LiteratureReference obj in lstitem)
                    {
                        obj.LiteratureName = db.Literatures.Where(l => l.Id == obj.Literature).ToList().SingleOrDefault().Name;
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

    }
}
