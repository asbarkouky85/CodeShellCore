using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CodeShellCore.Web.Features
{
    public class CustomizableFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly string controllersNameSpace;
        private readonly FeatureConfiguration config;

        public CustomizableFeatureProvider(string controllersNameSpace, FeatureConfiguration config)
        {
            this.controllersNameSpace = controllersNameSpace;
            this.config = config;
        }
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {

            if (config.All)
            {
                var cs = feature.Controllers.Where(d => d.Namespace.StartsWith(controllersNameSpace)).ToList();
                foreach (var item in cs)
                    feature.Controllers.Remove(item);
                return;
            }

            foreach (var item in config.Services)
            {
                var c = feature.Controllers.Where(d => d.Namespace.StartsWith(controllersNameSpace) && d.Name == item + "Controller").FirstOrDefault();
                if (c != null)
                    feature.Controllers.Remove(c);
            }

            foreach (var item in config.Domains)
            {
                var cs = feature.Controllers.Where(d => d.Namespace.StartsWith(controllersNameSpace + "." + item)).ToList();
                foreach (var c in cs)
                    feature.Controllers.Remove(c);
            }

            


        }
    }
}
