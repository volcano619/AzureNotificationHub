using System.Collections.Generic;
using System.Linq;
using Android.Util;
using Firebase.Messaging;
using Android.Support.V4.App;
using WindowsAzure.Messaging;
using Android.App;
using Android.Content;
using Xamarin.Forms;

namespace App5.Droid
{
    [Service]
    [IntentFilter(new[] {"com.google.firebase.MESSAGING_EVENT"})]
    [IntentFilter(new[] {"com.google.firebase.INSTANCE_ID_EVENT"})]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        NotificationHub hub;
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            if (message.GetNotification() != null)
            {
                //These is how most messages will be received
                Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
                SendNotification(message.GetNotification().Body);
            }
            else
            {
                //Only used for debugging payloads sent from the Azure portal
                SendNotification(message.Data.Values.First());
            }
        }

        public override void OnNewToken(string token)
        {
            Log.Debug(TAG, "FCM token: " + token);
            Xamarin.Forms.Application.Current.Properties["token"] = token;
            //SendRegistrationToServer(token);
        }

        void SendRegistrationToServer(string token)
        {
            // Register with Notification Hubs
            hub = new NotificationHub(MainActivity.Constants.NotificationHubName,
                MainActivity.Constants.ListenConnectionString, this);

            var tags = new List<string>()
            {
                Xamarin.Essentials.DeviceInfo.Manufacturer
            };
            var regID = hub.Register(token, tags.ToArray()).RegistrationId;

            Log.Debug(TAG, $"Successful registration of ID {regID}");
        }

        void SendNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID);

            notificationBuilder.SetContentTitle("FCM Message")
                .SetSmallIcon(Resource.Drawable.ic_launcher)
                .SetContentText(messageBody)
                .SetAutoCancel(true)
                .SetShowWhen(false)
                .SetVibrate(new long[]{2})
                .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);

            notificationManager.Notify(0, notificationBuilder.Build());
        }

    }
}