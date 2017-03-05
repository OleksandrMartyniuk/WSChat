using AuthServer;
using Nemiro.OAuth.LoginForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AuthServer
{
    public class ApiAuth
    {
        public IAuth api;
        public ApiAuth()
        {
            Idatabase db = new Filedb();
            api = new Destop(db);
        }
    }
}
