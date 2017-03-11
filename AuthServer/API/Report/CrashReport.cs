using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer
{
    public static class CrashReport
    {
        static PersonDAO_MySQL db;
        public static void AppendRecord(string message)
        {
            try
            {
                string date = string.Format("{0}_{1}_{2}", DateTime.Now.Date.Day, DateTime.Now.Month, DateTime.Now.Year);
                db.Create("crash", String.Format("{0} - {1}", date, message));
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: CrashReport" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
    }
}
