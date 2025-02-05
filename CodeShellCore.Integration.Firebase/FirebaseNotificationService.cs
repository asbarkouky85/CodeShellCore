using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeShellCore.Integration.Firebase.Flutter;
using CodeShellCore.Integration.Firebase.Results;
using Microsoft.Extensions.Options;

namespace CodeShellCore.Http.Pushing
{
    public class FirebaseNotificationService : HttpService, IFirebaseNotificationService
    {
        protected override string BaseUrl => Configuration.ServerUrl ?? "https://fcm.googleapis.com/fcm";
        private FirebaseOptions Configuration;

        public FirebaseNotificationService(IOptions<FirebaseOptions> options)
        {
            Configuration = options.Value;
            Headers["Authorization"] = "key=" + Configuration.ApiKey;
            if (Configuration.SenderId != null)
                Headers["Sender"] = "id=" + Configuration.SenderId;
        }

        public async Task<FirebasePushResult> SendNotificationToFlutter(FirebaseFlutterRequest request)
        {
            FirebasePushResult res = new FirebasePushResult();

            try
            {
                res = await PostAsAsync<FirebasePushResult>("send", request);
            }
            catch (Exception ex)
            {
                res.SetException(ex);
            }
            return res;

        }

        public async Task<FirebasePushResult> SendNotification(FirebaseRequest data)
        {
            FirebasePushResult res = new FirebasePushResult();

            try
            {
                res = await PostAsAsync<FirebasePushResult>("send", data);
            }
            catch (Exception ex)
            {
                res.SetException(ex);
            }
            return res;

        }

        public async Task<FirebasePushResult> SendNotification(FirebaseMessage message, string to = null, string[] topics = null, object data = null)
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
                res = await PostAsAsync<FirebasePushResult>("send", req);
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
