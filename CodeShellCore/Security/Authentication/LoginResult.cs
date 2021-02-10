using System;

namespace CodeShellCore.Security.Authentication
{
    public class LoginResult 
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IUser UserData { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiry { get; set; }

        public LoginResult(bool success, string message, IUser userData = null)
        {
            Success = success;
            Message = message;
            UserData = userData;
        }
    }
}
