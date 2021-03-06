﻿using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using AuthApp.Controllers;
using System.Net;
using System.Net.Http;
using AuthApp.Models;
using System.Configuration;
using System.Collections.Specialized;
using System.Threading;

namespace AuthApp
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    public class Login : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Dictionary<string,string> robj = null;
            using (StreamReader sr = new StreamReader(context.Request.InputStream))
            {
                string body = sr.ReadLine();
                robj = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
            }
            string login = robj["login"];
            string password = robj["password"];

            PersonDAO check = new PersonDAO();
            PersonAuth account = null;
            PersonDAO.LoginStatus res = check.TryLogIn(login, password, out account);

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
                    string accessKey = AccessKeyProvider.GetMD5Key(login);
                    string serviceURI = ConfigurationManager.AppSettings["app_URI"];

                    using (var client = new WebClient())
                    {
                        var values = new Dictionary<string,string>();
                        values.Add("username", account.name);
                        values.Add("key", accessKey);
                        values.Add("status", ((int)account.status).ToString());
                        string bantime = account.banTill == null ? "" : account.banTill.ToString();
                        values.Add("banTill", bantime);

                        var response = client.UploadString(serviceURI, JsonConvert.SerializeObject(values));
                    }
                    Thread.Sleep(100); 
                    context.Response.AddHeader("access-key", accessKey);
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