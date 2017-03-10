using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GameClient.API.Networking;
using Core;

namespace GameClient
{
    public static class ResponseHandler
    {
        static ResponseHandler()
        {
            Client.responseReceived += ProcessResponse;
        }

        public static void ProcessResponse(string Json)
        {
            RequestObject req = JsonConvert.DeserializeObject<RequestObject>(Json);

            switch (req.Module)
            {
                case "Auth":

                    switch (req.Cmd)
                    {
                        case "LogIn": loginSuccess?.Invoke(req.Args.ToString()); break;
                    }
                    break;
                case "Lobby":
                    switch (req.Cmd)
                    {
                        case "refreshClients": refreshClients?.Invoke(req.Args); break;
                        case "Notification": notificationLobby?.Invoke(req.Args); break;
                    }
                    break;
                case "HandShake":
                    switch (req.Cmd)
                    {
                        case "Invited": answer?.Invoke(req.Args); break;
                        case "Cancle": cancle?.Invoke(); break;
                        case "Wait": wait?.Invoke(); break;
                    }
                    break;
                case "Game":
                    switch (req.Cmd)
                    {
                        case "Start":
                            start?.Invoke(req.Args);
                            close?.Invoke();
                            enabled?.Invoke();
                            break;
                        case "Move":
                            move?.Invoke(req.Args);
                        break;
                        case "Over":
                             end?.Invoke();
                        break;
                    }
                    break;
            }
        }

        public delegate void loginDelegate(string username);
        public static event loginDelegate loginSuccess;
        public static event loginDelegate loginFail;
        
        public delegate void lobbyDelegate(object Client);
        public static event lobbyDelegate refreshClients;
        public static event lobbyDelegate notificationLobby;
        public static event lobbyDelegate answer;
        public static event lobbyDelegate start;
        public static event lobbyDelegate move;


        public delegate void notificationDelegate();
        public static event notificationDelegate cancle;
        public static event notificationDelegate wait;
        public static event notificationDelegate close;
        public static event notificationDelegate enabled;
        public static event notificationDelegate end;
    }
}