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

            Task removeByTimeout = new Task(async () =>
            {
                await Task.Delay(120000);
                pool.Remove(record);
            });
            removeByTimeout.Start();
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
            return res;
        }

        public class PoolObject
        {
            public PoolObject(string username, string key, int status, string banTill)
            {
                this.Key = key;
                this.Username = username;
                this.status = (AuthStatus)status;
                if (banTill != null && banTill != "")
                    this.banTill = DateTime.Parse(banTill);
                else
                    this.banTill = null;
            }
            public string Key { get; set; }
            public string Username { get; set; }
            public AuthStatus status { get; set; }
            public DateTime? banTill { get; set; }
        }
    }
}