using CodeShellCore.Web;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Asga.Common
{
    public class AsgaShell : WebShell
    {
        public AsgaShell(IConfiguration config) : base(config)
        {
        }

        public static Dictionary<long, string> ConnectionStrings { get; internal set; }
        public static object CurrentTenant { get; set; }

        protected override bool useLocalization => throw new NotImplementedException();

        protected override CultureInfo defaultCulture => throw new NotImplementedException();

        public static string GetTenantConnectionString(long tenantId) { return ""; }

        public static string GetTenantCode(long ten)
        {
            return "";
        }
    }
}
