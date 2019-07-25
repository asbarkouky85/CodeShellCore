using System;
using System.Linq;
using System.Net;
using System.IO;

using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Services.Http;
using CodeShellCore.Text;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Moldster.Db.Razor;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Db.Data;

namespace CodeShellCore.Moldster.Db.Services
{
    public class PagesService : EntityService<Page>
    {
        IConfigUnit Unit;
        public PagesService(IConfigUnit unit) : base(unit)
        {
            Unit = unit;
        }

        

        private void CheckLayout(CreatePageDTO dto, long pageCategory)
        {
            if (dto.Layout == null)
            {
                string baseType = Unit.PageCategoryRepository.GetSingleValue(d => d.BaseComponent, d => d.Id == pageCategory);
                if (baseType == "Edit")
                {
                    dto.Layout = "Layout/EditLayout";
                }
                else if (baseType == "List")
                {
                    dto.Layout = "Layout/ListLayout";
                }
                else if (baseType == "Tree")
                {
                    dto.Layout = "Layout/DefaultLayout";
                }
                else if (baseType == "Select")
                {
                    dto.Layout = "Layout/SelectLayout";
                }
                else
                {
                    throw new CodeShellHttpException(HttpStatusCode.BadRequest, "Layout should be filled if category's BaseComponent is not default");
                }
            }
            else
            {
                string path = Path.Combine(CodeShellCore.Shell.AppRootPath, "Views", dto.Layout + ".cshtml");
                if (!File.Exists(path))
                {
                    throw new CodeShellHttpException(HttpStatusCode.NotFound, "No such layout file '" + path + "'");
                }
            }
        }

        public SubmitResult SetViewParams(long id, ViewParams @params)
        {
            var p = Repository.FindSingle(id);
            if (p == null)
                throw new CodeShellHttpException(HttpStatusCode.NotFound, " id not found!!");

            p.ViewParams = @params.ToJson();
            return Unit.SaveChanges();
        }

        public SubmitResult Create(CreatePageDTO dto)
        {
            long domainId = Unit.DomainRepository.GetSingleValue(d => d.Id, d => d.Name == dto.Domain);
            if (domainId == 0)
                throw new CodeShellHttpException(HttpStatusCode.BadRequest, "Unknown domain " + dto.Name);

            long pageCategory = 0;

            if (dto.CategoryId == null)
                pageCategory = Unit.PageCategoryRepository.GetSingleValue(d => d.Id, e => e.ViewPath == dto.CategoryPath);
            else if (Unit.PageCategoryRepository.Exist(e => e.Id == dto.CategoryId.Value))
                pageCategory = dto.CategoryId.Value;

            if (pageCategory == 0)
                throw new CodeShellHttpException(HttpStatusCode.BadRequest, "No Template " + dto.CategoryPath + " or id " + dto.CategoryId);

            if (dto.Apps == null || !dto.Apps.Any())
                throw new CodeShellHttpException(HttpStatusCode.BadRequest, "Page must be associated with at least one app");

            Resource res = null;

            if (dto.Resource != null)
                res = Unit.ResourceRepository.GetResource(domainId, dto.Resource);

            TenantDomain dom = Unit.TenantDomainRepository.GetTenantDomain(domainId, dto.TenantCode);

            string folder = dto.CategoryPath.GetBeforeLast("/");
            CheckLayout(dto, pageCategory);

            Page p = new Page
            {
                Id = Utils.GenerateID(),
                Name = dto.Name,
                AppearsInNavigation = dto.AppearsInNavigation,
                ViewParams = dto.ViewParams?.ToJson(),
                PageCategoryId = pageCategory,
                RouteParameters = dto.RouteParameters,
                ViewPath = folder + "/" + dto.Name,
                SpecialPermission = dto.SpecialPermission,
                Layout = dto.Layout,
                SourceCollectionId = dto.CollectionId,
                DefaultAccessibility = dto.DefaultAccessibility ?? 2
            };
            p.Apps = "";
            string sep = "";
            foreach (var s in dto.Apps)
            {
                p.Apps += sep + "\"" + s + "\"";
                sep = ", ";
            }

            if (Unit.PageRepository.Exist(d => d.ViewPath == p.ViewPath && d.TenantDomainId == dom.Id))
                throw new CodeShellHttpException(HttpStatusCode.Conflict, "this page already exists " + dto.TenantCode + "/" + p.ViewPath);

            if (dto.Usage == null)
                throw new CodeShellHttpException(HttpStatusCode.BadRequest, "Usage cannot be null (R: routable,E: embeddable,RE: both)");

            p.HasRoute = dto.Usage.Contains("R");
            p.CanEmbed = dto.Usage.Contains("E");

            if (!p.HasRoute && !p.CanEmbed)
                throw new CodeShellHttpException(HttpStatusCode.BadRequest, "Invalid usage (R: routable,E: embeddable,RE: both)");

            string[] strs = new string[] { "view", "insert", "update", "delete", "details" };

            if (res != null)
            {
                if (strs.Contains(dto.ActionType.ToLower()))
                {
                    p.PrivilegeType = dto.ActionType;
                }
                else
                {
                    var ra = res.ResourceActions.Where(d => d.Name.ToLower() == dto.ActionType.ToLower() && d.TenantId == dom.TenantId).FirstOrDefault();
                    if (ra == null)
                    {
                        ra = new ResourceAction
                        {
                            Id = Utils.GenerateID(),
                            Name = dto.ActionType,
                            TenantId = dom.TenantId
                        };
                        res.ResourceActions.Add(ra);
                    }
                    ra.Pages.Add(p);
                }
            }

            dom.Pages.Add(p);
            if (res != null)
                res.Pages.Add(p);

            return Unit.SaveChanges();
        }

        public SubmitResult Update(CreatePageDTO dto)
        {
            Page p = Repository.FindSingle(dto.Id);
            CheckLayout(dto, p.PageCategoryId.Value);

            p.Id = dto.Id;
            p.Name = dto.Name;
            p.AppearsInNavigation = dto.AppearsInNavigation;
            p.ViewParams = dto.ViewParams?.ToJson();
            p.PrivilegeType = dto.ActionType;
            p.RouteParameters = dto.RouteParameters;
            p.ViewPath = p.ViewPath.GetBeforeLast("/") + "/" + dto.Name;
            p.SpecialPermission = p.SpecialPermission;

            Repository.Update(p);
            return Unit.SaveChanges();
        }
        public override SubmitResult Create(Page obj)
        {

            if (obj.ResourceId == 0)
            {
                long? res = Unit.PageCategoryRepository.GetValue(obj.PageCategoryId, d => d.ResourceId);
                if (res == null)
                    throw new CodeShellHttpException(HttpStatusCode.BadRequest, "ResourceId Required");
                obj.ResourceId = res.Value;
            }
            if (obj.ViewPath == null)
                obj.ViewPath = Unit.PageCategoryRepository.GetValue(obj.PageCategoryId, d => d.ViewPath);

            if (obj.Name == null)
                obj.Name = obj.ViewPath.GetAfterLast("/");


            return base.Create(obj);
        }
    }
}
