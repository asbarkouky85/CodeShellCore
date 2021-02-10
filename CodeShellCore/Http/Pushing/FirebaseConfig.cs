using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Http.Pushing
{
    public class FirebaseConfig
    {
        public string ApiKey { get { return Shell.GetConfigAs<string>("FirebaseConfig:ApiKey", true); } } //"AAAA1ZQDUcU:APA91bGfWlpJ86_FNgJz1B0VAXCYtGtw5CsYTWQKZrKwgzmxzWPdBbEdMZbFKd40ETtxUDcgXw7U8el3DrUfKN--ZI9V6zGVwuKMSF7HXlrD4zsc6i5x-BAgRvDKHFYVQyJbArcMwVAp",
        public string SenderId { get { return Shell.GetConfigAs<string>("FirebaseConfig:SenderId", false); } }// "917311279557",
        public string ServerUrl { get { return Shell.GetConfigAs<string>("FirebaseConfig:ServerUrl", false); } } // "https://fcm.googleapis.com/fcm/send",

    }
}
