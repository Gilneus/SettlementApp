using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementLibrary
{
    public class ErrorLogsMgmt
    {
        public static string CreateErrorLog(System.Reflection.MethodBase pBase, Exception pEx, string Description = null)
        {
            string ErrorMessage = string.Empty;
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    ErrorLog objErrorLogs = new ErrorLog();
                    //ExcpID = objErrorLogs.ErrorLogId;
                    objErrorLogs.Class = pBase.DeclaringType.Name;
                    objErrorLogs.Method = pBase.Name;
                    objErrorLogs.Message = "[0]" + ' ' + pEx.Message + Description;
                    objErrorLogs.Time = DateTime.Now;
                    db.Entry(objErrorLogs).State = EntityState.Added;
                    int Result = db.SaveChanges();

                    Exception pInn = pEx;

                    ErrorMessage = "[" + pBase.DeclaringType.Name + "." + pBase.Name + "] " + pEx.Message;

                    int i = 1;

                    while (pInn.InnerException != null)
                    {
                        objErrorLogs = new ErrorLog();
                        objErrorLogs.Class = pBase.DeclaringType.Name;
                        objErrorLogs.Method = pBase.Name;
                        objErrorLogs.Message = "[" + i + "]: " + pInn.InnerException.Message;
                        objErrorLogs.Time = DateTime.Now;
                        db.Entry(objErrorLogs).State = EntityState.Added;
                        Result = db.SaveChanges();

                        pInn = pInn.InnerException;
                        ErrorMessage = "[" + pBase.DeclaringType.Name + "." + pBase.Name + "]" + pInn.Message;

                        i++;
                    }
                    return "[error] " + ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "[ErrorLog error] " + ex.Message;
                return ErrorMessage;
            }
        }
    }
}
