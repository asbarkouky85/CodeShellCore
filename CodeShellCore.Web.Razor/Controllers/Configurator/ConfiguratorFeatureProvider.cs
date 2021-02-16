using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Web.Razor.Controllers.Configurator
{
    public class ConfiguratorFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var lst = feature.Controllers.Where(d => d.Namespace != "CodeShellCore.Web.Razor.Configurator" && d.Namespace.StartsWith("CodeShellCore.Web.Razor")).ToList();
            foreach (var item in lst)
            {
                feature.Controllers.Remove(item);
            }
        }
    }
}
