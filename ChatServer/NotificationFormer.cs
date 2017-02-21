using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public static class ResponseConstructor
    {
        public static string GetUserEnteredNotification(string room, string username)
        {
            object args = new string[] { room, username };
            return JsonConvert.SerializeObject(new RequestObject("msg", "entered", args));
        }

        public static string GetUserLeftNotification(string room, string username)
        {
            object args = new string[] { room, username };
            return JsonConvert.SerializeObject(new RequestObject("msg", "left", args));
        }

        public static string GetBannedNotification(TimeSpan time)
        {
            return JsonConvert.SerializeObject(new RequestObject("admin", "ban", time.ToString()));
        }

        public static string GetUnBannedNotification()
        {
            return JsonConvert.SerializeObject(new RequestObject("admin", "unban", null));
        }

        public static string GetLoginResultNotification(string role)
        {
            return JsonConvert.SerializeObject(new RequestObject("login", role, null));
        }
    }
}
