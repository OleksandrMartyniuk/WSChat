using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Core;

namespace ChatServer
{
    public class PersonDAO_MySQL
    {
        public PersonDAO_MySQL()
        {
        }
        /*
        table  db_a18fa2_alexand
            

MYSQL:            mysql5018.myasp.net
Имя пользователя: a18fa2_alexand
password:         xzxz123456
          */
        private MySqlConnection ConnectSQL()
        {
            MySqlConnection con = new MySqlConnection();
            //try
            //{
            //  //  con = new MySqlConnection("Server=mysql.zzz.com.ua;Database=unreal;Uid=unreal;Pwd=bilokbilok1");
            //    con = new MySqlConnection("Server=mysql5018.myasp.net;Database=db_a18fa2_alexand;Uid=a18fa2_alexand;Pwd=xzxz123456");
            //    con.Open();
            //}
            //catch(MySqlException ex)
            //{
            //    throw ex;
            //}
            return con;
        }
        private void Select(String str)
        {
            var con = ConnectSQL();
            //try
            //{
            //    MySqlCommand iniQuery = new MySqlCommand(str, con);
            //    iniQuery.ExecuteNonQuery();
            //    iniQuery.Dispose(); 
            //}
            //catch (MySqlException ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    con.Close();
            //}
         
        }
        public void Create(Person p)
        {
            Select("INSERT INTO person(id, name, pass, email) VALUES(null,\"5\", \"5\", \"jooooopa@mail.r\")");
        }
        public void Delete(Person p)
        {
            Select(String.Format("DELETE FROM person WHERE name=" + p.name));
        }
        public void Update(Person p)
        {
            Select(String.Format("UPDATE person SET name='{0}', pass='{1}', email='{2}' WHERE email='{2}'",  p.name, p.password, p.email));
        }
        //public bool SendSql(Person p)
        //{
        //    bool f;
        //    var con = ConnectSQL();
        //    try
        //    {
        //        DbCommand cmd = con.CreateCommand();
        //        cmd.CommandText = string.Format("SELECT name, pass FROM person  WHERE name = '{0}' AND pass = '{1}'", p.name, p.password);
        //        DbDataReader reader  = cmd.ExecuteReader();
        //        if (reader == null)
        //        {
        //            f = false;
        //        }
        //        else
        //        {
        //            f = true;
        //        }
        //        reader.Close();
        //        con.Close();
        //        cmd.Dispose();

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return f;
        //}
        public LinkedList<Person> Read()
        {
            LinkedList<Person> pp = new LinkedList<Person>();
            var con = ConnectSQL();
            try
            {
                DbCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT name,pass,email FROM person";
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pp.AddLast(new Person(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                }
                reader.Close();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
           // pp.AddLast(new Person("1", "1", "1"));
            return pp;
        }
    
    }
}
