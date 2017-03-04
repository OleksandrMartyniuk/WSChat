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

using Realms;

namespace PortableNative.Droid.Controller
{
    public class PersonDAO: IPersonDAO
    {
        public void Create(Person p)
        {
            Realm realm = Realm.GetInstance();
            realm.Write(() =>
            {
                realm.Add(p);
            });
        }

        public void Delete(int Id)
        {
            Realm realm = Realm.GetInstance();
            realm.Write(() =>
            {
                var p = realm.All<Person>().Where(d => d.id == Id).First<Person>();
                realm.Remove(p);
            });
        }
        public void DeletePersonByPosition(int position)
        {
            Realm realm = Realm.GetInstance();
            realm.Write(() =>
            {
                Person[] p = realm.All<Person>().ToArray<Person>();
                var pers = p[position];
                realm.Remove(pers);
            });
        }

        public Person Read(int id)
        {
            Realm realm = Realm.GetInstance();
            var p = realm.All<Person>().Where(d => d.id == id).First<Person>();
            return p;
        }

        public List<Person> Read()
        {
            Realm realm = Realm.GetInstance();
            var p = realm.All<Person>().ToList<Person>(); //ToArray<Person>();
            return p;
        }

        public void Update(Person pers)
        {
            Realm realm = Realm.GetInstance();
            realm.Write(() =>
            {
                var p = realm.All<Person>().Where(d => d.id == pers.id).First<Person>();
                p.FirstName = pers.FirstName;
                p.LastName = pers.LastName;
                p.Age = pers.Age;
            });
        }
    }
}