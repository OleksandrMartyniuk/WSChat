using ChatServer.AuthApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServer
{
    /// <summary>
    /// Summary description for ClientLoadHandler
    /// </summary>
    public class ClientLoadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string key = context.Request.QueryString.Get("key");
            if(key == null || key == "")
            {
                context.Response.StatusCode = 404;
                context.Response.Write("no access key provided");
                return;
            }

            var user = AuthPool.GetRecordByKey(key);

            if (user == null)
            {
                context.Response.StatusCode = 403;
                context.Response.Write("no access record was found");
                return;
            }

            string username = user.Username;
            int status = (int)user.status;

            context.Response.Cookies.Add(new HttpCookie("username", username));
            context.Response.Cookies.Add(new HttpCookie("status", status.ToString()));
            context.Response.WriteFile( AppDomain.CurrentDomain.BaseDirectory + "/default.html");
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