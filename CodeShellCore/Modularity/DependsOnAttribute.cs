using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Modularity
{
    public class DependsOnAttribute : Attribute
    {
        public IEnumerable<Type> Modules => _modules;
        private readonly IEnumerable<Type> _modules;
        public DependsOnAttribute(params Type[] modules)
        {
            _modules = modules;
        }
    }
}
