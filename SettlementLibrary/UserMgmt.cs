using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementLibrary
{
    public static class UserMgmt
    {
        public static User VerifyUser(string username, string passward)
        {
            try
            {
                using (SettlementDB db = new SettlementDB())
                {

                    User obj = db.Users.Where(u => u.UserName == username).SingleOrDefault();

                    if (obj != null && obj.Id != null)
                    {
                        string ePassword = GateKeeper.Decrypt(obj.Password);
                        if (ePassword == passward)
                        {
                            return obj;
                        }
                        else
                        {
                            return null;
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogsMgmt.CreateErrorLog(System.Reflection.MethodBase.GetCurrentMethod(), ex);
                throw ex;
                return null;
            }
        }

        public static int AddUser(string username, string passward)
        {
            int id = 0;
            try
            {
                using (SettlementDB db = new SettlementDB())
                {
                    User obj = new User();
                    obj.UserName = username;
                    obj.Password = GateKeeper.Encrypt(passward);

                    db.Users.Add(obj);
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
    }
}
