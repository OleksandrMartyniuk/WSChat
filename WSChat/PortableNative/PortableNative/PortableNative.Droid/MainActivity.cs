using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PortableNative.Droid.Controller;

namespace PortableNative.Droid
{
    [Activity(Label = "PortableNative.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        Button create;
        Button read;
        Button update;
        Button delete;

        EditText id;
        EditText fname;
        EditText lname;
        EditText age;

        IPersonDAO dao;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            create = FindViewById<Button>(Resource.Id.btn_create);
            read = FindViewById<Button>(Resource.Id.btn_read);
            update = FindViewById<Button>(Resource.Id.btn_update);
            delete = FindViewById<Button>(Resource.Id.btn_delete);

            id = FindViewById<EditText>(Resource.Id.et_id);
            fname = FindViewById<EditText>(Resource.Id.et_FName);
            lname = FindViewById<EditText>(Resource.Id.et_LName);
            age = FindViewById<EditText>(Resource.Id.et_Age);

            dao = new PersonDAO();

            create.Click += (sender, args) => OnCreateClick();
            read.Click += (sender, args) => OnReadClick();
            update.Click += (sender, args) => OnUpdateClick();
            delete.Click += (sender, args) => OnDeleteClick();
        }

        private void OnDeleteClick()
        {
            dao.Delete(int.Parse(id.Text));
        }

        private void OnUpdateClick()
        {
            dao.Update(new Person(int.Parse(id.Text), fname.Text, lname.Text, int.Parse(age.Text)));
        }

        private void OnReadClick()
        {
            var p = dao.Read(int.Parse(id.Text));
            fname.Text = p.FirstName;
            lname.Text = p.LastName;
            age.Text = p.Age.ToString();
        }

        protected Person GetPerson()
        {
            return null;
        }

        protected void OnCreateClick()
        {
            dao.Create(new Person(int.Parse(id.Text), fname.Text, lname.Text, int.Parse(age.Text)));
            id.Text = "";
            fname.Text = "";
            lname.Text = "";
            age.Text = "";
        }

        
    }
}

