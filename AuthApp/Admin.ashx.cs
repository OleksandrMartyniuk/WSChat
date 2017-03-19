using AuthApp.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AuthApp
{
    /// <summary>
    /// Summary description for Admin
    /// </summary>
    public class Admin : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string body = null;
            using (StreamReader sr = new StreamReader(context.Request.InputStream))
            {
                body = sr.ReadLine();
            }

            Dictionary<string, string> req = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
            int status = req["cmd"] == "ban" ? 1 : 0;
            string login = req["login"];
             
            PersonDAO dao = new PersonDAO();

            DateTime? dateTime = null;

            if(req.ContainsKey("time"))
                dateTime = DateTime.Parse(req["time"]);

            dao.SetStatus(login, (Core.Models.AuthStatus)status, dateTime);
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