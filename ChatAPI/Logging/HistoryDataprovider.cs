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
            PublicFolder = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Msg/Rooms/";
            PrivateFolder = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Msg/Private/";
            Directory.CreateDirectory(PublicFolder);
            Directory.CreateDirectory(PrivateFolder);
        }

        public static void AppendMessage(string roomName, ChatMessage message)
        {
            ChatMessage msg = new ChatMessage(message.Sender, message.Text, message.TimeStamp.ToUniversalTime());
            File.AppendAllLines(PublicFolder + roomName, new string[] { JsonConvert.SerializeObject(msg) });
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

        public static LinkedList<ChatMessage> GetPrivateHistory(string user1, string user2)
        {
            return GetHistoryFromFile(FindPrivateHistory(user1, user2));
        }

        public static ChatMessage[] GetPrivateHistory(string user1, string user2, DateTime time)
        {
            LinkedList<ChatMessage> Messages = GetHistoryFromFile(FindPrivateHistory(user1, user2));

            LinkedList<ChatMessage> msgs = new LinkedList<ChatMessage>();

            LinkedListNode<ChatMessage> next = Messages.Last;

            while (next != null && next.Value.TimeStamp >= time)
            {
                next = next.Previous;
            }

            if (next == null)
            {
                return msgs.ToArray();
            }

            LinkedListNode<ChatMessage> current = next;

            for (int i = 0; i <= 10; i++)
            {
                if (current != null)
                {
                    msgs.AddFirst(current.Value);
                }
                else
                {
                    break;
                }
                current = current.Previous;
            }

            return msgs.ToArray();
        }

        internal static void RemoveHistory(string roomName)
        {
            string path = PublicFolder + roomName;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static LinkedList<ChatMessage> GetHistory(string roomName)
        {
            return GetHistoryFromFile(PublicFolder + roomName);
        }

        private static LinkedList<ChatMessage> GetHistoryFromFile(string path)
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
            return messages;
        }
    }
}
