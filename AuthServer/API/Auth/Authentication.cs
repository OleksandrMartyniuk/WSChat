using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace AuthServer
{
    public class Destop : IAuth
    {
        IPersonDAO db;
        public Destop(IPersonDAO db){
            this.db = db;
        }
        private Object[] json(Object args)
        {
            return JsonConvert.DeserializeObject<object[]>(args.ToString());
        }
        public string LogIn(Person user)
        {
            LinkedList<Person> users = db.Read();
            if (users != null)
            {
                foreach (Person record in users)
                {
                    if (record.name == user.name)
                    {
                        if (record.password == user.password)
                        {
                            LogProvider.AppendRecord(string.Format("loggin user [{0}]", user.name));   
                            return "true";
                        }
                    }
                }
            }
            LogProvider.AppendRecord(string.Format("login and password no correctly [{0}]", user.name));
            return "Please check that you have entered your login and password correctly";
        }
        public string Reg(Person user)
        {
            LinkedList<Person> users = db.Read();
            foreach (Person record in users)
            {
                if (record.name == user.name)
                {
                    LogProvider.AppendRecord(string.Format("registered user exists [{0}]", user.name));
                    return "User exists";
                }
            }
            LogProvider.AppendRecord(string.Format("registered new user [{0}]", user.name));
            db.Create(user);
            return "You have registered"; 
        }
    
        public bool ForgotPassword(string email)
        {
            LinkedList<Person> users = db.Read();
            foreach (Person record in users)
            {
                if (record.email == email)
                {
                    LogProvider.AppendRecord(string.Format("recovery password user [{0}]", record.name));
                    SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 587);
                    Smtp.Credentials = new NetworkCredential("gameXO.helpe@gmail.com", "bilokbilok1");
                    MailMessage Message = new MailMessage();
                    Message.From = new MailAddress("gameXO.helpe@gmail.com");
                    Message.To.Add(new MailAddress(record.email));
                    Message.Subject = "Password";
                    Message.Body = "your password : " + record.password;
                    Smtp.EnableSsl = true;
                    Smtp.Send(Message);
                    LogProvider.AppendRecord(string.Format("Forgot user [{0}]",email));
                    return true;
                }
            }
            return false;
        }
     
    }
}
