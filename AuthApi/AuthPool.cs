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

        public static void AppendRecord(string username, string key, int status)
        {
            PoolObject record = new PoolObject(username, key, status);
            pool.AddLast(record);

            Task removeByTimeout = new Task(() =>
            {
                Task.Delay(new TimeSpan(0, 2, 0));
                pool.Remove(record);
            });
            removeByTimeout.Start();
        }

        public static PoolObject GetRecordByKey(string key)
        {
            var res = pool.Where<PoolObject>((obj) => obj.Key == key).ToArray();
            if(res.Length == 0)
            {
                return null;
            }
            else
            {
                pool.Remove(res[0]);
                return res[0];
            }
        }

        public class PoolObject
        {
            public PoolObject(string username, string key, int status)
            {
                this.Key = key;
                this.Username = username;
                this.status = (AuthStatus)status;
            }
            public string Key { get; set; }
            public string Username { get; set; }
            public AuthStatus status { get; set; }
        }
    }
}