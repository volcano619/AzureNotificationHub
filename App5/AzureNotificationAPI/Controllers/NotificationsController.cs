using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AzureNotificationAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(string pns, [FromBody]string message, string to_tag)
        {
            var user = HttpContext.User.Identity.Name;
            var userTag = new string[]
            {
                "username:" + to_tag,
                "from:" + user
            };

            Microsoft.Azure.NotificationHubs.NotificationOutcome outcome = null;
            var ret = HttpStatusCode.InternalServerError;

            switch (pns.ToLower())
            {
                case "wns":
                    // Windows 8.1 / Windows Phone 8.1
                    var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">" +
                                "From " + user + ": " + message + "</text></binding></visual></toast>";
                    outcome = await RegisterController.Notifications.Instance.Hub.SendWindowsNativeNotificationAsync(toast, userTag);
                    break;
                case "apns":
                    // iOS
                    var alert = "{\"aps\":{\"alert\":\"" + "From " + user + ": " + message + "\"}}";
                    outcome = await RegisterController.Notifications.Instance.Hub.SendAppleNativeNotificationAsync(alert, userTag);
                    break;
                case "fcm":
                    // Android
                    var notif = "{ \"data\" : {\"message\":\"" + "From " + user + ": " + message + "\"}}";
                    outcome = await RegisterController.Notifications.Instance.Hub.SendFcmNativeNotificationAsync(notif, userTag);
                    break;
            }

            if (outcome != null)
            {
                if (!((outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Abandoned) ||
                    (outcome.State == Microsoft.Azure.NotificationHubs.NotificationOutcomeState.Unknown)))
                {
                    ret = HttpStatusCode.OK;
                }
            }

            return StatusCode((int)ret);
        }
    }
}