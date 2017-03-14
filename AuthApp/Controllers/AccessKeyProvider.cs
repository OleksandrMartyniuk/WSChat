using AuthApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthApp.Controllers
{
    public static class AccessKeyProvider
    {
        public static string GetKey()
        {
            return System.Web.Security.Membership.GeneratePassword(20, 5);
        }
    }
}