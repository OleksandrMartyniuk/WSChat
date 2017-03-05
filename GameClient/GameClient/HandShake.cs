using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    public class HandShake
    {
       
        public EventHandler Answer;
        public EventHandler Cancle;
        public EventHandler Wait;

        public HandShake()
        {
        }

        public void Dispacher(RequestObject tmpinfo)
        {
            switch (tmpinfo.Cmd)
            {
                case "Invited":
                    Answer(tmpinfo.Args, null);
                    break;
                case "Cancle":
                    Cancle(null, null);
                    break;
                case "Wait":
                    Wait(null, null);
                    break;
            }
        }
      
        public void SendInvite(object args)
        {
            Listener.SendMessage(new RequestObject("HandShake","Invite", args));
        }
        public void SendOk(object args)
        {
            Listener.SendMessage(new RequestObject("HandShake", "Ok", args));
        }

        public void SendCancle(object args)
        {
            Listener.SendMessage(new RequestObject("HandShake", "Cancle", args));
        }
    }
}
