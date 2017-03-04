using Nemiro.OAuth.LoginForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Core
{
    public class ApiAuth
    {
        public ApiAuth()
        {
        }
        public string Google_Auth()
        {
            string name = null;

            var login = new GoogleLogin("282890727966-fnsa1tofldqjseo801t93i8lah4bijko.apps.googleusercontent.com", "rK8eRcFoO6CjYP9PmrwUczZW", "https://www.googleapis.com/auth/plus.login", autoLogout: true, loadUserInfo: true);
            login.ShowDialog();

            if (login.IsSuccessfully)
            {
                name = login.UserInfo.FirstName;
            }
            return name;
        }
        public string Facebook_Auth()
        {
            string name = null;

            var login = new FacebookLogin("1435890426686808", "c6057dfae399beee9e8dc46a4182e8fd", autoLogout: true, loadUserInfo: true);
            login.ShowDialog();

            if (login.IsSuccessfully)
            {
                name = login.UserInfo.FirstName;
            }
            return name;
        }
        public string Tr(string s)
        {
            string ret = "";
            List<String> rus = new List<string>{ "а", "б","в","г","д","е","ё","ж","з","и","й","к","л","м","н","о","п","р","с","т","у","ф","х","ц","ч","ш","щ","ъ","ы","ь","э","ю","я",
            "А","Б","В","Г","Д","Е","Ё","Ж","З","И","Й","К","Л","М","Н","О","П","Р","С","Т","У","Ф","Х","Ц","Ч","Ш","Щ","Ъ","Ы","Ь","Э","Ю","Я"};
            List<String> eng = new List<string>{"a","b","v","g","d","e","yo","zh","z","i","j","k","l","m","n","o","p","r","s","t","u","f","h","c","ch","sh","sch","j","i","j","e","yu","ya",
            "A","B","V","G","D","E","Yo","Zh","Z","I","J","K","L","M","N","O","P","R","S","T","U","F","H","C","Ch","Sh","Sch","J","I","J","E","Yu","Ya"};

            for (int j = 0; j < s.Length; j++)
            {
                if (rus.Contains(s.Substring(j, 1)))
                {
                    int i = rus.IndexOf(s.Substring(j, 1));
                    ret += eng[i];
                }
                else
                    continue;
            }
            return ret;
        }
        bool invalid = false;
        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;
            
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        private string DomainMapper(Match match)
        {
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}
