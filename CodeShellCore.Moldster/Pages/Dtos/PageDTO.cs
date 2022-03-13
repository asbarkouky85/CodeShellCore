using CodeShellCore.Data;
using CodeShellCore.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeShellCore.Moldster.Pages.Dtos
{
    [EntityName("Page")]
    public class PageDto : EntityDto<long>
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(300)]
        public string ViewPath { get; set; }
        public string Apps { get; set; }
        public string ViewParams { get; set; }
        public long? ResourceId { get; set; }
        [StringLength(200)]
        public string Layout { get; set; }
        [StringLength(50)]
        public string PrivilegeType { get; set; }
        public long? ResourceActionId { get; set; }
        [StringLength(50)]
        public string SpecialPermission { get; set; }
        public long? SourceCollectionId { get; set; }
        public long? PageCategoryId { get; set; }
        [StringLength(100)]
        public string RouteParameters { get; set; }
        public bool HasRoute { get; set; }
        public bool CanEmbed { get; set; }
        public int DefaultAccessibility { get; set; }
        public long DomainId { get; set; }
        public long TenantId { get; set; }
        public bool? IsHomePage { get; set; }
        public long? ParentId { get; set; }
    }
}
