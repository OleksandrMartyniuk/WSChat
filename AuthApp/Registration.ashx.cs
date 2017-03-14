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
            RequestObject robj = null;
            using (StreamReader sr = new StreamReader(context.Request.InputStream))
            {
                string body = sr.ReadLine();
                robj = JsonConvert.DeserializeObject<RequestObject>(body);
            }
            string[] args = JsonConvert.DeserializeObject<string[]>(robj.Args.ToString());
            string login = args[0];
            string password = args[1];
            string email = args[2];

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