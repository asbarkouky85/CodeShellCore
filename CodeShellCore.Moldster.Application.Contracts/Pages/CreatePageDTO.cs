using CodeShellCore.Data;
using CodeShellCore.Localization;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Pages
{
    [EntityName("Page")]
    public class CreatePageDTO : EntityDto<long>
    {
        public string TenantCode { get; set; }
        public string ComponentName { get; set; }
        public string ComponentPath { get; set; }
        public string ComponentDomain { get; set; }
        public string TemplatePath { get; set; }
        public string RouteParameters { get; set; }
        public long? CategoryId { get; set; }
        public string Resource { get; set; }
        public string NavigationGroup { get; set; }
        public string ActionType { get; set; }
        public string SpecialPermission { get; set; }
        public string Usage { get; set; }
        public string Layout { get; set; }
        public IEnumerable<string> Apps { get; set; }
        public string AppsString { get; set; }
        public string PrivilegeType { get; set; }

        public int? DefaultAccessibility { get; set; }
        public long? CollectionId { get; set; }
        public long? DomainId { get; set; }
        public long? ResourceId { get; set; }
        public long TenantId { get; set; }
    }
}
