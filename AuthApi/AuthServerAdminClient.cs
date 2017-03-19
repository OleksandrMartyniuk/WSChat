using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace ChatServer.AuthApi
{
    public static class AuthServerAdminClient
    {
        public static void Ban(string login, DateTime? time)
        {
            var values = new Dictionary<string, string>();
            values.Add("login", login);
            values.Add("cmd", "ban");
            if (time != null)
            {
                values.Add("time", time.ToString());
            } 
            SendRequest(values, false);
        }

        public static void UnBan(string login)
        {
            var values = new Dictionary<string, string>();
            values.Add("login", login);
            values.Add("cmd", "unban");
            SendRequest(values, true);
        }

        private static void SendRequest(Dictionary<string, string> values, bool ban)
        {
            string URI = ConfigurationManager.AppSettings["auth_server_host"] + ConfigurationManager.AppSettings["auth_admin_handler"];

            var request = WebRequest.Create(URI);
            request.Method = "POST";
            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.WriteLine(JsonConvert.SerializeObject(values));
            }

            var response = request.GetResponse();
            string body = null;
            string key = response.Headers["access-key"];

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                body = sr.ReadLine();
            }
        }
        
    }
}