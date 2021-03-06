﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;

namespace ChatServer
{
    public class Gmail : LogInBase
    {
        public override bool Handle(IClientObject client, RequestObject request)
        {
            if (request.Module != "Gmail")
            {
                return false;
            }
            string name = request.Args.ToString();

            LogProvider.AppendRecord(string.Format("{0} loggin gmail user [{1}]", DateTime.Now.ToString(), name));
            client.Username = name;
            ResolveStatus(client);
            return true;
        }
    }
}