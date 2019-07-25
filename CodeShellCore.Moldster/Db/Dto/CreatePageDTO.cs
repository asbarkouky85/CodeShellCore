using CodeShellCore.Moldster.Db.Razor;
using System.Collections;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Db.Dto
{
    public class CreatePageDTO
    {
        public long Id { get; set; }
        public string Domain { get; set; }
        public string Roles { get; set; }
        public string TenantCode { get; set; }
        public string Name { get; set; }
        public string RouteParameters { get; set; }
        public string CategoryPath { get; set; }
        public long? CategoryId { get; set; }
        public string Resource { get; set; }
        public bool IsLookup { get; set; }
        public bool AppearsInNavigation { get; set; }
        public ViewParams ViewParams { get; set; }
        public string ActionType { get; set; }
        public string SpecialPermission { get; set; }
        public string Usage { get; set; }
        public string Layout { get; set; }
        public IEnumerable<string> Apps { get; set; }
        public int? DefaultAccessibility { get; set; }
        public long? CollectionId { get; set; }
    }
}
