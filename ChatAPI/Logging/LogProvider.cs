using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ChatServer
{
    public static class LogProvider
    {
        private static string LogFolder;

        static LogProvider()
        {
            LogFolder = HttpContext.Current.Server.MapPath(@"/App_Data/Log/");
            Directory.CreateDirectory(LogFolder);
        }
        public static void AppendRecord(string record)
        {
            string filename = string.Format("{0}_{1}_{2}", DateTime.Now.Date.Day, DateTime.Now.Month, DateTime.Now.Year);
            string path = LogFolder + filename;
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            File.AppendAllLines(path , new string[] { DateTime.Now.ToShortTimeString() + record });
        }
    }
}
