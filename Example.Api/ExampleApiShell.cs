using CodeShellCore.Web;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Api
{
    public class ExampleApiShell : WebShell
    {
        protected override bool useLocalization => true;

        protected override CultureInfo defaultCulture => new CultureInfo("en");

        protected override string ApiPrefix => "app";

        public ExampleApiShell(IConfiguration config) : base(config)
        {
        }

        
    }
}
