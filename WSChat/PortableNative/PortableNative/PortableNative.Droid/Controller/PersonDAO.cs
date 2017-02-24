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
    public class PersonDAO
    {
        public void AddPerson(Person p)
        {
            Realm realm = Realm.GetInstance();
            realm.Write(() =>
            {
                realm.Add(p);
            });
        }

        public void DeletePersonById(int Id)
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

        public Person GetPersonById(int id)
        {
            Realm realm = Realm.GetInstance();
            var p = realm.All<Person>().Where(d => d.id == id).First<Person>();
            return p;
        }

        public Person[] GetAllPersons()
        {
            Realm realm = Realm.GetInstance();
            var p = realm.All<Person>().ToArray<Person>();
            return p;
        }
    }
}