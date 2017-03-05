using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    public class Client
    {
        public static TcpClient client { get; set; }
        public static NetworkStream netstream;

        public Client()
        {
            try
            {
                client = new TcpClient();
                client.Connect("192.168.0.98", 9898);
                netstream = client.GetStream();
            } catch (Exception e)
            {

            }

        }

        public static void SendMessage(RequestObject str)
        {
            try
            {
                StreamWriter writer = new StreamWriter(netstream);
                writer.WriteLine(JsonConvert.SerializeObject(str));
                writer.Flush();
            }
            catch (Exception e)
            {
                
            }
        }
        public static RequestObject OutStreamRead()
        {
            StreamReader reader = new StreamReader(netstream);
            return JsonConvert.DeserializeObject<RequestObject>(reader.ReadLine());
        }
    }
}
