using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Person
    {
        public string name;
        public string password;
        public string email;
       // public string status;

        public Person()
        {

        }

        public Person(string name, string password, string email)
        {
            this.name = name;
            this.password = password;
            this.email = email;
        }

        //public Person(string name, string password, string email, string status): this(name, password, email)
        //{
        //    this.status = status;
        //}
    }
}
