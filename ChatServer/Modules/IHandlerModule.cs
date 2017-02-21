using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public interface IHandlerModule
    {
        bool Handle(ClientObject client, RequestObject request);
    }
}
