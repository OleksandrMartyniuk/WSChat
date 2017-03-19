using ChatServer.AuthApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ChatServer
{
    /// <summary>
    /// Summary description for LoginHandler
    /// </summary>
    public class LoginHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string input = null;
            using (StreamReader sr = new StreamReader(context.Request.InputStream))
            {
                input = sr.ReadLine();
            }
            Dictionary<string, string> obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(input);

            AuthPool.AppendRecord(new AuthPool.PoolObject(
                    obj["username"], 
                    obj["key"], 
                    int.Parse(obj["status"]), 
                    obj["banTill"]));
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