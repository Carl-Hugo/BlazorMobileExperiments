using Android.App;
using Android.Widget;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Based on: https://stackoverflow.com/a/44126899/8339553
[assembly: Xamarin.Forms.Dependency(typeof(App.Droid.MessageAndroid))]
namespace App.Droid
{
    public class MessageAndroid : IAlertManager
    {
        public void Show(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}
