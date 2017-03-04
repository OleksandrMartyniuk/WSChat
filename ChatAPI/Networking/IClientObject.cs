using ChatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public interface IClientObject
    {
        string Username { get; set; }
        Roles.RoleBase Role { get; set; }

        event handler MessageRecieved;

        void Start();

        void SendMessage(string message);

        void Close();
    }


}
