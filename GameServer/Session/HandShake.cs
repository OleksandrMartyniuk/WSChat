using AuthServer;
using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class HandShake
    {
        private Clients clients { get; set; }
        Rooms rooms { get; set; }
      
        public HandShake(Clients clients, Rooms rooms)
        {
            this.clients = clients;
            this.rooms = rooms;
          
        }

        public void Dispacher(Client client, RequestObject info)
        {
            object []arg = JsonConvert.DeserializeObject<object[]>(info.Args.ToString());
            switch (info.Cmd)
            {
                case "Invite":
                    if (arg[0] != null && arg[1] != null)
                        Invite(client, arg[0].ToString(), arg[1].ToString());
     
                    break;
                case "Ok":
                    Start(client, arg[0].ToString(), arg[1].ToString());
                    break;
                case "Cancle":
                    Cancle(client, arg[0].ToString());
                    break;
            }
        }

        private void Invite(Client clientcreator, string invitedName, string gameName)
        {
            clientcreator.isBusy = true;
            Client clientinvited = clients.clientsList.Find(c => c.name == invitedName);
            
            if(clientinvited.inGame)
            {
                Lobby.SendNotification("This user is already in the game", clientinvited);
                Lobby.SendClients(clientinvited, clients.clientsList);
                return;
            }
            if (clientinvited==null)
            {
                Lobby.SendNotification("This user has already logged out", clientinvited);
                Lobby.SendClients(clientinvited, clients.clientsList);
                return;
            }
            if(clientinvited.isBusy)
            {
                Lobby.SendNotification("This user is busy", clientcreator);
                clientcreator.isBusy = false;
                return;
            }
            clientinvited.isBusy = true;
            LogProvider.AppendRecord(string.Format("{0} invited client [{1}] vs game [{2}]", 
                DateTime.Now.ToString(), clientcreator.name,gameName));

            clientinvited.Write(new RequestObject("HandShake", "Invited", 
                new object[] { clientcreator.name, gameName }));


            LogProvider.AppendRecord(string.Format("{0} Wait [{1}]", DateTime.Now.ToString(), clientcreator.name));
            clientcreator.Write(new RequestObject("HandShake", "Wait", null));
        }

        private void Start(Client invitedClient, string creatorName, string gameName)
        {
            Client creatorClient = this.clients.clientsList.Find(c=> c.name == creatorName);
            List<Client> tmpclients = new List<Client>();
            tmpclients.Add(creatorClient);
            tmpclients.Add(invitedClient);

            rooms.Add(tmpclients, gameName);

            for(int i=0; i<tmpclients.Count; i++)
            {
                LogProvider.AppendRecord(string.Format("{0} Start [{1}]", DateTime.Now.ToString(), tmpclients[i].name));
                tmpclients[i].Write(new RequestObject("Game", "Start", 
                    new object[] { rooms.rooms.Count - 1, gameName }));
            }
        }
        private void Cancle(Client invitedClient, string creatorName)
        {
            Client creator = clients.clientsList.Find(c => c.name == creatorName);
            creator.isBusy = false;
            invitedClient.isBusy = false;
            LogProvider.AppendRecord(string.Format("{0} handShake Cancle [{1}]", DateTime.Now.ToString(), creatorName));
            creator.Write(new RequestObject("HandShake", "Cancle", null));
        }
    }
}
