using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer
{
    public class CrashReport
    {
        private static string pathreport = "CrashReport";
        
    
        public static void CrashReportToFile(params string[] message)
        {
            try
            {
                string filename = string.Format("{0}_{1}_{2}", DateTime.Now.Date.Day, DateTime.Now.Month, DateTime.Now.Year);
                string pathname = pathreport + filename;
                if (!File.Exists(pathname))
                {
                    File.Create(pathname);
                }
                File.AppendAllText(pathname, String.Format("{0} {1}", message[0], message[1]) + Environment.NewLine);
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: CrashReport" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
    }
}
