using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Web.Features
{
    public class ControllerBlockerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly ControllerBlockerOptions opts;

        public ControllerBlockerFeatureProvider(ControllerBlockerOptions opts)
        {
            this.opts = opts;
        }
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var item in opts.BlockedControllers)
            {
                var cs = feature.Controllers.Where(d => d.FullName == item.FullName).ToList();
                foreach (var c in cs)
                    feature.Controllers.Remove(c);
            }
        }
    }
}
