using CodeShellCore.Data;
using CodeShellCore.Localization;

namespace CodeShellCore.Moldster.Pages
{
    [EntityName("PageParameter")]
    public class PageParameterDto : EntityDto<long>, IDetailObject<long>
    {
        public long PageCategoryParameterId { get; set; }
        public long PageId { get; set; }
        public long? LinkedPageId { get; set; }
        public string ParameterValue { get; set; }
        public bool UseDefault { get; set; }
        public string State { get; set; }
    }
}
