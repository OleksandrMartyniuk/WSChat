using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MultiRoomChatClient.GUI.Auth
{
    static class InputValidator
    {
        public static bool VlidateEmail(string email)
        {
            string pattern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|ua|ru|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum)\b";
            return Regex.IsMatch(email, pattern);
        }
        public static bool VlidateLogin(string login)
        {
            string pattern = @"(\w{3,})";
            return Regex.IsMatch(login, pattern);
        }
        public static bool VlidatePassword(string password)
        {
            string pattern = @"(\w{3,})";
            return Regex.IsMatch(password, pattern);
        }
    }
}
