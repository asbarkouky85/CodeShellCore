using CodeShellCore.Data.Lookups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth.Dto
{
    public class ResourceWithActionsDTO : Named<long>
    {
        public IEnumerable<Named<long>> Actions { get; set; }
        public IEnumerable<Named<long>> Collections { get; set; }
    }
}
