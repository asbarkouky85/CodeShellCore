using CodeShellCore.Localization;
using System;

namespace CodeShellCore.Moldster.PageCategories
{
    [EntityName("PageCategory")]
    public class TemplateDTO
    {
        public string Name { get; set; }
        public string ViewPath { get; set; }
        public string BaseComponent { get; set; }
        public string ResourceName { get; set; }
        public long? ResourceId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
