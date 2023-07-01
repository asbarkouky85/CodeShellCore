using System;

namespace CodeShellCore.Net
{
    public class UploadConfig
    {
        public string Type { get; set; }
        public string Server { get; set; }
        public string ServerUrl { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string PathOnServer { get; set; }
        public bool Active { get; set; }

        

        public static UploadConfig GetFromConfig()
        {
            return new UploadConfig
            {
                Type = Shell.GetConfigAs<string>("UploadConfig:Type"),
                Server = Shell.GetConfigAs<string>("UploadConfig:Server"),
                UserName = Shell.GetConfigAs<string>("UploadConfig:UserName"),
                Password = Shell.GetConfigAs<string>("UploadConfig:Password"),
                Active = Shell.GetConfigAs<bool>("UploadConfig:Active"),
                ServerUrl = Shell.GetConfigAs<string>("UploadConfig:ServerUrl",false),
                PathOnServer= Shell.GetConfigAs<string>("UploadConfig:PathOnServer")
            };

        }
    }
}
