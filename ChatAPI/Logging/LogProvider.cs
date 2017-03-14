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
        static LogProvider()
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Log");
        }
        public static void AppendRecord(string record)
        {
            string filename = string.Format("{0}_{1}_{2}", DateTime.Now.Date.Day, DateTime.Now.Month, DateTime.Now.Year);
            string path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Log" + filename;

            using (StreamWriter sr = File.AppendText(path))
            {
                string rec = DateTime.Now.ToShortTimeString() + record;
                sr.WriteLine(rec);
            }
        }
    }
}
