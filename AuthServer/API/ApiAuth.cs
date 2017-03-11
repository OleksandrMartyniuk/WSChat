using AuthServer;
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
            IPersonDAO db = new PersonDAO_MySQL();
            api = new Destop(db);
        }
      
    }
}
