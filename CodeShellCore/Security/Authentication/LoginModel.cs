﻿namespace CodeShellCore.Security.Authentication
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? RememberMe { get; set; }
        public string DeviceId { get; set; }
    }
}
