using AuthApp.Controllers;
using AuthApp.Models;
using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AuthApp
{
    /// <summary>
    /// Summary description for OutterLogin
    /// </summary>
    public class OutterLogin : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            RequestObject robj = null;
            using (StreamReader sr = new StreamReader(context.Request.InputStream))
            {
                string body = sr.ReadLine();
                robj = JsonConvert.DeserializeObject<RequestObject>(body);
            }

            string email = (string)robj.Args;
            string username = email.Split('@')[0];
            string password = Membership.GeneratePassword(10, 3);

            PersonDAO check = new PersonDAO();
            PersonDAO.RegistrationStatus res = check.TryRegister(username, password, email);
            RequestObject response = new RequestObject("registration", "", null);
            switch (res)
            {
                case PersonDAO.RegistrationStatus.EmailExists:
                    response.Cmd = "fail";
                    response.Args = "Email exists";
                    break;
                case PersonDAO.RegistrationStatus.LoginExists:
                    PersonAuth pers = null;
                    PersonDAO.LoginStatus loginStatus = check.TryLogIn(username, password, out pers);
                    //switch
                    response.Cmd = "fail";
                    response.Args = "Login exists";
                    break;

                case PersonDAO.RegistrationStatus.OK:
                    response.Cmd = "ok";
                    response.Args = username;
                    break;
            }
            string r = JsonConvert.SerializeObject(response);
            context.Response.Write(r);
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