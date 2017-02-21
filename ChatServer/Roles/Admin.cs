using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Roles
{
    public class Admin : RoleBase
    {
        public Admin(ClientObject clnt): base(clnt)
        {
            RoomObserver ro = new RoomObserver();
            ro.client = clnt;
            Handlers = new IHandlerModule[] {
                new Logout(),
                new Info(),
                ro,
                new Private(),
                new Room(),
                new AdminModule() };
        }
    }
}
