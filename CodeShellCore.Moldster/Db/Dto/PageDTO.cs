using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using CodeShellCore.Text;

namespace CodeShellCore.Moldster.Db.Dto
{
    public class PageDTO
    {
        public long TenantId { get; set; }
        public string TenantCode { get; set; }
        public Page Page { get; set; }
        public string BaseScript { get; set; }
        public string PageIdentifier { get; set; }
        public string ActionName { get; set; }
        public string ResourceName { get; set; }
        public string DomainName { get; set; }
        public bool ParentHasResource { get; set; }
        public string CollectionId { get; set; }

        public string ComponentName { get { return Page.ViewPath.GetAfterLast("/"); } }

        public static Expression<Func<Page, PageDTO>> ExpressionForRendering
        {
            get
            {
                return e => new PageDTO
                {
                    TenantCode = e.Tenant.Code,
                    TenantId = e.TenantId,
                    Page = e,
                    BaseScript = e.PageCategory.ScriptPath,
                    ParentHasResource = e.PageCategory.ResourceId != null,
                    ResourceName = e.Resource.Name,
                    DomainName = e.Domain.Name,
                    CollectionId = e.SourceCollection == null ? null : e.SourceCollection.Name
                };
            }
        }

        public static Expression<Func<Page, PageDTO>> ExpressionForRouting
        {
            get
            {
                return p => new PageDTO
                {
                    DomainName = p.Domain.Name,
                    TenantCode = p.Tenant.Code,
                    TenantId = p.TenantId,
                    Page = p,
                    BaseScript = p.PageCategory.ScriptPath,
                    ResourceName = p.Resource.Name,
                    ActionName = p.ResourceAction == null ? (p.SpecialPermission ?? null) : p.ResourceAction.Name,
                    PageIdentifier = p.Domain.Name + "__" + p.Name,
                };
            }
        }

        public string Registration { get { return "Registry.Register(\"" + Page.ViewPath + "\", " + ComponentName + ");\n"; } }

        public string GetImportString(bool sameFolder = false)
        {
            string path = !sameFolder ? "../" + Page.ViewPath : "./" + ComponentName;
            return $"import {{ {ComponentName} }} from \"{path}\";\n";
        }


    }
}
