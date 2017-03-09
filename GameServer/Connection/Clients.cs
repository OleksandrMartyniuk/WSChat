using AuthServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameServer
{
    public class Clients
    {
        public List<Client> clientsList;
      
        public Clients()
        {
            clientsList = new List<Client>(); 
        }

        public void Add(TcpListener server)
        {
            TcpClient tcpClient = server.AcceptTcpClient();
            Client client = new Client(tcpClient);
            while (!client.client.GetStream().DataAvailable);

            byte[] bytes = new byte[client.client.Available];

            client.client.GetStream().Read(bytes, 0, bytes.Length);

            string inp = Encoding.UTF8.GetString(bytes);

            if (new Regex("^GET").IsMatch(inp))
            {
                Byte[] response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + Environment.NewLine
                    + "Connection: Upgrade" + Environment.NewLine
                    + "Upgrade: websocket" + Environment.NewLine
                    + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                        SHA1.Create().ComputeHash(
                            Encoding.UTF8.GetBytes(
                                new Regex("Sec-WebSocket-Key: (.*)").Match(inp).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                            )
                        )
                    ) + Environment.NewLine
                    + Environment.NewLine);

                client.client.GetStream().Write(response, 0, response.Length);
                client.type = 0;
            }
            else
            {
                client.type = 1;
            }
            clientsList.Add(client);
        }

        public void Dell(Client client)
        {
            LogProvider.AppendRecord(string.Format("Remove client [{0}]", client.name));
            clientsList.Remove(client);
        }
    }
}
