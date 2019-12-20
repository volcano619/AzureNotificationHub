using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsAzure.Messaging;
using App5.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Crop))]
namespace App5.iOS
{
    public class Crop : ICrop
    {
        public const string ListenConnectionString = "Endpoint=sb://azurenotificationhubname.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=s63GLTNyi1upwcrGrIbwZb4gc87rQorT1/08laOz+9M=";
        public const string NotificationHubName = "AzureNotificationHubTest";
        private SBNotificationHub Hub { get; set; }
        public void CropImage()
        {

        }

        public void GetDevices()
        {

        }

        public void SendRegistrationToken()
        {
            Hub = new SBNotificationHub(ListenConnectionString, NotificationHubName);
            if (Xamarin.Forms.Application.Current.Properties["token"] is NSData deviceToken)
            {
                Hub.UnregisterAll(deviceToken, async (error) =>
                {
                    if (error != null)
                    {
                        System.Diagnostics.Debug.WriteLine("Error calling Unregister: {0}", error.ToString());
                        return;
                    }

                    NSSet tags = null; // create tags if you want
                    tags = new NSSet(Xamarin.Essentials.DeviceInfo.Manufacturer);
                    await Hub.RegisterNativeAsync(deviceToken, tags);
                });

            }
        }
    }
}