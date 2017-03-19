using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Roles
{
    public class BannedUser : RoleBase
    {
        public DateTime banTill;
        public BannedUser(IClientObject client, DateTime? banTill): base(client)
        {
            MessageReadonly ro = new MessageReadonly();
            ro.client = this.client;
            Handlers = new IHandlerModule[] {
                new Logout(),
                new Info(),
                ro,
                new Private() };
            this.client = client;

            if (banTill != null || banTill != DateTime.MaxValue)
            {
                Task.Factory.StartNew(async() => {
                    TimeSpan taskRunTime = banTill.Value.Subtract(DateTime.Now);
                    if(taskRunTime.TotalMilliseconds > 0)
                    {
                        await Task.Delay(taskRunTime);
                    }
                    TrackBlackList(client.Username);
                });
            }
        }


        public void TrackBlackList(string username)
        {
            IClientObject user = Manager.FindClient(username);
            
            string tmp = ResponseConstructor.GetUnBannedNotification(username);
            user.SendMessage(tmp);
            user.Role = new User(user);
        }
    }

        
}
