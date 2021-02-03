using CodeShellCore.Helpers;
using System;

namespace CodeShellCore.Security.Authentication
{
    public class LoginResult : Result
    {
        private bool _success = false;
        public IUser UserData { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiry { get; set; }

        public LoginResult(bool success, string message, IUser userData = null)
        {
            _success = success;
            Code = success ? 0 : 1;
            Message = message;
            UserData = userData;
        }
    }
}
