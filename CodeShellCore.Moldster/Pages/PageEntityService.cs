using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Services;
using CodeShellCore.Extensions.Data;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.Pages.Views;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Text;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public class PageEntityService : DtoEntityService<Page, long, PagedListRequestDto, PageListDTO, CreatePageDTO, CreatePageDTO, CreatePageDTO>, IPageEntityService
    {
        private readonly IMoldsterUnit Unit;

        IDomainScriptGenerationService domainTs => Unit.ServiceProvider.GetService<IDomainScriptGenerationService>();
        IPageHtmlGenerationService _html => Unit.ServiceProvider.GetService<IPageHtmlGenerationService>();
        IPageScriptGenerationService pageTs => Unit.ServiceProvider.GetService<IPageScriptGenerationService>();
        IMoldsterLookupService Lookups => Unit.ServiceProvider.GetService<IMoldsterLookupService>();
        public override bool ProjectGetSingle => false;

        public PageEntityService(IMoldsterUnit unit) : base(unit)
        {
            this.Unit = unit;
        }

        public async Task<PagedResult<PageListDTO>> FindPages(PagedListRequestDto opts, FindPageRequest request)
        {
            return await Unit.PageRepository.FindUsing<PageListDTO>(request, Mapper.Map(opts, new PagedListRequest()));

        }

        public override async Task<Dictionary<string, IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> dto)
        {
            return await Lookups.PageEdit(dto);
        }

        public override async Task<EntitySubmitResult<CreatePageDTO>> Put(CreatePageDTO dto)
        {
            bool pathChanged = false;
            string oldPath = null;
            var page = await Unit.PageRepository.FindSingle(dto.Id);
            page.AppendProperties(dto, true, new[] { "Resource", "Apps", "ViewParams" });

            page.Apps = GetApps(dto.Apps);
            if (IsDefaultAction(dto.ActionType))
                page.PrivilegeType = dto.ActionType;
            var dom = await Unit.DomainRepository.GetOrCreatePath(dto.ComponentDomain);
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
            var res = (await Unit.SaveChanges()).ToSubmitResult<CreatePageDTO>();

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

                await _html.MoveHtmlTemplate(moveRequest);
                await pageTs.MoveScript(moveRequest);
                await domainTs.GenerateDomainModule(moveRequest.TenantCode, moveRequest.FromPath.GetBeforeLast("/"));
                await domainTs.GenerateDomainModule(moveRequest.TenantCode, moveRequest.ToPath.GetBeforeLast("/"));
                await domainTs.GenerateRoutes(moveRequest.TenantCode);
            }
            return res;
        }

        public async Task<SubmitResult> UpdatePageRefeneces(long pageId, long tenantId)
        {
            IEnumerable<Page> ps = await Unit.PageRepository.GetReferencing(pageId, tenantId);
            foreach (var p in ps)
            {
                PageParameterForJson[] pars = (await Unit.PageParameterRepository.FindForJsonByPage(p.Id)).ToArray();
                var routeView = await Unit.PageRouteRepository.FindByPage(p.Id);
                var dto = Mapper.Map(routeView, new PageRouteDTO());
                var fs = (await Unit.CustomFieldRepository.FindAs(
                    e => new FieldDefinition { Name = e.Name, Type = e.Type },
                    d => d.PageId == p.Id)).ToArray();
                fs = fs.Any() ? fs : null;

                await Unit.PageRepository.UpdatePageViewParamsJson(p, pars, routeView, fs);
            }
            return await Unit.SaveChanges();
        }

        public override async Task<DeleteResult> Delete(long id)
        {
            var page = await Unit.PageRepository.FindSingleAs(d => new { d.ViewPath, d.Tenant.Code }, d => d.Id.Equals(id));
            Unit.PageRepository.DeleteById(id);
            var result = (await Unit.SaveChanges()).MapToResult<DeleteResult>();
            if (result.IsSuccess)
            {
                var path = new MovePageRequest { TenantCode = page.Code, FromPath = page.ViewPath };

                await _html.DeleteHtmlTemplate(path.TenantCode, path.FromPath);
                await pageTs.DeleteScript(path.TenantCode, path.FromPath);
                await domainTs.GenerateDomainModule(path.TenantCode, path.FromPath.GetBeforeLast("/"));
                await domainTs.GenerateRoutes(path.TenantCode);

            }

            return result;
        }

        #region Customization
        public async Task<PageCustomizationDTO> GetCustomizationData(long id)
        {
            var p = await Unit.PageRepository.FindSingleAs(d => new { d.ViewPath, d.Name, d.TenantId, d.Tenant.Code, d.Layout }, id);
            if (p == null)
                return null;

            var routeView = Unit.PageRouteRepository.FindByPage(id);
            var dto = new PageCustomizationDTO
            {
                Controls = await Unit.PageControlRepository.FindAndMap<PageControlListDTO>(d => d.PageId == id),
                Parameters = await GetViewParameters(id),
                Route = routeView == null ? new PageRouteDTO() : Mapper.Map(routeView, new PageRouteDTO()),
                Fields = await Unit.CustomFieldRepository.FindAndMap<CustomFieldDto>(d => d.PageId == id),
                ViewPath = p.ViewPath,
                Id = id,
                TenantId = p.TenantId,
                TenantCode = p.Code,
                Layout = p.Layout
            };
            return dto;
        }

        public async Task<SubmitResult> ApplyCustomization(PageCustomizationDTO dto)
        {
            Page page = await Unit.PageRepository.GetForCustomization(dto.Id);

            if (dto.Controls != null && dto.Controls.Any())
            {
                page.PageControls.ApplyChanges(dto.Controls, Mapper);
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
                page.PageParameters.ApplyChanges(dto.Parameters.Select(d => d.Entity), Mapper);
            }

            if (dto.Route != null)
            {
                page.SetRoute(Mapper.Map(dto.Route, new PageRoute()));
            }
            page.CustomFields.ApplyChanges(dto.Fields, Mapper);
            // Unit.PageRepository.Update(page);
            var res = await Unit.SaveChanges();

            if (res.Code == 0)
            {
                var pars = await Unit.PageParameterRepository.FindForJsonByPage(dto.Id);
                var routeView = await Unit.PageRouteRepository.FindByPage(dto.Id);

                if (page.Layout != dto.Layout)
                {
                    page.Layout = dto.Layout;
                    Unit.PageRepository.Update(page);
                }

                dto.Route = Mapper.Map(routeView, new PageRouteDTO());

                var fs = (await Unit.CustomFieldRepository.FindAs(e => new FieldDefinition { Name = e.Name, Type = e.Type }, d => d.PageId == dto.Id)).ToArray();
                fs = fs.Any() ? fs : null;
                await Unit.PageRepository.UpdatePageViewParamsJson(page, pars.ToArray(), routeView, fs);
                res.Data["updatingJsonResult"] = Unit.SaveChanges();
            }
            return res;
        }

        public async Task<IEnumerable<PageParameterEditDto>> GetViewParameters(long id)
        {
            var catId = await Unit.PageRepository.GetValue(id, e => e.PageCategoryId);
            var categoryParameters = await Unit.PageCategoryParameterRepository.FindAndMap<PageParameterEditDto>(e => e.PageCategoryId == catId);
            var pageParameters = await Unit.PageParameterRepository.Find(e => e.PageId == id);
            List<PageReference> references = await Unit.PageParameterRepository.GetReferencesByPage(id);

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

        public override async Task<CreatePageDTO> GetSingle(long id)
        {
            var p = await Unit.PageRepository.FindSingleAs(a => new CreatePageDTO
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

        public override async Task<EntitySubmitResult<CreatePageDTO>> Post(CreatePageDTO dto)
        {
            string domainPath = dto.ComponentPath.GetBeforeLast("/");
            var domain = await Unit.DomainRepository.GetOrCreatePath(domainPath);
            var submitResult = new EntitySubmitResult<CreatePageDTO>();

            if (dto.Usage == null)
            {
                submitResult.Code = (int)HttpStatusCode.BadRequest;
                submitResult.Message = "Usage cannot be null (R: routable,E: embeddable,RE: both)";
                return submitResult;
            }

            long pageCategory = 0;

            if (dto.CategoryId == null)
                pageCategory = await Unit.PageCategoryRepository.GetSingleValue(d => d.Id, e => e.ViewPath == dto.TemplatePath);
            else if (await Unit.PageCategoryRepository.Exist(e => e.Id == dto.CategoryId.Value))
                pageCategory = dto.CategoryId.Value;

            if (pageCategory == 0)
            {
                submitResult.Code = (int)HttpStatusCode.BadRequest;
                submitResult.Message = "No Template " + dto.TemplatePath + " or id " + dto.CategoryId;
                return submitResult;
            }


            Tenant tenant = await Unit.TenantRepository.FindSingle(d => d.Code == dto.TenantCode);
            if (tenant == null)
            {
                submitResult.Code = (int)HttpStatusCode.BadRequest;
                submitResult.Message = "Invalid tenant : " + dto.TenantCode;
                return submitResult;
            }

            string folder = dto.TemplatePath.GetBeforeLast("/");
            await CheckLayout(dto, pageCategory);

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

            if (await Unit.PageRepository.Exist(d => d.Name == p.Name && d.DomainId == domain.Id && d.TenantId == tenant.Id))
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
                resource = await Unit.ResourceRepository.FindSingle(dto.ResourceId);
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

                resource = await Unit.ResourceRepository.GetResource(_res, serviceName);

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
                await AddToNavigation(p, dto.NavigationGroup);
            }

            p.Tenant = tenant;
            p.Domain = domain;
            Repository.Add(p);

            submitResult = (await Unit.SaveChanges()).ToSubmitResult<CreatePageDTO>();
            submitResult.Data["Id"] = p.Id;
            return submitResult;
        }

        public async Task<PagedResult<PageListDTO>> GetPagesByDomain(long domainId, PagedListRequestDto opt)
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

            var lst = await Unit.PageRepository.FindAndMap(op);
            await Unit.PageRepository.FillReferencedBy(lst.List);
            await Unit.PageRepository.FillReferences(lst.List);
            return lst;
        }

        public async Task<SubmitResult> SetViewParams(ViewParamsSetter setter)
        {
            var ps = new List<Page>();
            if (setter.PageName != null)
                ps = await Repository.Find(d => d.TenantId == setter.TenantId && d.Name == setter.PageName);
            else if (setter.TemplateName != null)
                ps = await Repository.Find(d => d.TenantId == setter.TenantId && d.PageCategory.Name == setter.TemplateName);
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

            return await Unit.SaveChanges();
        }

        #region Private
        private async Task CheckLayout(CreatePageDTO dto, long pageCategory)
        {
            if (string.IsNullOrEmpty(dto.Layout))
            {
                string baseType = await Unit.PageCategoryRepository.GetSingleValue(d => d.BaseComponent, d => d.Id == pageCategory);
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

        protected async Task AddToNavigation(Page p, string navName)
        {
            var ng = await Unit.NavigationGroupRepository.GetNavigationGroup(navName);
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
