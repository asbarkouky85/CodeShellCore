using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Localization
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class EntityNameAttribute : Attribute
    {
        public string EntityName { get; private set; }
        public EntityNameAttribute(string entityName)
        {
            EntityName = entityName;
        }

    }
}
