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
      /*  public static string GetRoolNotification(string role, string username)
        { 
            object args = new string[] { role, username };
            return JsonConvert.SerializeObject(new RequestObject("msg", "entered", args));
           
        }*/
  
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

        public static string GetBannedNotification(DateTime time)
        {
            return JsonConvert.SerializeObject(new RequestObject("admin", "ban", time.ToString()));
        }

        public static string GetUnBannedNotification(string username)
        {
            return JsonConvert.SerializeObject(new RequestObject("admin", "unban", username));
        }

        public static string GetLoginResultNotification(string role, string username)
        {
            return JsonConvert.SerializeObject(new RequestObject("login", role, username));
        }

        internal static string GetRoomCreatedNotification(string room)
        {
            return JsonConvert.SerializeObject(new RequestObject("room", "created", room));
        }

        internal static string GetUserLeftNotification(string room)
        {
            return JsonConvert.SerializeObject(new RequestObject("room", "closed", room));
        }

        internal static string GetErrorNotification(string text, string module)
        {
            return JsonConvert.SerializeObject(new RequestObject(module, "error", text));
        }
    }
}
