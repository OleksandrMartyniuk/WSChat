using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
/*using SQLite;
using System.Threading.Tasks;

namespace PortableNative.Droid.Controller
{
    public class Person_SqLite : IPersonDAO
    {
        private string path;
        private string tableName;

        public Person_SqLite(string path, string tablename)
        {
            this.path = path;
            this.tableName = tablename;
            var connection = new SQLiteConnection(path);
            var tables = connection.Query<object>(string.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}';", tableName));

            if (tables.Count == 0)
                connection.CreateTable<Person>();
        }

        private void insertUpdateData(Person data)
        {
            var db = new SQLiteAsyncConnection(path);
            Task<int> t = db.InsertAsync(data);
            t.Wait();
            int res = t.Result;
            if (res != 0)
            {
                db.UpdateAsync(data);
            }
        }

        public void Create(Person p)
        {
            insertUpdateData(p);
        }

        public void Delete(int id)
        {
            SQLiteConnection db = new SQLiteConnection(path);
            db.Query<Person>(string.Format("DELETE FROM {0} WHERE id={1};", tableName, id));
        }

        public List<Person> Read()
        {
            SQLiteConnection db = new SQLiteConnection(path);
            List<Person> persons = db.Query<Person>("SELECT * FROM " + tableName);
            return persons;
        }

        public Person Read(int id)
        {
            SQLiteConnection db = new SQLiteConnection(path);
            var p = db.Query<Person>(string.Format("SELECT FROM {0} WHERE id={1};", tableName, id));
            return p.Count != 0 ? p[0] : null;
        }

        public void Update(Person p)
        {
            insertUpdateData(p);
        }
    }
}*/