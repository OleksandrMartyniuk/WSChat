using ChatServer.ChatAPI.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Roles
{
    public class UnknownUser : RoleBase
    {
        public UnknownUser(IClientObject clnt): base(clnt)
        {
            Handlers = new IHandlerModule[] {
                new AuthModule()
            };
        }
    }
}
