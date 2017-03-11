using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace XamarinXO.Droid
{
    public class Writer
    {
        public Auth auth = new Auth();
        public Lobby lobby = new Lobby();
        public HandShake handShake = new HandShake();
        public Game game = new Game();



        public class Auth
        {
            public Request data;
            public string SendLogIn(string login, string password)
            {
                Request data = new Request("Auth", "LogIn", new object[] { login, password });
                return JsonConvert.SerializeObject(data);
            }

            public string SendRegistaration(string login, string password, string email)
            {
                data = new Request("Auth", "Registration", new Object[] { login, password, email });
                return JsonConvert.SerializeObject(data);
            }

            public string SendLogOut(string login)
            {
                data = new Request("Auth", "LogOut", login);
                return JsonConvert.SerializeObject(data);
            }
            public string SendForget(string login)
            {
                data = new Request("Auth", "Forget", login);
                return JsonConvert.SerializeObject(data);
            }


        }

        public class Lobby
        {
            public Request data;
            public string SendRefresh()
            {
                data = new Request("Lobby", "refreshClients", "null");
                return JsonConvert.SerializeObject(data);
            }
        }

        public class HandShake
        {
            public Request data;
            public string SendInvite(object player)
            {
                data = new Request("HandShake", "Invite", new object[] { player, "XO" });
                return JsonConvert.SerializeObject(data);
            }

            public string SendOk(object play)
            {
                data = new Request("HandShake", "Ok", play);
                return JsonConvert.SerializeObject(data);
            }

            public string SendCancel(object player)
            {
                data = new Request("HandShake", "Cancle", new object[] { player, "XO" });
                return JsonConvert.SerializeObject(data);
            }
        }

        public class Game
        {
            public Request data;

            public string SendMove(int roomNumber, string playerName ,string playerMove, int number)
            {
                data = new Request("Game", "Move", new object[] { roomNumber, playerMove, number, playerName });
                return JsonConvert.SerializeObject(data);
            }

            public string SendStart(object info)
            {
                data = new Request("Game", "Move", info);
                return JsonConvert.SerializeObject(data);
            }

        }
    }
}