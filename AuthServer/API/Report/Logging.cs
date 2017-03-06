using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AuthServer
{
    public static class LogProvider
    {
        private static string path = "Log/";
        public static void AppendRecord(string record)
        {
            try
            {
                string filename = string.Format("{0}_{1}_{2}", DateTime.Now.Date.Day, DateTime.Now.Month, DateTime.Now.Year);
                string pathname = path + filename;
                if (!File.Exists(pathname))
                {
                    File.Create(pathname);
                }
                File.AppendAllLines(pathname, new string[] { DateTime.Now.ToShortTimeString() + record });
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: LogToFile" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
    }
}
