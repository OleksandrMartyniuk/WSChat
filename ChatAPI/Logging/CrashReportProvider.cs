using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public static class CrashReportProvider
    {
        private static string CrashReportFolder = @"/App_Data/CrashReports/";

        static CrashReportProvider()
        {
            Directory.CreateDirectory(CrashReportFolder);
        }
        public static void AppendRecord(string record)
        {
            File.AppendAllLines(CrashReportFolder + DateTime.Today.Date.ToString(), new string[] { DateTime.Now.ToShortTimeString() + record });
        }
    }
}
