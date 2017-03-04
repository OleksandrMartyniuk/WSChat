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

namespace PortableNative.Droid.Controller
{
    public interface IPersonDAO
    {
        void Create(Person p);
        List<Person> Read();
        Person Read(int id);
        void Update(Person p);
        void Delete(int id);
    }
}