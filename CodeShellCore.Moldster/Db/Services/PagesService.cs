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
        private readonly DomainService domainService;

        public PagesService(IConfigUnit unit, DomainService domainService) : base(unit)
        {
            Unit = unit;
            this.domainService = domainService;
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
            string domainPath = dto.ComponentPath.GetBeforeLast("/");
            var domainResult = domainService.CreatePathAndGetId(domainPath);

            long domainId = (long)domainResult.Data["LastId"];
            long pageCategory = 0;

            if (dto.CategoryId == null)
                pageCategory = Unit.PageCategoryRepository.GetSingleValue(d => d.Id, e => e.ViewPath == dto.TemplatePath);
            else if (Unit.PageCategoryRepository.Exist(e => e.Id == dto.CategoryId.Value))
                pageCategory = dto.CategoryId.Value;

            if (pageCategory == 0)
                return new SubmitResult((int)HttpStatusCode.BadRequest, "No Template " + dto.TemplatePath + " or id " + dto.CategoryId);

            if (dto.Apps == null)
                dto.Apps = new string[0];

            Resource res = null;

            if (dto.Resource != null)
                res = Unit.ResourceRepository.GetResource(dto.Resource);

            if(res==null)
                return new SubmitResult((int)HttpStatusCode.BadRequest, "No such Resource " + dto.Resource);

            Tenant tenant = Unit.TenantRepository.FindSingle(d => d.Code == dto.TenantCode);
            if (tenant == null)
                return new SubmitResult(401, "Invalid tenant : " + dto.TenantCode);
            string folder = dto.TemplatePath.GetBeforeLast("/");
            CheckLayout(dto, pageCategory);

            Page p = new Page
            {
                Id = Utils.GenerateID(),
                Name = dto.ComponentPath.GetAfterLast("/"),
                AppearsInNavigation = dto.AppearsInNavigation,
                ViewParams = dto.ViewParams?.ToJson(),
                PageCategoryId = pageCategory,
                RouteParameters = dto.RouteParameters,
                ViewPath = dto.ComponentPath,
                SpecialPermission = dto.SpecialPermission,
                Layout = dto.Layout,
                SourceCollectionId = dto.CollectionId,
                DefaultAccessibility = dto.DefaultAccessibility ?? 2,
                DomainId=domainId
            };
            p.Apps = "";
            string sep = "";
            foreach (var s in dto.Apps)
            {
                p.Apps += sep + "\"" + s + "\"";
                sep = ", ";
            }

            if (Unit.PageRepository.Exist(d => d.Name == p.Name && d.DomainId == domainId && d.TenantId == tenant.Id))
                return new SubmitResult((int)HttpStatusCode.Conflict, "this page already exists " + dto.TenantCode + "/" + p.ViewPath);

            if (dto.Usage == null)
                return new SubmitResult((int)HttpStatusCode.BadRequest, "Usage cannot be null (R: routable,E: embeddable,RE: both)");

            p.HasRoute = dto.Usage.Contains("R");
            p.CanEmbed = dto.Usage.Contains("E");

            if (!p.HasRoute && !p.CanEmbed)
                return new SubmitResult((int)HttpStatusCode.BadRequest, "Invalid usage (R: routable,E: embeddable,RE: both)");

            string[] strs = new string[] { "view", "insert", "update", "delete", "details" };

            if (res != null)
            {
                if (strs.Contains(dto.ActionType.ToLower()))
                {
                    p.PrivilegeType = dto.ActionType;
                }
                else
                {
                    var ra = res.ResourceActions.Where(d => d.Name.ToLower() == dto.ActionType.ToLower() && d.TenantId == tenant.Id).FirstOrDefault();
                    if (ra == null)
                    {
                        ra = new ResourceAction
                        {
                            Id = Utils.GenerateID(),
                            Name = dto.ActionType,
                            TenantId = tenant.Id
                        };
                        res.ResourceActions.Add(ra);
                    }
                    ra.Pages.Add(p);
                }
            }

            tenant.Pages.Add(p);
            if (res != null)
                res.Pages.Add(p);

            var nn= Unit.SaveChanges();
            nn.Data["entity"] = p;
            return nn;
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
