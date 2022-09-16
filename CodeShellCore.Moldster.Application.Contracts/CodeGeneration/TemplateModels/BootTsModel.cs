using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.CodeGeneration.Models
{
    public class BootTsModel
    {
        public bool OtherTenants { get; set; }
        public string Code { get; set; }
        public string Style { get; set; }
        public string ModulePath { get; set; }
        public string Base => OtherTenants ? "'" + Code + "'" : "";
    }
}
