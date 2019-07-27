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
                    TenantCode = e.TenantDomain.Tenant.Code,
                    TenantId = e.TenantDomain.TenantId,
                    Page = e,
                    BaseScript = e.PageCategory.ScriptPath,
                    ParentHasResource = e.PageCategory.ResourceId != null,
                    ResourceName = e.Resource.Name,
                    DomainName = e.TenantDomain.Domain.Name,
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
                    DomainName=p.TenantDomain.Domain.Name,
                    TenantCode = p.TenantDomain.Tenant.Code,
                    TenantId = p.TenantDomain.TenantId,
                    Page = p,
                    BaseScript = p.PageCategory.ScriptPath,
                    ResourceName = p.Resource.Name,
                    ActionName = p.ResourceAction == null ? (p.SpecialPermission ?? null) : p.ResourceAction.Name,
                    PageIdentifier = p.TenantDomain.Domain.Name + "__" + p.Name,
                };
            }
        }

        public string Registration { get { return "Registry.Register(\"" + Page.ViewPath + "\", " + ComponentName + ");\n"; } }

        public string GetImportString(bool removeDomain = false)
        {

            string path = Page.ViewPath;
            if (removeDomain)
            {
                Regex reg = new Regex("^" + DomainName + "/");
                path = reg.Replace(Page.ViewPath, "");
            }
            return $"import {{ {ComponentName} }} from \"./{path}\";\n";
        }


    }
}
