﻿using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Dto
{
    public class PageListDTO
    {
        public long Id { get; set; }
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
        public static Expression<Func<Page, PageListDTO>> Expression
        {
            get
            {
                return e => new PageListDTO
                {
                    Id = e.Id,
                    Layout = e.Layout,
                    Name = e.Name,
                    ActionType = e.ResourceActionId.HasValue ? e.ResourceAction.Name : e.SpecialPermission == null ? e.PrivilegeType : e.SpecialPermission,
                    ViewPath = e.ViewPath,
                    HasRoute = e.HasRoute,
                    CanEmbed = e.CanEmbed,
                    PageType = e.HasRoute && e.CanEmbed ? "Route,Embedded" : (e.HasRoute ? "Route" : "Embedded"),
                    RouteParameters = e.RouteParameters,
                    ResourceName = e.ResourceId.HasValue ? e.Resource.Name : "",
                    TenantId = e.TenantId,
                    DomainId = e.DomainId,
                    TenantCode = e.Tenant.Code,
                    PageCategoryName = e.PageCategory.Name,
                    BaseComponent = e.PageCategory.BaseComponent,
                    DomainName = e.Domain.Name,
                    Apps = e.Apps
                };
            }
        }
    }
}
