using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChatServer.AuthApi
{
    public static class AuthPool
    {
        static LinkedList<PoolObject> pool = new LinkedList<PoolObject>();

        public static void AppendRecord(PoolObject record)
        {
            pool.AddLast(record);
        }

        public static PoolObject GetRecordByKey(string key)
        {
            PoolObject res = null;
            foreach(PoolObject p in pool)
            {
                if(p.Key == key)
                {
                    res = p;
                    break;
                }
            }
            res.Active = true;
            return res;
        }

        public static void BeginRemoveObject(string username)
        {
            LinkedListNode<PoolObject> r = pool.First;

            while(r != pool.Last)
            {
                if(r.Value.Username == username)
                {
                    break;
                }
            }
            r.Value.Active = false;
            Task removeByTimeout = new Task(async () =>
            {
                await Task.Delay(30000);

                if(!r.Value.Active)
                    pool.Remove(r.Value);
            });
            removeByTimeout.Start();
        }

        public class PoolObject
        {
            public PoolObject(string username, string key, int status, string banTill)
            {
                this.Key = key;
                this.Username = username;
                this.status = (AuthStatus)status;
                this.Active = true;
                if (banTill != null && banTill != "")
                    this.banTill = DateTime.Parse(banTill);
                else
                    this.banTill = null;
            }
            public string Key { get; set; }
            public bool Active { get; set; }
            public string Username { get; set; }
            public AuthStatus status { get; set; }
            public DateTime? banTill { get; set; }
        }
    }
}