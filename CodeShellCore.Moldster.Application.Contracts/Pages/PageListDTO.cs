using CodeShellCore.Data;

namespace CodeShellCore.Moldster.Pages
{
    public class PageListDTO : EntityDto<long>
    {
        public string Name { get; set; }
        public string Layout { get; set; }
        public string ActionType { get; set; }
        public string ResourceName { get; set; }
        public long DomainId { get; set; }
        public string ViewPath { get; set; }
        public bool HasRoute { get; set; }
        public bool CanEmbed { get; set; }
        public string PageType { get; set; }
        public string RouteParameters { get; set; }
        public string TenantCode { get; set; }
        public long TenantId { get; set; }
        public string PageCategoryName { get; set; }
        public string BaseComponent { get; set; }
        public string Apps { get; set; }
        public string AppsString { get { return Apps?.Replace("\"", ""); } }
        public string DomainName { get; set; }
        public bool Persistant { get; set; }
        public int ReferencedBy { get; set; }
        public int References { get; set; }

    }
}
