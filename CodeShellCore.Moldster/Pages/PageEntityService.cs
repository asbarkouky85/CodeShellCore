using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Services;
using CodeShellCore.Extensions.Data;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Text;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CodeShellCore.Moldster.Pages
{
    public class PageEntityService : DtoEntityService<Page, long, LoadOptions, PageListDTO, CreatePageDTO, CreatePageDTO, CreatePageDTO>, IPageEntityService
    {
        private readonly IConfigUnit Unit;

        IDomainScriptGenerationService domainTs => Unit.ServiceProvider.GetService<IDomainScriptGenerationService>();
        IPageHtmlGenerationService _html => Unit.ServiceProvider.GetService<IPageHtmlGenerationService>();
        IPageScriptGenerationService pageTs => Unit.ServiceProvider.GetService<IPageScriptGenerationService>();
        IMoldsterLookupService Lookups => Unit.ServiceProvider.GetService<IMoldsterLookupService>();
        public override bool ProjectGetSingle => false;

        public PageEntityService(IConfigUnit unit) : base(unit)
        {
            this.Unit = unit;
        }

        public LoadResult<PageListDTO> FindPages(LoadOptions opts, FindPageRequest request)
        {
            return Unit.PageRepository.FindUsing(request, opts);

        }

        public override Dictionary<string, IEnumerable<Named<object>>> GetEditLookups(Dictionary<string, string> dto)
        {
            return Lookups.PageEdit(dto);
        }

        public override SubmitResult<CreatePageDTO> Put(CreatePageDTO dto)
        {
            bool pathChanged = false;
            string oldPath = null;
            var page = Unit.PageRepository.FindSingle(dto.Id);
            page.AppendProperties(dto, true, new[] { "Resource", "Apps", "ViewParams" });

            page.Apps = GetApps(dto.Apps);
            if (IsDefaultAction(dto.ActionType))
                page.PrivilegeType = dto.ActionType;
            var dom = Unit.DomainRepository.GetOrCreatePath(dto.ComponentDomain);
            page.Domain = dom;
            page.SourceCollectionId = dto.CollectionId;
            page.HasRoute = dto.Usage.Contains("R");
            page.CanEmbed = dto.Usage.Contains("E");
            if (page.ViewPath != dto.ComponentPath)
            {
                oldPath = page.ViewPath;
                page.ViewPath = dto.ComponentPath;
                pathChanged = true;
            }

            page.Name = dto.ComponentName;
            Repository.Update(page);
            var res = Unit.SaveChanges().ToSubmitResult<CreatePageDTO>();

            if (res.IsSuccess && pathChanged)
            {
                res.Data["ReferenceUpdateResult"] = UpdatePageRefeneces(dto.Id, dto.TenantId);

                var moveRequest = new MovePageRequest
                {
                    DomainId = page.DomainId,
                    FromPath = oldPath,
                    ToPath = page.ViewPath,
                    PageId = page.Id,
                    TenantCode = dto.TenantCode
                };

                _html.MoveHtmlTemplate(moveRequest);
                pageTs.MoveScript(moveRequest);
                domainTs.GenerateDomainModule(moveRequest.TenantCode, moveRequest.FromPath.GetBeforeLast("/"));
                domainTs.GenerateDomainModule(moveRequest.TenantCode, moveRequest.ToPath.GetBeforeLast("/"));
                domainTs.GenerateRoutes(moveRequest.TenantCode);
            }
            return res;
        }

        public SubmitResult UpdatePageRefeneces(long pageId, long tenantId)
        {
            IEnumerable<Page> ps = Unit.PageRepository.GetReferencing(pageId, tenantId);
            foreach (var p in ps)
            {
                PageParameterForJson[] pars = Unit.PageParameterRepository.FindForJsonByPage(p.Id).ToArray();
                PageRouteDTO r = Unit.PageRouteRepository.FindByPage(p.Id);
                var fs = Unit.CustomFieldRepository.FindAs(
                    e => new FieldDefinition { Name = e.Name, Type = e.Type },
                    d => d.PageId == p.Id).ToArray();
                fs = fs.Any() ? fs : null;
                Unit.PageRepository.UpdatePageViewParamsJson(p, pars, r, fs);
            }
            return Unit.SaveChanges();
        }

        public override DeleteResult Delete(long id)
        {
            var page = Unit.PageRepository.FindSingleAs(d => new { d.ViewPath, d.Tenant.Code }, d => d.Id.Equals(id));
            Unit.PageRepository.DeleteById(id);
            var result = Unit.SaveChanges().MapToResult<DeleteResult>();
            if (result.IsSuccess)
            {
                var path = new MovePageRequest { TenantCode = page.Code, FromPath = page.ViewPath };

                _html.DeleteHtmlTemplate(path.TenantCode, path.FromPath);
                pageTs.DeleteScript(path.TenantCode, path.FromPath);
                domainTs.GenerateDomainModule(path.TenantCode, path.FromPath.GetBeforeLast("/"));
                domainTs.GenerateRoutes(path.TenantCode);

            }

            return result;
        }

        #region Customization
        public PageCustomizationDTO GetCustomizationData(long id)
        {
            var p = Unit.PageRepository.FindSingleAs(d => new { d.ViewPath, d.Name, d.TenantId, d.Tenant.Code, d.Layout }, id);
            if (p == null)
                return null;
            var dto = new PageCustomizationDTO
            {
                Controls = Unit.PageControlRepository.FindAndMap<PageControlListDTO>(d => d.PageId == id),
                Parameters = GetViewParameters(id),
                Route = Unit.PageRouteRepository.FindByPage(id) ?? new PageRouteDTO(),
                Fields = Unit.CustomFieldRepository.FindAndMap<CustomFieldDto>(d => d.PageId == id),
                ViewPath = p.ViewPath,
                Id = id,
                TenantId = p.TenantId,
                TenantCode = p.Code,
                Layout = p.Layout
            };
            return dto;
        }

        public SubmitResult ApplyCustomization(PageCustomizationDTO dto)
        {
            Page page = Unit.PageRepository.GetForCustomization(dto.Id);

            if (dto.Controls != null && dto.Controls.Any())
            {
                page.PageControls.ApplyChangesLong(dto.Controls, Mapper);
            }

            if (dto.Parameters != null && dto.Parameters.Any())
            {
                foreach (var par in dto.Parameters)
                {
                    if (par.Entity.Id == 0)
                    {
                        par.Entity.State = ChangeStates.Added;
                        par.Entity.PageCategoryParameterId = par.Id;
                    }
                    else
                    {
                        par.Entity.State = par.State;
                    }
                }
                page.PageParameters.ApplyChangesLong(dto.Parameters.Select(d => d.Entity), Mapper);
            }

            if (dto.Route != null)
            {
                page.SetRoute(Mapper.Map(dto.Route, new PageRoute()));
            }
            page.CustomFields.ApplyChangesLong(dto.Fields, Mapper);

            var res = Unit.SaveChanges();

            if (res.Code == 0)
            {
                var pars = Unit.PageParameterRepository.FindForJsonByPage(dto.Id);

                if (page.Layout != dto.Layout)
                {
                    page.Layout = dto.Layout;
                    Unit.PageRepository.Update(page);
                }

                if (dto.Route == null)
                    dto.Route = Unit.PageRouteRepository.FindByPage(dto.Id);

                var fs = Unit.CustomFieldRepository.FindAs(e => new FieldDefinition { Name = e.Name, Type = e.Type }, d => d.PageId == dto.Id).ToArray();
                fs = fs.Any() ? fs : null;
                Unit.PageRepository.UpdatePageViewParamsJson(page, pars.ToArray(), dto.Route, fs);
                res.Data["updatingJsonResult"] = Unit.SaveChanges();
            }
            return res;
        }

        public IEnumerable<PageParameterEditDto> GetViewParameters(long id)
        {
            var catId = Unit.PageRepository.GetValue(id, e => e.PageCategoryId);
            var categoryParameters = Unit.PageCategoryParameterRepository.FindAndMap<PageParameterEditDto>(e => e.PageCategoryId == catId);
            var pageParameters = Unit.PageParameterRepository.Find(e => e.PageId == id);
            List<PageReference> references = Unit.PageParameterRepository.GetReferencesByPage(id);

            foreach (var categoryParameter in categoryParameters)
            {
                var pageParameter = pageParameters.FirstOrDefault(e => e.PageCategoryParameterId == categoryParameter.Id);
                if (pageParameter == null)
                {
                    pageParameter = new PageParameter { UseDefault = true };
                }
                else
                {
                    categoryParameter.ViewPath = references.Where(e => e.PageParameterId == pageParameter.Id)
                        .Select(e => e.ViewPath)
                        .FirstOrDefault();
                }
                categoryParameter.Entity = Mapper.Map(pageParameter ?? new PageParameter { UseDefault = true }, new PageParameterDto());
            }
            return categoryParameters;
        }
        #endregion

        public override CreatePageDTO GetSingle(long id)
        {
            var p = Unit.PageRepository.FindSingleAs(a => new CreatePageDTO
            {
                Id = a.Id,
                ActionType = a.PrivilegeType,
                AppsString = a.Apps,
                CategoryId = a.PageCategoryId,
                CollectionId = a.SourceCollectionId,
                ComponentName = a.ViewPath.GetAfterLast("/"),
                ComponentPath = a.ViewPath,
                ComponentDomain = a.Domain.NameChain,
                DefaultAccessibility = a.DefaultAccessibility,
                Layout = a.Layout,
                NavigationGroup = a.NavigationPages.Where(x => x.PageId == id).FirstOrDefault().NavigationGroup.Name,
                Resource = a.Resource.Name,
                RouteParameters = a.RouteParameters,
                SpecialPermission = a.SpecialPermission,
                TemplatePath = a.PageCategory.ViewPath,
                TenantCode = a.Tenant.Code,
                Usage = a.HasRoute ? "R" : a.CanEmbed ? "E" : "RE",
                DomainId = a.DomainId,
                PrivilegeType = a.PrivilegeType,
                ResourceId = a.ResourceId,
                TenantId = a.TenantId
            }, a => a.Id == id);

            p.Apps = p.AppsString != null ? p.AppsString.Replace("\"", "").Replace(" ", "").Split(',').ToList() : null;

            return p;
        }

        public override SubmitResult<CreatePageDTO> Post(CreatePageDTO dto)
        {
            string domainPath = dto.ComponentPath.GetBeforeLast("/");
            var domain = Unit.DomainRepository.GetOrCreatePath(domainPath);
            var submitResult = new SubmitResult<CreatePageDTO>();

            if (dto.Usage == null)
            {
                submitResult.Code = (int)HttpStatusCode.BadRequest;
                submitResult.Message = "Usage cannot be null (R: routable,E: embeddable,RE: both)";
                return submitResult;
            }

            long pageCategory = 0;

            if (dto.CategoryId == null)
                pageCategory = Unit.PageCategoryRepository.GetSingleValue(d => d.Id, e => e.ViewPath == dto.TemplatePath);
            else if (Unit.PageCategoryRepository.Exist(e => e.Id == dto.CategoryId.Value))
                pageCategory = dto.CategoryId.Value;

            if (pageCategory == 0)
            {
                submitResult.Code = (int)HttpStatusCode.BadRequest;
                submitResult.Message = "No Template " + dto.TemplatePath + " or id " + dto.CategoryId;
                return submitResult;
            }


            Tenant tenant = Unit.TenantRepository.FindSingle(d => d.Code == dto.TenantCode);
            if (tenant == null)
            {
                submitResult.Code = (int)HttpStatusCode.BadRequest;
                submitResult.Message = "Invalid tenant : " + dto.TenantCode;
                return submitResult;
            }

            string folder = dto.TemplatePath.GetBeforeLast("/");
            CheckLayout(dto, pageCategory);

            Page p = new Page
            {
                Id = Utils.GenerateID(),
                Name = dto.ComponentPath.GetAfterLast("/"),
                PageCategoryId = pageCategory,
                RouteParameters = dto.RouteParameters,
                ViewPath = dto.ComponentPath,
                SpecialPermission = !string.IsNullOrEmpty(dto.SpecialPermission) ? dto.SpecialPermission : null,
                Layout = !string.IsNullOrEmpty(dto.Layout) ? dto.Layout : null,
                SourceCollectionId = dto.CollectionId,
                DefaultAccessibility = dto.DefaultAccessibility ?? 2,
                HasRoute = dto.Usage.Contains("R"),
                CanEmbed = dto.Usage.Contains("E")
            };

            if (Unit.PageRepository.Exist(d => d.Name == p.Name && d.DomainId == domain.Id && d.TenantId == tenant.Id))
            {
                submitResult.Code = (int)HttpStatusCode.Conflict;
                submitResult.Message = "this page already exists " + dto.TenantCode + "/" + p.ViewPath;
                return submitResult;
            }


            if (!p.HasRoute && !p.CanEmbed)
            {
                submitResult.Code = (int)HttpStatusCode.BadRequest;
                submitResult.Message = "Invalid usage (R: routable, E: embeddable, RE: both)";
                return submitResult;
            }

            p.Apps = GetApps(dto.Apps);
            Resource resource = null;

            if (dto.ResourceId.HasValue)
            {
                p.ResourceId = dto.ResourceId;
                resource = Unit.ResourceRepository.FindSingle(dto.ResourceId);
            }
            else if (!string.IsNullOrEmpty(dto.Resource))
            {
                string serviceName = null;
                string _res = dto.Resource;
                if (dto.Resource.Contains("/"))
                {
                    serviceName = dto.Resource.GetBeforeFirst("/");
                    _res = dto.Resource.GetAfterFirst("/");
                }

                resource = Unit.ResourceRepository.GetResource(_res, serviceName);

                if (resource == null)
                {
                    submitResult.Code = (int)HttpStatusCode.BadRequest;
                    submitResult.Message = "No such Resource " + dto.Resource;
                    return submitResult;
                }

                if (string.IsNullOrEmpty(dto.ActionType) && string.IsNullOrEmpty(dto.SpecialPermission))
                {
                    submitResult.Code = (int)HttpStatusCode.BadRequest;
                    submitResult.Message = "Action type or SpecialPermission is required if resource is not null";
                    return submitResult;
                }
            }

            if (resource != null)
                p.Resource = resource;

            if (!string.IsNullOrEmpty(dto.ActionType) && resource != null)
            {
                if (IsDefaultAction(dto.ActionType))
                {
                    p.PrivilegeType = dto.ActionType;
                }
                else
                {
                    var ra = resource.ResourceActions.Where(d => d.Name.ToLower() == dto.ActionType.ToLower() && d.TenantId == tenant.Id).FirstOrDefault();
                    if (ra == null)
                    {
                        ra = new ResourceAction
                        {
                            Id = Utils.GenerateID(),
                            Name = dto.ActionType,
                            TenantId = tenant.Id
                        };
                        resource.ResourceActions.Add(ra);
                    }
                    p.ResourceAction = ra;
                }
            }

            if (!string.IsNullOrEmpty(dto.NavigationGroup))
            {
                AddToNavigation(p, dto.NavigationGroup);
            }

            p.Tenant = tenant;
            p.Domain = domain;
            Repository.Add(p);

            submitResult = Unit.SaveChanges().ToSubmitResult<CreatePageDTO>();
            submitResult.Data["Id"] = p.Id;
            return submitResult;
        }

        public LoadResult<PageListDTO> GetPagesByDomain(long domainId, LoadOptions opt)
        {
            var op = opt.GetOptionsFor<PageListDTO>();
            if (domainId == -1)
            {
                op.AddFilter(d => d.CanEmbed);
            }
            else
            {
                op.AddFilter(d => d.DomainId == domainId);
            }

            var lst = Unit.PageRepository.FindAndMap(op);
            Unit.PageRepository.FillReferencedBy(lst.List);
            Unit.PageRepository.FillReferences(lst.List);
            return lst;
        }

        public SubmitResult SetViewParams(ViewParamsSetter setter)
        {
            var ps = new List<Page>();
            if (setter.PageName != null)
                ps = Repository.Find(d => d.TenantId == setter.TenantId && d.Name == setter.PageName);
            else if (setter.TemplateName != null)
                ps = Repository.Find(d => d.TenantId == setter.TenantId && d.PageCategory.Name == setter.TemplateName);
            else
                throw new ArgumentException("PageName or TemplateName must be provided");

            if (!ps.Any())
                throw new ArgumentOutOfRangeException("not found");

            foreach (var p in ps)
            {
                var vp = new ViewParams();
                if (p.ViewParams != null)
                    vp = p.ViewParams.FromJson<ViewParams>();

                if (vp.Other == null)
                    vp.Other = new Dictionary<string, string>();

                if (setter.Fields != null)
                {
                    vp.Fields = setter.Fields;
                }

                foreach (var kv in setter.Data)
                {
                    if (kv.Key.ToLower().StartsWith("other."))
                    {
                        string key = kv.Key.GetAfterFirst(".");
                        if (kv.Value.ToLower() == "default")
                        {
                            vp.Other.Remove(key);
                        }
                        else
                        {
                            vp.Other[key] = kv.Value;
                        }

                    }
                    else
                    {
                        var prop = typeof(ViewParams).GetProperty(kv.Key);
                        if (prop != null)
                        {
                            prop.SetValue(vp, kv.Value);
                        }
                    }
                }
                p.ViewParams = vp.ToJson();
            }

            return Unit.SaveChanges();
        }

        #region Private
        private void CheckLayout(CreatePageDTO dto, long pageCategory)
        {
            if (string.IsNullOrEmpty(dto.Layout))
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
                    dto.Layout = "Layout/EmptyLayout";
                }
            }

        }

        protected bool IsDefaultAction(string action)
        {
            if (string.IsNullOrEmpty(action))
                return false;
            string[] strs = new string[] { "view", "insert", "update", "delete", "details" };

            return strs.Contains(action.ToLower());

        }

        protected string GetApps(IEnumerable<string> apps)
        {
            string ret = null;
            if (apps != null && apps.Any())
            {
                ret = "";
                string sep = "";
                foreach (var s in apps)
                {
                    ret += sep + "\"" + s + "\"";
                    sep = ", ";
                }
            }

            return ret;
        }

        protected void AddToNavigation(Page p, string navName)
        {
            var ng = Unit.NavigationGroupRepository.GetNavigationGroup(navName);
            var np = new NavigationPage
            {
                Id = Utils.GenerateID()
            };
            p.NavigationPages.Add(np);
            ng.NavigationPages.Add(np);
        }

        #endregion
    }
}
