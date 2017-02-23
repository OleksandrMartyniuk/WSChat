using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class MessageReadonly : RoomObserverBase
    {
        protected override void HandleMessage(IClientObject client, RequestObject request){}
    }
}
