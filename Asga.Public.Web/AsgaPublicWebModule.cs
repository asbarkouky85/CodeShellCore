using CodeShellCore.Web.Features;
using CodeShellCore.Web;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public
{
    public static class AsgaPublicWebModule
    {

        public static void ConfigureAsgaPublicWeb(this IMvcBuilder mvc,Action<IFeatureConfiguration> acc)
        {
            mvc.ConfigureAddedServices("Asga.Public.Web.Controllers", acc);
        }
    }
}
