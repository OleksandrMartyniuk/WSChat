using AuthApp.Controllers;
using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AuthApp
{
    /// <summary>
    /// Summary description for Registration
    /// </summary>
    public class Registration : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Dictionary<string, string> robj = null;
            using (StreamReader sr = new StreamReader(context.Request.InputStream))
            {
                string body = sr.ReadLine();
                robj = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
            }
            string login = robj["login"];
            string email = robj["email"];
            string password = robj["password"];

            PersonDAO check = new PersonDAO();
            PersonDAO.RegistrationStatus res = check.TryRegister(login, password, email);

            switch (res)
            {
                case PersonDAO.RegistrationStatus.EmailExists:
                    context.Response.Write("email");
                    break;
                case PersonDAO.RegistrationStatus.LoginExists:
                    context.Response.Write("login");
                    break;
                case PersonDAO.RegistrationStatus.LoginAndEmailExists:
                    context.Response.Write("login&email");
                    break;
                case PersonDAO.RegistrationStatus.OK:
                    context.Response.Write("ok");
                    break;
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