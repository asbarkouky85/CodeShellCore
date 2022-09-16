using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Moldster.Tenants;
using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;using System.Linq;

namespace CodeShellCore.Moldster.Pages{    public partial class Page : MoldsterModelBase, IMoldsterModel    {        public Page()        {            CustomFields = new HashSet<CustomField>();            NavigationPages = new HashSet<NavigationPage>();            PageControls = new HashSet<PageControl>();            PageParameters = new HashSet<PageParameter>();            PageRoutes = new HashSet<PageRoute>();        }        public long Id { get; set; }        [StringLength(50)]        public string Name { get; set; }        [StringLength(300)]        public string ViewPath { get; set; }        public string Apps { get; set; }        [Column(TypeName = "ntext")]        public string ViewParams { get; set; }        public long? ResourceId { get; set; }        [StringLength(200)]        public string Layout { get; set; }        [StringLength(50)]        public string PrivilegeType { get; set; }        public long? ResourceActionId { get; set; }        [StringLength(50)]        public string SpecialPermission { get; set; }        public long? SourceCollectionId { get; set; }        public long? PageCategoryId { get; set; }        [StringLength(100)]        public string RouteParameters { get; set; }        public bool HasRoute { get; set; }        public bool CanEmbed { get; set; }        [Column(TypeName = "datetime")]        public DateTime? CreatedOn { get; set; }        [Column(TypeName = "datetime")]        public DateTime? UpdatedOn { get; set; }        public long? CreatedBy { get; set; }        public long? UpdatedBy { get; set; }        public int DefaultAccessibility { get; set; }        public long DomainId { get; set; }        public long TenantId { get; set; }        public bool? IsHomePage { get; set; }        public long? ParentId { get; set; }        [ForeignKey("DomainId")]        [InverseProperty("Pages")]
        [System.Runtime.Serialization.IgnoreDataMember]
        public Domain Domain { get; set; }        [ForeignKey("PageCategoryId")]        [InverseProperty("Pages")]
        [System.Runtime.Serialization.IgnoreDataMember]
        public PageCategory PageCategory { get; set; }        [ForeignKey("ResourceId")]        [InverseProperty("Pages")]
        [System.Runtime.Serialization.IgnoreDataMember]
        public Resource Resource { get; set; }        [ForeignKey("ResourceActionId")]        [InverseProperty("Pages")]
        [System.Runtime.Serialization.IgnoreDataMember]
        public ResourceAction ResourceAction { get; set; }        [ForeignKey("SourceCollectionId")]        [InverseProperty("Pages")]
        [System.Runtime.Serialization.IgnoreDataMember]
        public ResourceCollection SourceCollection { get; set; }        [ForeignKey("TenantId")]        [InverseProperty("Pages")]
        [System.Runtime.Serialization.IgnoreDataMember]
        public Tenant Tenant { get; set; }        [InverseProperty("Page")]        public ICollection<CustomField> CustomFields { get; set; }        [InverseProperty("Page")]        public ICollection<NavigationPage> NavigationPages { get; set; }        [InverseProperty("Page")]        public ICollection<PageControl> PageControls { get; set; }        [InverseProperty("Page")]        public ICollection<PageParameter> PageParameters { get; set; }        [InverseProperty("Page")]        public ICollection<PageRoute> PageRoutes { get; set; }

        public void SetRoute(PageRoute pageRoute)
        {
            var route = PageRoutes.FirstOrDefault();
            if (route == null)
            {
                route = new PageRoute();
                PageRoutes.Add(route);
            }
            route.AddUrl = pageRoute.AddUrl;
            route.ListUrl = pageRoute.ListUrl;
            route.EditUrl = pageRoute.EditUrl;
            route.DetailsUrl = pageRoute.DetailsUrl;
        }

        public List<string> GetAppsList()
        {
            return Apps != null ? Apps.Replace("\"", "").Replace(" ", "").Split(',').ToList() : null;
        }
    }}