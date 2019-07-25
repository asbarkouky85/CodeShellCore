using CodeShellCore.Data.Lookups;
using Asga.Auth.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth.Lookups
{
    public class UsersLookups
    {
        public IEnumerable Roles { get; set; }
        public IEnumerable Domains { get; set; }
        public IEnumerable Apps { get; set; }
    }
}
