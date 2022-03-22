using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Text;

namespace CodeShellCore.Web.Moldster
{
   public class DefaultServerConfig : IServerConfig
    {
        public string BaseURL { get; set; }
        public string Domain { get; set; }
        public string Locale { get; set; }
        public string Env { get; set; }
        public string Version { get; set; }
        public string Hash { get; set; }
        public Dictionary<string, string> Urls { get; set; }

        public string DefaultLocale { get; private set; }

        public DefaultServerConfig()
        {
            DefaultLocale = Shell.DefaultCulture.TwoLetterISOLanguageName;
        }

        public string GetJson()
        {
            return this.ToJson();
        }
    }
}
