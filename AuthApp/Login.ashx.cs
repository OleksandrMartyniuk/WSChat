using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using AuthApp.Controllers;

namespace AuthApp
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    public class Login : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            RequestObject robj = null;
            using (StreamReader sr = new StreamReader(context.Request.InputStream))
            {
                string body = sr.ReadLine();
                robj = JsonConvert.DeserializeObject<RequestObject>(body);
            }
            string[] args = JsonConvert.DeserializeObject<string[]>(robj.Args.ToString());
            string login = args[0];
            string password = args[1];

            PersonDAO check = new PersonDAO();
            PersonDAO.LoginStatus res = check.TryLogIn(login, password);

            switch (res)
            {
                case PersonDAO.LoginStatus.WrongLogin:
                    context.Response.Write("login");
                    break;
                case PersonDAO.LoginStatus.WrongPassword:
                    context.Response.Write("password");
                    break;
                case PersonDAO.LoginStatus.OK:
                    context.Response.Write("ok");
                    break;
            }
            LogProvider.AppendRecord("Logged in");
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