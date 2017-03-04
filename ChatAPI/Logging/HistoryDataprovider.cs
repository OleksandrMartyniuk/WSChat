using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ChatServer
{
    public static class HistoryDataprovider
    {
        private static string PublicFolder;
        private static string PrivateFolder;

        static HistoryDataprovider()
        {
            PublicFolder = HttpContext.Current.Server.MapPath(@"/App_Data/Msg/Rooms/");
            PrivateFolder = HttpContext.Current.Server.MapPath(@"/App_Data/Msg/Private/");
            Directory.CreateDirectory(PublicFolder);
            Directory.CreateDirectory(PrivateFolder);
        }

        public static void AppendMessage(string roomName, ChatMessage message)
        {
            File.AppendAllLines(PublicFolder + roomName, new string[] { JsonConvert.SerializeObject(message) });
        }

        public static void AppendPrivateMessage(string user1, string user2, ChatMessage message)
        {
            File.AppendAllLines(FindPrivateHistory(user1, user2), new string[] { JsonConvert.SerializeObject(message) });
        }

        private static string FindPrivateHistory(string user1, string user2)
        {
            string fileName = PrivateFolder + user1 + "+" + user2;
            if (!File.Exists(fileName))
            {
                fileName = PrivateFolder + user2 + "+" + user1;
            }
            return fileName;
        }

        public static ChatMessage[] GetPrivateHistory(string user1, string user2)
        {
            return GetHistoryFromFile(FindPrivateHistory(user1, user2));
        }

        public static ChatMessage[] GetPrivateHistory(string user1, string user2, DateTime last)
        {
            if(last == default(DateTime))
            {
                last = DateTime.Now;
            }
            ChatMessage[] msgs = GetHistoryFromFile(FindPrivateHistory(user1, user2));
            if(msgs.Length == 0)
            {
                return msgs;
            }

            LinkedList<ChatMessage> res = new LinkedList<ChatMessage>();

            for(int i = msgs.Length-1, j = 0; i > 0 && j < 10; i--)
            {
                if(msgs[i].TimeStamp < last)
                {
                    res.AddLast(msgs[i]);
                    j++;
                }
            }
            return res.ToArray();
        }

        public static ChatMessage[] GetHistory(string roomName)
        {
            return GetHistoryFromFile(PublicFolder + roomName);
        }

        private static ChatMessage[] GetHistoryFromFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            string[] list = File.ReadAllLines(path);
            LinkedList<ChatMessage> messages = new LinkedList<ChatMessage>();
            int length = list.Length;
            for (int i = 0; i < length; i++)
            {
                ChatMessage msg = JsonConvert.DeserializeObject<ChatMessage>(list[i]);
                messages.AddLast(msg);
            }
            return messages.ToArray();
        }
    }
}
