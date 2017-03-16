using AuthApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AuthApp.Controllers
{
    public static class AccessKeyProvider
    {
        public static string GetKey()
        {
            return System.Web.Security.Membership.GeneratePassword(20, 0);
            
        }

        public static string GetMD5Key(string s)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5");
        }
    }
}