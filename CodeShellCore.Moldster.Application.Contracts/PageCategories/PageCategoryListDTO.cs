using CodeShellCore.Data;
using CodeShellCore.Localization;

namespace CodeShellCore.Moldster.PageCategories
{
    [EntityName("PageCategory")]
    public class PageCategoryListDTO : EntityDto<long>
    {
        public string Name { get; set; }
        public string BaseComponent { get; set; }
        public string ResourceName { get; set; }
        public string DomainName { get; set; }
        public string Layout { get; set; }
        public string ViewPath { get; set; }
        public long? ResourceId { get; set; }

        public long? DomainId { get; set; }


    }
}
