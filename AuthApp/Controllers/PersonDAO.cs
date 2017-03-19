using AuthApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using Core.Models;

namespace AuthApp.Controllers
{
    public class PersonDAO
    {
        string connectionString;
        private MySqlConnection connection;
        public PersonDAO()
        {
            connectionString = ConfigurationManager.AppSettings["MySql"];
            connection = null;
        }

        public enum LoginStatus
        {
            OK,
            WrongLogin,
            WrongPassword
        }

        public enum RegistrationStatus
        {
            OK,
            LoginExists,
            EmailExists,
            LoginAndEmailExists
        }

        public LoginStatus TryLogIn(string login, string password, out PersonAuth p)
        {
            p = GetPerson("username", login);
            if(p == null)
            {
                return LoginStatus.WrongLogin;
            }
            else if(p.password != password)
            {
                return LoginStatus.WrongPassword;
            }
            else
            {
                if(p.status == AuthStatus.Banned && p.banTill < DateTime.Now)
                {
                    SetStatus(login, 0, null);
                    p.status = 0;
                    p.banTill = null;
                }
                return LoginStatus.OK;
            }
        }

        public bool ResetPassword(string email, string pass)
        {
            if (connection == null)
            {
                connection = new MySqlConnection(connectionString);
            }
            string insertScript = String.Format("UPDATE `Persons` SET `password`='{0}' WHERE `email`='{1}'", pass, email);
            MySqlCommand iniQuery = null;
            int res = 0;
            try
            {
                connection.Open();
                iniQuery = new MySqlCommand(insertScript, connection);
                res = iniQuery.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                //
                //
                //
            }
            connection.Close();
            return res == 1 ? true : false;
        }

        public bool SetStatus(string username, AuthStatus status, DateTime? datetime)
        {
            if (connection == null)
            {
                connection = new MySqlConnection(connectionString);
            }

            string insertScript = null;

            if (datetime != null)
            {
                insertScript = String.Format("UPDATE `Persons` SET `status`='{0}',`banTill`='{1}' WHERE `username`='{2}'", (int)status, datetime.Value.ToString(), username);
            }
            else
            {
                insertScript = String.Format("UPDATE `Persons` SET `status`='{0}',`banTill`='' WHERE `username`='{1}'", (int)status, username);
            }
            
            MySqlCommand iniQuery = null;
            int res = 0;
            try
            {
                connection.Open();
                iniQuery = new MySqlCommand(insertScript, connection);
                res = iniQuery.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                //
                //
                //
            }
            connection.Close();
            return res == 1 ? true : false;
        }

        public RegistrationStatus TryRegister(string login, string pass, string email)
        {
            RegistrationStatus status = RegistrationStatus.OK;
            if (connection == null)
            {
                connection = new MySqlConnection(connectionString);
            }

            var person = new PersonAuth();
            person.name = login;
            person.email = email;
            person.password = pass;
            person.status = AuthStatus.User;

            string insertScript = String.Format(@"INSERT INTO `Persons`(`email`,`username`,`password`,`status`) VALUES('{0}', '{1}', '{2}', '{3}')", 
                person.email, 
                person.name, 
                person.password, 
                (int)person.status);
            MySqlCommand iniQuery = null;

            try
            {
                connection.Open();
                iniQuery = new MySqlCommand(insertScript, connection);
                iniQuery.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                if(ex.Number == 1062)
                {
                    if (ex.Message.Contains(email))
                    {
                        status = RegistrationStatus.EmailExists;
                    }
                    if (ex.Message.Contains(login))
                    {
                        status = status == RegistrationStatus.EmailExists? RegistrationStatus.LoginAndEmailExists: RegistrationStatus.LoginExists;
                    }
                }
            }
            connection.Close();

            return status;
        }

        public PersonAuth GetPerson(string key, string value)
        {
            PersonAuth res = null;

            if (connection == null)
            {
                connection = new MySqlConnection(connectionString);
            }
            MySqlCommand iniQuery = null;
            MySqlDataReader dataReader = null;
            string selectQuery = string.Format(
                "SELECT `username`, `email`, `password`, `status`, `banTill` FROM  `Persons` WHERE `{0}`='{1}'", key, value);
            try
            {
                connection.Open();
                iniQuery = new MySqlCommand(selectQuery, connection);
                dataReader = iniQuery.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                return null;
            }

            if (dataReader.Read())
            {
                res = new PersonAuth(
                    dataReader.GetString("username"),
                    dataReader.GetString("email"),
                    dataReader.GetString("password"),
                    dataReader.GetInt32("status"),
                    dataReader.GetString("banTill"));
            }

            dataReader.Close();
            dataReader.Dispose();
            iniQuery.Dispose();
            connection.Close();

            return res;
        }
    }
}