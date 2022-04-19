using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.CodeGeneration.Models
{
    public class ServerConfigTsModel
    {
        public string ApiUrl { get; set; }
        public string Production { get; set; }
        public string DefaultLocale { get; set; }
    }
}
