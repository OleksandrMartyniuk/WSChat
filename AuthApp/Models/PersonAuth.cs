using Core;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthApp.Models
{
    public class PersonAuth: Person
    {
        public PersonAuth()
        {

        }
        public PersonAuth(string username, string email, string password, int status)
        {
            this.name = username;
            this.email = email;
            this.password = password;
            this.status = (AuthStatus)status;
        }
        public AuthStatus status { get; set; }
    }

    
}