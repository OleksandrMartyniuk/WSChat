﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiRoomChatClient.API.Networking
{
    public interface IClient
    {
        void StartClient();
        
        void AddRequest(string message);
      
        void Disconnect();

        event responseHandler responseReceived;
        event errorMessage NewErrorMessage;
        event connectionHandler ConnectionOpened;
    }
    public delegate void responseHandler(string json);
    public delegate void errorMessage(string msg);
    public delegate void connectionHandler();
}
