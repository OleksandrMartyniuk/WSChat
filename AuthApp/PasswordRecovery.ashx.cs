using AuthApp.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AuthApp
{
    /// <summary>
    /// Summary description for PasswordRecovery
    /// </summary>
    public class PasswordRecovery : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string email = null;
            using (StreamReader sr = new StreamReader(context.Request.InputStream))
            {
                email = sr.ReadLine();
            }
           
            PersonDAO check = new PersonDAO();
            var p = check.GetPerson("email", email);
            
            if(p == null)
            {
                context.Response.Write("not_found");
            }
            else
            {
                PasswordRecoveryManager.SendByEmail(p.email, p.name, p.password);
                context.Response.Write("ok");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}