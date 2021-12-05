using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Text.Localization
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class EntityNameAttribute : Attribute
    {
        public string EntityName { get; private set; }
        public EntityNameAttribute(string entityKey)
        {
            EntityName = entityKey;
        }
    }
}
