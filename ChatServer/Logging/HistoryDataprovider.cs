using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public static class HistoryDataprovider
    {
        private static string Folder ="Msg/";

        static HistoryDataprovider()
        {
            Directory.CreateDirectory(Folder);
        }

        public static void AppendMessage(string roomName, ChatMessage message)
        {
            File.AppendAllLines(Folder + roomName, new string[] {JsonConvert.SerializeObject(message)});
        }

        public static ChatMessage[] GetHistory(string roomName)
        {
            if (!File.Exists(Folder + roomName))
            {
                return null;
            }

            string[] list = File.ReadAllLines(Folder + roomName);
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
