using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AuthServer
{
    public static class LogProvider
    {
        static PersonDAO_MySQL db = new PersonDAO_MySQL();
        public static void AppendRecord(string message)
        {
            try
            {
                string date = string.Format("{0}_{1}_{2}", DateTime.Now.Date.Day, DateTime.Now.Month, DateTime.Now.Year);
                db.Create("log", String.Format("'{0} - {1}'", date, message));
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: CrashReport" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
        //private static string path = "Log/";
        //public static void AppendRecord(string record)
        //{
        //    try
        //    {
        //        string filename = string.Format("{0}_{1}_{2}", DateTime.Now.Date.Day, DateTime.Now.Month, DateTime.Now.Year);
        //        string pathname = path + filename;
        //        if (!File.Exists(pathname))
        //        {
        //            File.Create(pathname);
        //        }
        //        File.AppendAllText(pathname, DateTime.Now.ToShortDateString()+" - "+ record + Environment.NewLine);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("METHOD: LogToFile" + ex.StackTrace + ex.Message, ex.InnerException);
        //    }
        //}
    }
}