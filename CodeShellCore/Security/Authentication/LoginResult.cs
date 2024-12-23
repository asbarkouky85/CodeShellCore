using CodeShellCore.Helpers;
using System;

namespace CodeShellCore.Security.Authentication
{
    public class LoginResult<T> : Result where T : class, IUser
    {
        private bool _success = false;
        public T UserData { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenExpiry { get; set; }
        public override bool IsSuccess => _success;

        public LoginResult(bool success, string message, T userData = null)
        {
            _success = success;
            Code = success ? 0 : 1;
            Message = message;
            UserData = userData;
        }

    }

    public class LoginResult : LoginResult<IUser>
    {
        public LoginResult(bool success, string message, IUser userData = null) : base(success, message, userData)
        {
        }

        public LoginResult<T> MapToLoginResult<T>() where T : class, IUser
        {
            var res = new LoginResult<T>(IsSuccess, Message, (T)UserData);
            res = MapToResult(res);
            res.RefreshToken = RefreshToken;
            res.TokenExpiry = TokenExpiry;
            res.Token = Token;
            return res;
        }
    }
}
