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
    public static class AuthProvider
    {
     //   private static string AuthForlder = @"/App_Data/Auth/";
       // private static string List = "PersonList";

        //static AuthProvider()
        //{
        //    //AuthForlder = HttpContext.Current.Server.MapPath(@"/App_Data/Auth/");
        //    //Directory.CreateDirectory(AuthForlder);
        //}
        //public static void AppendRecord(string username, string password)
        //{
        //    //File.AppendAllLines(AuthForlder + List, new string[]
        //    //{
        //    //    JsonConvert.SerializeObject(new AuthList(username,password))
        //    //});
        //}
        //public static string RecordExists(string username,string password)
        //{
        //    //LinkedList<AuthList> list = GetPersonList();
        //    //foreach (AuthList record in list)
        //    //{
        //    //    if (record.userName == username)
        //    //    {
        //    //        if (record.password == password)
        //    //        {
        //    //            return "password";
        //    //        }
        //    //        else
        //    //        {
        //    //            return "login";
        //    //        }
        //    //    }
        //    //}
        //    //return "false";
        //}
    
        //private static LinkedList<Person> GetPersonList()
        //{
        //    //if (!File.Exists(AuthForlder + List))
        //    //{
        //    //    return new LinkedList<AuthList>();
        //    //}
        //    //string[] list = File.ReadAllLines(AuthForlder + List);
        //    //LinkedList<AuthList> records = new LinkedList<AuthList>();
        //    //int length = list.Length;
        //    //for (int i = 0; i < length; i++)
        //    //{
        //    //    AuthList rec = JsonConvert.DeserializeObject<AuthList>(list[i]);
        //    //    records.AddLast(rec);
        //    //}
        //    return records;
        //}
        
        ////private class AuthList
        ////{
        ////    public AuthList(string name, string password)
        ////    {
        ////        this.userName = name;
        ////        this.password = password;
        ////    }
        ////    public string userName { get; set; }
        ////    public string password { get; set; }
            
        ////}
    }
}
