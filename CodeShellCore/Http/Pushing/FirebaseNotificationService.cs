using System;
using System.Collections.Generic;

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
            FirebasePushResult res = new FirebasePushResult();

            try
            {
                res = PostAs<FirebasePushResult>("send", data);
            }
            catch (Exception ex)
            {
                res.SetException(ex);
            }
            return res;

        }

        public FirebasePushResult SendNotification(FirebaseMessage message, string to = null, string[] topics = null, object data = null)
        {
            var req = new FirebaseRequest
            {
                notification = message,
                data = data,
                to = to
            };

            if (topics != null)
            {
                List<string> conds = new List<string>();
                foreach (var c in topics)
                {
                    conds.Add("'" + c + "' in topics");
                }
                req.condition = string.Join("||", conds);
            }

            FirebasePushResult res = new FirebasePushResult();

            try
            {
                res = PostAs<FirebasePushResult>("send", req);
            }
            catch (Exception ex)
            {
                res.success = 0;
                res.failure = 1;
                res.SetException(ex);
            }
            return res;
        }



        public string Lang { get; set; }


    }
}
