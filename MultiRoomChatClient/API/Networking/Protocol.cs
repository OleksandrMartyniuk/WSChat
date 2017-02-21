using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiRoomChatClient.API.Networking
{
    public static class Protocol
    {
        public static ProtocolTypes type;
    }

    public enum ProtocolTypes
    {
        TCP,
        WebSocket
    }
}
