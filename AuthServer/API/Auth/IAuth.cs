using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthServer
{
    public interface IAuth
    {
        string LogIn(Person user);
        string Reg(Person user);
        bool ForgotPassword(string email);
    }
}
