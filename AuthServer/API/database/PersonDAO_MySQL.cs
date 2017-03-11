using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Core;

namespace AuthServer
{
    class PersonDAO_MySQL : IPersonDAO
    {
        private MySqlConnection con;
        private void ConnectSQL()
        {
            try
            {
                con = new MySqlConnection("Server=MYSQL5018.myASP.NET;Database=db_a18fa2_alexand;Uid=a18fa2_alexand;Pwd=xzxz123456");
                con.Open();
            }
            catch(MySqlException ex)
            {
                throw new Exception("METHOD: Open" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }
        private void Select(String str)
        {
            ConnectSQL();
            try
            {
                MySqlCommand iniQuery = new MySqlCommand(str, con);
                iniQuery.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        public void Create(Person p)
        {
            Select(String.Format("INSERT INTO person(id, name, pass, email) VALUES(null, '{0}', '{1}', '{2}')", p.name, p.password, p.email));
        }
        public void Create(params string[] p)
        {
            Select(String.Format("INSERT INTO {0} (id, name) VALUES(null, {1})",  p[0],  p[1]));
        }
        public LinkedList<Person> Read()
        {
            LinkedList<Person> pp = new LinkedList<Person>();
            ConnectSQL();
            try
            {
                DbCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT name, pass, email FROM person";
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pp.AddLast(new Person(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: Create" + ex.StackTrace + ex.Message, ex.InnerException);
            }
            return pp;
        }
    
    }
}
