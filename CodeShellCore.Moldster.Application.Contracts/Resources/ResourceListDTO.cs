using CodeShellCore.Data;
using CodeShellCore.Localization;
using System;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Resources
{
    [EntityName("Resource")]
    public class ResourceListDTO : EntityDto<long>
    {
        public string Name { get; set; }
        public string Domain { get; set; }

    }
}
