using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MultiRoomChatClient.API
{
    public class AuthServerClient
    {
        public void Login(string login, string password)
        {
            string URI = ConfigurationManager.AppSettings["auth_server_host"] + ConfigurationManager.AppSettings["auth_login_handler"];
            var values = new Dictionary<string, string>();
            values.Add("login", login);
            values.Add("password", password);

            var request = WebRequest.Create(URI);
            request.Method = "POST";
            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.WriteLine(JsonConvert.SerializeObject(values));
            }

            var response = request.GetResponse();
            string body = null;
            string key = response.Headers["access-key"];

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                body = sr.ReadLine();
            }

            switch (body)
            {
                case "ok":
                    login_Successfull?.Invoke(key);
                    break;
                case "login":
                    login_WrongUsername?.Invoke();
                    break;
                case "password":
                    login_WrongPassword?.Invoke();
                    break;
                default:
                    break;
            }
        }

        public void Register(string login, string password, string email)
        {
            string URI = ConfigurationManager.AppSettings["auth_server_host"] + ConfigurationManager.AppSettings["auth_registration_handler"];

            var values = new Dictionary<string, string>();
            values.Add("login", login);
            values.Add("password", password);
            values.Add("email", email);

            var request = WebRequest.Create(URI);
            request.Method = "POST";
            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.WriteLine(JsonConvert.SerializeObject(values));
            }

            var response = request.GetResponse();
            string body = null;
            
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                body = sr.ReadLine();
            }

            switch (body)
            {
                case "ok":
                    reg_OK?.Invoke();
                    break;
                case "login":
                    reg_UsernameExists?.Invoke();
                    break;
                case "email":
                    reg_EmailExists?.Invoke();
                    break;
                case "login&email":
                    reg_EmailUsernameExists?.Invoke();
                    break;
                default:
                    break;
            }
        }

        public void PasswordRecovery(string email)
        {
            string URI = ConfigurationManager.AppSettings["auth_server_host"] + ConfigurationManager.AppSettings["auth_passwordRecovery_handler"];

            var request = WebRequest.Create(URI);
            request.Method = "POST";
            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.WriteLine(email);
            }

            var response = request.GetResponse();
            string body = null;

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                body = sr.ReadLine();
            }

            switch (body)
            {
                case "ok":
                    passRecovery_Successfull?.Invoke();
                    break;
                case "not_found":
                    passRecovery_WrongEmail?.Invoke();
                    break;
                default:
                    break;
            }
        }

        public delegate void AuthResultHandlerKey(string key);
        public delegate void AuthResultHandler();

        public event AuthResultHandlerKey login_Successfull;
        public event AuthResultHandler login_WrongPassword;
        public event AuthResultHandler login_WrongUsername;
        public event AuthResultHandler reg_OK;
        public event AuthResultHandler reg_UsernameExists;
        public event AuthResultHandler reg_EmailExists;
        public event AuthResultHandler reg_EmailUsernameExists;
        public event AuthResultHandler passRecovery_WrongEmail;
        public event AuthResultHandler passRecovery_Successfull;
    }
}
