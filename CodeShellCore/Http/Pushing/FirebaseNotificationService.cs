using CodeShellCore.Helpers;
using CodeShellCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CodeShellCore.Http.Pushing
{
    public class PushNotificationService : HttpService
    {
        protected override string BaseUrl => Configuration.ServerUrl ?? "https://fcm.googleapis.com/fcm";
        private FirebaseConfig Configuration;

        public PushNotificationService()
        {
            Configuration = new FirebaseConfig();
            Headers["Authorization"] = "key=" + Configuration.ApiKey;
            if (Configuration.SenderId != null)
                Headers["Sender"] = "id=" + Configuration.SenderId;
        }

        public FirebasePushResult SendNotification(FirebaseRequest data)
        {
            return PostAs<FirebasePushResult>("send", data);
        }
        public string Lang { get; set; }


    }
}
