using CodeShellCore.Data;
using CodeShellCore.Localization;

namespace CodeShellCore.Moldster.Pages
{
    [EntityName("PageRoute")]
    public class PageRouteDTO : EntityDto<long>, IDetailObject<long>
    {
        public long PageId { get; set; }
        public long? ListUrl { get; set; }
        public long? AddUrl { get; set; }
        public long? EditUrl { get; set; }
        public long? DetailsUrl { get; set; }
        public string ListUrlString { get; set; }
        public string AddUrlString { get; set; }
        public string EditUrlString { get; set; }
        public string DetailsUrlString { get; set; }
        public string State { get; set; }
    }
}
