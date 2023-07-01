using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Features
{
    public class FeatureConfiguration : IFeatureConfiguration
    {
        private List<Type> types = new List<Type>();
        public string[] Services { get; private set; } = new string[0];
        public IReadOnlyList<Type> Controllers { get; private set; }
        public string[] Domains { get; private set; } = new string[0];
        public bool All { get; private set; } = false;

        public void BlockServices(string[] services)
        {
            Services = services;
        }

        public void BlockDomains(string[] domains)
        {
            Domains = domains;
        }

        public void BlockAll()
        {
            All = true;
        }

        public void BlockController<T>() where T : Controller
        {
            types.Add(typeof(T));
        }
    }
}
