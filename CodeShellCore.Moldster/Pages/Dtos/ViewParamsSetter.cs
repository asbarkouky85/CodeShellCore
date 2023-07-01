using CodeShellCore.Moldster.Razor;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Pages.Dtos
{
    public class ViewParamsSetter
    {
        public long TenantId { get; set; }
        public string PageName { get; set; }
        public string TemplateName { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public IEnumerable<FieldDefinition> Fields { get; set; }
    }
}
