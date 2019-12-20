using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsAzure.Messaging;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using App5.Droid;
using Xamarin.Forms;
using System.Threading.Tasks;
using Application = Xamarin.Forms.Application;

[assembly: Dependency(typeof(Crop))]
namespace App5.Droid
{
    public class Crop : ICrop
    {
        private NotificationHub hub;
        public void CropImage()
        {
            
        }

        public void GetDevices()
        {
            throw new NotImplementedException();
        }

        public async Task<string> SendRegistrationToken()
        {
            // Register with Notification Hubs
            hub = new NotificationHub(MainActivity.Constants.NotificationHubName,
                MainActivity.Constants.ListenConnectionString, Android.App.Application.Context);

            var tags = new List<string>()
            {
                Xamarin.Essentials.DeviceInfo.Manufacturer
            };
            Application.Current.Properties["REGID"] = await Task.Run(() => hub.Register(Xamarin.Forms.Application.Current.Properties["token"].ToString(), tags.ToArray()).RegistrationId );
            return Application.Current.Properties["REGID"]?.ToString();
            //Log.Debug(TAG, $"Successful registration of ID {regID}");
        }
    }
}