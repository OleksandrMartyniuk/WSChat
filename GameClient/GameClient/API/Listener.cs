using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using GameClient.API.Networking;

namespace GameClient
{
    public class Listener
    {
        public Listener() {}
        public void SendInvite(object args)
        {
            Client.AddRequest(new RequestObject("HandShake", "Invite", args));
        }
        public void SendOk(object args)
        {
            Client.AddRequest(new RequestObject("HandShake", "Ok", args));
        }

        public void SendCancle(object args)
        {
            Client.AddRequest(new RequestObject("HandShake", "Cancle", args));
        }
    }

}
