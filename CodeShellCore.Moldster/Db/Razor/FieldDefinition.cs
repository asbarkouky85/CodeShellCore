using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Db.Razor
{
    public class FieldDefinition
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public static Expression<Func<CustomField, FieldDefinition>> Expression => e => new FieldDefinition
        {
            Name = e.Name,
            Type = e.Type
        };
    }
}
