using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthServer
{
    public class ApiAuth
    {
        Idatabase db;
        public IAuth api;
        public ApiAuth()
        {
            db = new Filedb();
            api = new Destop(db);
        }
      
    }
}
