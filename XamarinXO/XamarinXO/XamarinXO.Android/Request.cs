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


namespace XamarinXO.Droid
{
   public  class Request 
    {

       public string Module;
       public string Cmd;
       public object Args;

        public Request(string Module, string Cmd, object Args)
        {
            this.Module = Module;
            this.Cmd = Cmd;
            this.Args = Args;
        }

    }
}