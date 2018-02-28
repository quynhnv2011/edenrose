using Edenrose.Data.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vks.Common.Utils;

namespace Edenrose.Data.Service
{
    public class AccountService: BaseService
    {
        public AccountService()
            : base()
        {

        }
        public bool DoLogin(string email, string pass, ref Account acc)
        {
            try
            {
                if(email == "admin" && pass == "FDB8060958F8769E55720EA6E27029A6")//edenrose@123
                {
                    acc.Email = "admin";
                    acc.Password = "";
                    acc.FullName = "Admin";
                    return true;
                }
                return false;
            }catch(Exception ex)
            {
                OutputLog.WriteOutputLog(ex);
                return false;
            }
        }        
    }
}
