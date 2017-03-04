using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    public class Client
    {
        public TcpClient client;
        public NetworkStream netStream;

        public string name;
        public bool inGame;
        public bool isBusy;
        public int type { get; set; }
        
        public Client(TcpClient client)
        {
            this.client = client;
            netStream = this.client.GetStream();
            inGame = false;
            isBusy = false;
        }
        public void Write(RequestObject request)
        {
            string message = JsonConvert.SerializeObject(request);
            try
            {
                if (type == 1)
                {
                    StreamWriter sw = new StreamWriter(client.GetStream());
                    sw.WriteLine(message);
                    sw.Flush();
                }
                else
                {
                    byte[] rawData = Encoding.UTF8.GetBytes(" " + message);
                    int frameCount = 0;
                    byte[] frame = new byte[10];

                    frame[0] = (byte)129;

                    if (rawData.Length <= 125)
                    {
                        frame[1] = (byte)rawData.Length;
                        frameCount = 2;
                    }
                    else if (rawData.Length >= 126 && rawData.Length <= 65535)
                    {
                        frame[1] = (byte)126;
                        int len = rawData.Length;
                        frame[2] = (byte)((len >> 8) & (byte)255);
                        frame[3] = (byte)(len & (byte)255);
                        frameCount = 4;
                    }
                    else
                    {
                        frame[1] = (byte)127;
                        int len = rawData.Length;
                        frame[2] = (byte)((len >> 56) & (byte)255);
                        frame[3] = (byte)((len >> 48) & (byte)255);
                        frame[4] = (byte)((len >> 40) & (byte)255);
                        frame[5] = (byte)((len >> 32) & (byte)255);
                        frame[6] = (byte)((len >> 24) & (byte)255);
                        frame[7] = (byte)((len >> 16) & (byte)255);
                        frame[8] = (byte)((len >> 8) & (byte)255);
                        frame[9] = (byte)(len & (byte)255);
                        frameCount = 10;
                    }

                    int bLength = frameCount + rawData.Length;

                    byte[] reply = new byte[bLength];

                    int bLim = 0;
                    for (int i = 0; i < frameCount; i++)
                    {
                        reply[bLim] = frame[i];
                        bLim++;
                    }
                    for (int i = 0; i < rawData.Length; i++)
                    {
                        reply[bLim] = rawData[i];
                        bLim++;
                    }
                    client.GetStream().Write(reply, 0, reply.Length);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("METHOD: Write" + ex.StackTrace + ex.Message, ex.InnerException);
            }
        }

        public string Read()
        {
            if (type == 1)
            {
                StreamReader sr = new StreamReader(client.GetStream());
                return sr.ReadLine();
            }
            else
            {
                byte[] bytes1 = new byte[client.Available];

                client.GetStream().Read(bytes1, 0, bytes1.Length);

                string inp = DecodeMessage(bytes1);
                return inp;
            }
        }
        public string DecodeMessage(byte[] bytes)
        {
            string incomingData = string.Empty;
            byte secondByte = bytes[1];
            int dataLength = secondByte & 127;
            int indexFirstMask = 2;
            if (dataLength == 126)
                indexFirstMask = 4;
            else if (dataLength == 127)
                indexFirstMask = 10;

            IEnumerable<byte> keys = bytes.Skip(indexFirstMask).Take(4);
            int indexFirstDataByte = indexFirstMask + 4;

            byte[] decoded = new byte[bytes.Length - indexFirstDataByte];
            for (int i = indexFirstDataByte, j = 0; i < bytes.Length; i++, j++)
            {
                decoded[j] = (byte)(bytes[i] ^ keys.ElementAt(j % 4));
            }

            return incomingData = Encoding.UTF8.GetString(decoded, 0, decoded.Length);
        }
     

    }
}
