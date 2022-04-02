using CodeShellCore.Data;

namespace CodeShellCore.Moldster.PageCategories
{

    public class PageCategoryParameterDto : EntityDto<long>, IEditable<long>
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public string DefaultValue { get; set; }
        public string State { get; set; }

    }
}
