using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Roles
{
    public class BannedUser : RoleBase
    {

        public BannedUser(IClientObject client): base(client)
        {
            MessageReadonly ro = new MessageReadonly();
            ro.client = this.client;
            Handlers = new IHandlerModule[] {
                new Logout(),
                new Info(),
                ro,
                new Private() };
            this.client = client;
            TrackBlackList(client.Username);
        }


        public async Task TrackBlackList(string username)
        {
            DateTime timeLeft = BlackListProvider.GetDateTillBanDiscard(username);
            TimeSpan taskRunTime = DateTime.Now.Subtract(timeLeft);
            if(taskRunTime.Ticks>0)
            {
                taskRunTime = new TimeSpan(0);
            }
            await Task.Delay(taskRunTime);
            
            BlackListProvider.RemoveRecord(username);
            IClientObject user = Manager.FindClient(username);
            
            string tmp = ResponseConstructor.GetUnBannedNotification(username);
            user.SendMessage(tmp);
            user.Role = new User(user);
          
        }
    }

        
}
