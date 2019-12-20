
using Foundation;
using UIKit;
using WindowsAzure.Messaging;
using UserNotifications;

namespace App5.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private SBNotificationHub Hub { get; set; }
        public const string ListenConnectionString = "Endpoint=sb://azurenotificationhubname.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=s63GLTNyi1upwcrGrIbwZb4gc87rQorT1/08laOz+9M=";
        public const string NotificationHubName = "AzureNotificationHubTest";
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Stormlion.ImageCropper.iOS.Platform.Init();
            global::Xamarin.Forms.Forms.Init();
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
                    (granted, error) =>
                    {
                        if (granted)
                            InvokeOnMainThread(UIApplication.SharedApplication.RegisterForRemoteNotifications);
                    });
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                    new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
            }

            LoadApplication(new App());

            //  return true;

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Xamarin.Forms.Application.Current.Properties["token"] = deviceToken;
            //Hub = new SBNotificationHub(ListenConnectionString, NotificationHubName);

            //Hub.UnregisterAll(deviceToken, async (error) => {
            //    if (error != null)
            //    {
            //        System.Diagnostics.Debug.WriteLine("Error calling Unregister: {0}", error.ToString());
            //        return;
            //    }

            //    NSSet tags = null; // create tags if you want
            //    tags = new NSSet(Xamarin.Essentials.DeviceInfo.Manufacturer);
            //    await Hub.RegisterNativeAsync(deviceToken, tags);
            //});
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            ProcessNotification(userInfo, false);
        }

        private void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
        {
            // Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
            if (null != options && options.ContainsKey(new NSString("aps")))
            {
                //Get the aps dictionary
                NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

                string alert = string.Empty;

                //Extract the alert text
                // NOTE: If you're using the simple alert by just specifying
                // "  aps:{alert:"alert msg here"}  ", this will work fine.
                // But if you're using a complex alert with Localization keys, etc.,
                // your "alert" object from the aps dictionary will be another NSDictionary.
                // Basically the JSON gets dumped right into a NSDictionary,
                // so keep that in mind.
                if (aps != null && aps.ContainsKey(new NSString("alert")))
                    alert = ((NSString) aps[new NSString("alert")]).ToString();

                //If this came from the ReceivedRemoteNotification while the app was running,
                // we of course need to manually process things like the sound, badge, and alert.
                if (!fromFinishedLaunching)
                {
                    //Manually show an alert
                    if (!string.IsNullOrEmpty(alert))
                    {
                        UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
                        avAlert.Show();
                    }
                }
            }
        }

    }
}
