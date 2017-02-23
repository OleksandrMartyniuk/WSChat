using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Roles
{
    public abstract class RoleBase
    {
        protected IHandlerModule[] Handlers;
        protected IClientObject client;

        public RoleBase(IClientObject clnt)
        {
            this.client = clnt;
        }

        public virtual void Handle(IClientObject client, string request)
        {
            RequestObject req = JsonConvert.DeserializeObject<RequestObject>(request);
            foreach(IHandlerModule module in client.Role.Handlers)
            {
                if(module.Handle(client, req))
                {
                    break;
                }
            }
        }

    }
}
