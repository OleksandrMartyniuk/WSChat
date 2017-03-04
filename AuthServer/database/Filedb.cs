using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace AuthServer
{
    public class Filedb: Idatabase
    {
        private string tablename = "user";
        public Filedb() { }
   
        public void Create(Person user)
        {
            try
            {
                File.AppendAllLines(tablename, new string[] { JsonConvert.SerializeObject(user) });
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: Create" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
        public LinkedList<Person> Read()
        {
            LinkedList<Person> records = new LinkedList<Person>();
            if (!File.Exists(tablename))
            {
                return records;
            }
            string[] json = File.ReadAllLines(tablename);
            if (json.Length == 0)
                return records;

            int length = json.Length;
            for (int i = 0; i < length; i++)
            {
                records.AddLast(JsonConvert.DeserializeObject<Person>(json[i].ToString()));
            }

            return records;
        }
       

        public void Update(string login, string password)
        {

           // sqlsend("UPDATE '" + tablename + "' SET password = '" + password + "' WHERE login= '" + login + "';");
        }

        public void Delete()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: Delete" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
    }
}
