using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace MultiRoomChatClient.GUI
{
    static class ResourceProvider
    {
        public static string locale { get; private set; }
        public static string lang { get; private set; }
        static string path = @"C:\Users\sanyok\Source\Repos\WSChat\MultiRoomChatClient\locale\";
        static Dictionary<string, string> languages = new Dictionary<string, string>();
        static JObject json { get; set; }

        static ResourceProvider()
        {
            ResolveLocales();
            lang = ConfigurationManager.AppSettings["lang"];
            SetLocale(lang);
        }

        static void ResolveLocales()
        {
            string[] localeFiles = Directory.GetFiles(path, "*.json")
                                     .Select(Path.GetFileName)
                                     .ToArray();

            foreach(string file in localeFiles)
            {
                string str = null;
                using (StreamReader sr = new StreamReader(new FileStream(path + file, FileMode.Open)))
                {
                    str = sr.ReadToEnd();
                }
                str = Regex.Replace(str, @"\s+", "");
                JObject jobj = JObject.Parse(str);
                string language = jobj["lang"].Value<string>();
                languages.Add(language, file);
            }
        }

        public static void SetLocale(string language)
        {
            string filepath = path + languages[language];
            string file = null;
            using (StreamReader sr = new StreamReader(new FileStream(filepath, FileMode.Open)))
            {
                file = sr.ReadToEnd();
            }
            json = JObject.Parse(file);
            lang = json["lang"].Value<string>();
            locale = json["locale"].Value<string>();
            UpdateSetting("lang", lang);
        }

        private static void UpdateSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string GetValue(string identifier)
        {
            JToken t = json.SelectToken(identifier);
            return t.Value<string>();
        }

        public static string[] GetLanguageList()
        {
            return languages.Keys.ToArray<string>();
        }
    }
}
