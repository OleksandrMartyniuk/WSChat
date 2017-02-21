using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiRoomChatClient
{
    public class HistoryDataprovider
    {
        private string folder;

        public HistoryDataprovider(string Folder)
        {
            this.folder = Folder + "/";
            Directory.CreateDirectory(folder);
        }

        public void AppendMessage(string room, ChatMessage message)
        {
            FileAppend(folder + room, new string[] { JsonConvert.SerializeObject(message) });
        }

        public void AppendSequence(string room, ChatMessage[] messages)
        {
            List<ChatMessage> history = new List<ChatMessage>(GetHistory(room));
            if (history.Count == 0)
                return;
            int i = history.Count;
            while(i > 0)
            {
                if(messages[0].TimeStamp == history[i-1]?.TimeStamp)
                {
                    //i++;
                    return;
                }
                i--;
            }
            history.RemoveRange(i, history.Count - i);
            for(int j =i; j < messages.Length; j++)
            {
                history.Add(messages[j]);
            }

            string[] text = new string[history.Count];
            for(int j = 0; j< text.Length-1; j++)
            {
                 text[j] = JsonConvert.SerializeObject(messages[j]);
            }
            FileAppend(folder + room, text);
        }
        private void FileAppend(string room, string[] text)
        {
            try
            {
                File.AppendAllLines(room, text);
            }
            catch (Exception e)
            {

            }
        }

        public ChatMessage[] GetHistory(string roomName, ChatMessage last = null)
        {
            if (!File.Exists(folder + roomName))
            {
                return new LinkedList<ChatMessage>().ToArray();
            }

            string[] list = File.ReadAllLines(folder + roomName);
            LinkedList<ChatMessage> messages = new LinkedList<ChatMessage>();
            int length = list.Length;
            for (int i = 0; i < length; i++)
            {
                ChatMessage msg = JsonConvert.DeserializeObject<ChatMessage>(list[length - 1 - i]);
                if(last != null && last.TimeStamp == msg.TimeStamp)
                {
                    break;
                }
                messages.AddFirst(msg);
            }
            return messages.ToArray();
        }

    }
}
