using System;
using System.Linq;
using System.Net;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Text;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Data;
using System.Collections.Generic;
using CodeShellCore.Cli;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Razor;

namespace CodeShellCore.Moldster.Configurator.Services
{
    public class PagesService : EntityService<Page>, IPagesDataService
    {
        IConfigUnit Unit;
        private readonly IOutputWriter output;

        public PagesService(
            IConfigUnit unit,
            IOutputWriter output) : base(unit)
        {
            Unit = unit;
            this.output = output;
        }

        public SubmitResult ViewParamsToData(long id)
        {
            ViewParams jsonParams = new ViewParams();
            List<string> errors = new List<string>();

            var data = Unit.PageRepository.FindSingleAs(d => new { d.ViewParams, d.PageCategoryId, d.TenantId, d.ViewPath }, id);
            output.Write("ViewParams to data " + data.ViewPath);

            if (!string.IsNullOrEmpty(data.ViewParams))
            {
                jsonParams = data.ViewParams.FromJson<ViewParams>();
                UpdateRoutesFromJson(id, data.TenantId, jsonParams, ref errors);
                UpdateParametersFromJson(id, data.TenantId, jsonParams, ref errors);
                UpdateFieldsFromJson(id, jsonParams);
            }

            var res = Unit.SaveChanges();

            if (res.IsSuccess)
            {
                output.Write(" [ Affected : " + res.AffectedRows + " ]");
                output.WriteLine();
                if (errors.Any())
                {
                    using (output.Set(ConsoleColor.Red))
                    {
                        output.WriteLine("Errors Found :");
                        foreach (var err in errors)
                        {
                            output.WriteLine(err);
                        }
                    }

                }
            }
            else
            {
                using (output.Set(ConsoleColor.Red))
                {
                    output.WriteLine(res.Message);
                    output.WriteLine(res.ExceptionMessage);
                    foreach (var err in res.StackTrace)
                    {
                        output.WriteLine(err);
                    }
                }
            }


            return res;
        }

        public void UpdateFieldsFromJson(long id, ViewParams jsonParams)
        {

            if (jsonParams.Fields == null || !jsonParams.Fields.Any())
                return;

            var fs = Unit.CustomFieldRepository.Find(d => d.PageId == id);
            foreach (var f in jsonParams.Fields)
            {
                var dbF = fs.Where(d => d.Name == f.Name).FirstOrDefault();
                if (dbF == null)
                {
                    Unit.CustomFieldRepository.Add(new CustomField
                    {
                        Name = f.Name,
                        Type = f.Type,
                        PageId = id
                    });
                }
                else
                {
                    dbF.Type = f.Type;
                    Unit.CustomFieldRepository.Update(dbF);
                }
            }
        }

        public void UpdateRoutesFromJson(long id, long tenantId, ViewParams jsonParams, ref List<string> errors)
        {
            var rout = Unit.PageRouteRepository.FindSingle(d => d.PageId == id);
            if (rout == null)
            {
                rout = new PageRoute
                {
                    Id = Utils.GenerateID(),
                    PageId = id
                };
                Unit.PageRouteRepository.Add(rout);
            }
            else
            {
                Unit.PageRouteRepository.Update(rout);
            }

            if (jsonParams.AddUrl != null)
                rout.AddUrl = Unit.PageRepository.FindLinkedPage("AddUrl", jsonParams.AddUrl, tenantId, ref errors)?.Id;

            if (jsonParams.EditUrl != null)
                rout.EditUrl = Unit.PageRepository.FindLinkedPage("EditUrl", jsonParams.EditUrl, tenantId, ref errors)?.Id;

            if (jsonParams.DetailsUrl != null)
                rout.DetailsUrl = Unit.PageRepository.FindLinkedPage("DetailsUrl", jsonParams.DetailsUrl, tenantId, ref errors)?.Id;

            if (jsonParams.ListUrl != null)
                rout.ListUrl = Unit.PageRepository.FindLinkedPage("ListUrl", jsonParams.ListUrl, tenantId, ref errors)?.Id;

        }

        PageParameterDTO CreateParameter(string key, string value, long pageId, long tenantId, long catId)
        {

            var param = new PageParameter
            {
                Id = Utils.GenerateID(),
                PageId = pageId,
                ParameterValue = value,
                UseDefault = true,
                State = "Added"
            };

            PageCategoryParameter p = new PageCategoryParameter
            {
                Id = Utils.GenerateID(),
                Name = key,
                DefaultValue = value,
                PageCategoryId = catId,
                Type = (int)PageParameterTypes.Text,
                PageParameters = new List<PageParameter> { param }
            };

            var err = new List<string>();
            var pg = Unit.PageRepository.FindLinkedPage("Other." + key, value, tenantId, ref err);
            if (pg != null)
            {
                param.LinkedPageId = pg.Id;
                p.Type = (int)(pg.Embedded ? PageParameterTypes.Embedded : PageParameterTypes.PageLink);
            }

            Unit.PageCategoryParameterRepository.Add(p);

            return new PageParameterDTO
            {
                Id = p.Id,
                DefaultValue = p.DefaultValue,
                Name = p.Name,
                Entity = param,
                Type = p.Type
            };
        }

        public void UpdateParametersFromJson(long id, long tenantId, ViewParams jsonParams, ref List<string> errors)
        {
            if (jsonParams.Other == null)
                return;
            long catId = Unit.PageRepository.GetValue(id, d => d.PageCategoryId ?? 0);
            List<PageParameterDTO> pars = Unit.PageParameterRepository.FindForPage(id);
            int[] isPageParam = new[] {
                    (int)PageParameterTypes.Embedded,
                    (int)PageParameterTypes.Modal,
                    (int)PageParameterTypes.PageLink
                };

            foreach (var fromJson in jsonParams.Other)
            {
                if (!string.IsNullOrEmpty(fromJson.Value))
                {
                    var par = pars.Where(d => d.Name == fromJson.Key).FirstOrDefault();

                    if (par == null)
                        par = CreateParameter(fromJson.Key, fromJson.Value, id, tenantId, catId);

                    var n = par.Entity;

                    if (par.Entity.Id == 0)
                    {
                        n = new PageParameter
                        {
                            Id = Utils.GenerateID(),
                            PageId = id,
                            PageCategoryParameterId = par.Id,
                            UseDefault = false,
                            ParameterValue = par.Type == 1 ? fromJson.Value : null
                        };

                        Unit.PageParameterRepository.Add(n);
                    }
                    else
                    {
                        n.ParameterValue = par.Type == 1 ? fromJson.Value : null;
                        if (n.State != "Added")
                            Unit.PageParameterRepository.Update(n);
                    }

                    PageAndTypeDTO p = null;
                    if (par.Type != (int)PageParameterTypes.Text)
                        p = Unit.PageRepository.FindLinkedPage("Other." + par.Name, fromJson.Value, tenantId, ref errors);

                    if (p != null)
                    {
                        n.LinkedPageId = p.Id;
                    }
                }
            }
        }

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

        public IEnumerable<long> GetPagesWithJsonParams(string modCode)
        {
            return Unit.PageRepository.GetValues(d => d.Id, d => d.Tenant.Code == modCode && d.ViewParams != null);
        }

        protected bool IsDefaultAction(string action)
        {
            if (string.IsNullOrEmpty(action))
                return false;
            string[] strs = new string[] { "view", "insert", "update", "delete", "details" };

            return strs.Contains(action.ToLower());

        }

        public SubmitResult Create(CreatePageDTO dto)
        {
            string domainPath = dto.ComponentPath.GetBeforeLast("/");
            var domain = Unit.DomainRepository.GetOrCreatePath(domainPath);

            if (dto.Usage == null)
                return new SubmitResult((int)HttpStatusCode.BadRequest, "Usage cannot be null (R: routable,E: embeddable,RE: both)");

            long pageCategory = 0;

            if (dto.CategoryId == null)
                pageCategory = Unit.PageCategoryRepository.GetSingleValue(d => d.Id, e => e.ViewPath == dto.TemplatePath);
            else if (Unit.PageCategoryRepository.Exist(e => e.Id == dto.CategoryId.Value))
                pageCategory = dto.CategoryId.Value;

            if (pageCategory == 0)
                return new SubmitResult((int)HttpStatusCode.BadRequest, "No Template " + dto.TemplatePath + " or id " + dto.CategoryId);

            Tenant tenant = Unit.TenantRepository.FindSingle(d => d.Code == dto.TenantCode);
            if (tenant == null)
                return new SubmitResult((int)HttpStatusCode.BadRequest, "Invalid tenant : " + dto.TenantCode);

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
                return new SubmitResult((int)HttpStatusCode.Conflict, "this page already exists " + dto.TenantCode + "/" + p.ViewPath);

            if (!p.HasRoute && !p.CanEmbed)
                return new SubmitResult((int)HttpStatusCode.BadRequest, "Invalid usage (R: routable, E: embeddable, RE: both)");

            p.Apps = GetApps(dto.Apps);
            Resource res = null;

            if (dto.ResourceId.HasValue)
            {
                p.ResourceId = dto.ResourceId;
                res = Unit.ResourceRepository.FindSingle(dto.ResourceId);
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
                res = Unit.ResourceRepository.GetResource(_res, serviceName);
                if (res == null)
                    return new SubmitResult((int)HttpStatusCode.BadRequest, "No such Resource " + dto.Resource);

                if (string.IsNullOrEmpty(dto.ActionType) && string.IsNullOrEmpty(dto.SpecialPermission))
                    return new SubmitResult(1, "Action type or SpecialPermission is required if resource is not null");

                res.Pages.Add(p);
            }


            if (!string.IsNullOrEmpty(dto.ActionType) && res != null)
            {
                if (IsDefaultAction(dto.ActionType))
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

            if (!string.IsNullOrEmpty(dto.NavigationGroup))
            {
                AddToNavigation(p, dto.NavigationGroup);
            }

            tenant.Pages.Add(p);
            domain.Pages.Add(p);
            dto.DomainId = domain.Id;
            var nn = Unit.SaveChanges();
            nn.Data["Id"] = p.Id;
            return nn;
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

            var lst = Unit.PageRepository.FindAs(PageListDTO.Expression, op);
            Unit.PageRepository.FillReferencedBy(lst.ListT);
            Unit.PageRepository.FillReferences(lst.ListT);
            return lst;
        }

        public LoadResult<PageListDTO> GetAll(LoadOptions op)
        {
            var opts = op.GetOptionsFor<PageListDTO>();
            return Unit.PageRepository.FindAs(PageListDTO.Expression, opts);
        }

        public SubmitResult ApplyCustomization(PageCustomizationDTO dto)
        {
            if (dto.Controls != null && dto.Controls.Any())
            {
                var list = dto.Controls.MapTo<PageControl>(false);
                Unit.PageControlRepository.ApplyChanges(list);
            }

            if (dto.Parameters != null && dto.Parameters.Any())
            {
                foreach (var par in dto.Parameters)
                {
                    if (par.Entity.Id == 0)
                    {
                        par.Entity.State = "Added";
                        par.Entity.PageId = dto.Id;
                        par.Entity.PageCategoryParameterId = par.Id;
                    }
                    else
                    {
                        par.Entity.State = par.State;
                    }
                }
                Unit.PageParameterRepository.ApplyChanges(dto.Parameters.Select(d => d.Entity));
            }
            if (dto.Route != null)
            {
                var rout = Unit.PageRouteRepository.FindSingle(dto.Route.Id);
                if (rout == null)
                {
                    rout = new PageRoute
                    {
                        PageId = dto.Id,
                    };
                    Unit.PageRouteRepository.Add(rout);
                }
                else
                {
                    Unit.PageRouteRepository.Update(rout);
                }
                rout.AppendProperties(dto.Route, true, new[] { "CreatedOn", "CreatedBy", "PageId" });
            }
            Unit.CustomFieldRepository.ApplyChanges(dto.Fields);
            var res = Unit.SaveChanges();
            if (res.Code == 0)
            {
                var pars = Unit.PageParameterRepository.FindForJsonByPage(dto.Id);
                var page = Unit.PageRepository.FindSingle(dto.Id);
                if (page.Layout != dto.Layout)
                {
                    page.Layout = dto.Layout;
                    Unit.PageRepository.Update(page);
                }


                if (dto.Route == null)
                    dto.Route = Unit.PageRouteRepository.FindByPage(dto.Id);
                var fs = Unit.CustomFieldRepository.FindAs(FieldDefinition.Expression, d => d.PageId == dto.Id).ToArray();
                fs = fs.Any() ? fs : null;
                Unit.PageRepository.UpdatePageViewParamsJson(page, pars.ToArray(), dto.Route, fs);
                res.Data["updatingJsonResult"] = Unit.SaveChanges();
            }
            return res;
        }

        public LoadResult<PageListDTO> FindPages(FindPageRequest request, LoadOptions opts)
        {
            return Unit.PageRepository.FindUsing(request, opts);

        }

        public PageCustomizationDTO GetCustomizationData(long id)
        {
            var p = Unit.PageRepository.FindSingleAs(d => new { d.ViewPath, d.Name, d.TenantId, d.Tenant.Code, d.Layout }, id);
            if (p == null)
                return null;
            var dto = new PageCustomizationDTO
            {
                Controls = Unit.PageControlRepository.FindAs(PageControlListDTO.Expression, d => d.PageId == id),
                Parameters = Unit.PageParameterRepository.FindForPage(id),
                Route = Unit.PageRouteRepository.FindByPage(id) ?? new PageRouteDTO(),
                Fields = Unit.CustomFieldRepository.Find(d => d.PageId == id),
                ViewPath = p.ViewPath,
                Id = id,
                TenantId = p.TenantId,
                TenantCode = p.Code,
                Layout = p.Layout
            };
            return dto;
        }

        public IEnumerable<PageParameterDTO> GetViewParameters(long id)
        {
            return Unit.PageParameterRepository.FindForPage(id);
        }

        public PageAcquisitorDTO GetPageAcquisitor(long pageId)
        {
            return Repository.FindSingleAs(d => new PageAcquisitorDTO { ModuleCode = d.Tenant.Code, ViewPath = d.ViewPath }, pageId);
        }

        public CreatePageDTO GetSinglePage(long id)
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

        public SubmitResult UpdatePageRefeneces(long pageId, long tenantId)
        {
            IEnumerable<Page> ps = Unit.PageRepository.GetReferencing(pageId, tenantId);
            foreach (var p in ps)
            {
                PageParameterForJson[] pars = Unit.PageParameterRepository.FindForJsonByPage(p.Id).ToArray();
                PageRouteDTO r = Unit.PageRouteRepository.FindByPage(p.Id);
                var fs = Unit.CustomFieldRepository.FindAs(FieldDefinition.Expression, d => d.PageId == p.Id).ToArray();
                fs = fs.Any() ? fs : null;
                Unit.PageRepository.UpdatePageViewParamsJson(p, pars, r, fs);
            }
            return Unit.SaveChanges();
        }

        public override DeleteResult DeleteById(object prime)
        {
            var v = Unit.PageRepository.FindSingleAs(d => new { d.ViewPath, d.Tenant.Code }, d => d.Id.Equals(prime));
            var success = base.DeleteById(prime);
            if (success.IsSuccess)
                success.Data["ViewPath"] = new MovePageRequest { TenantCode = v.Code, FromPath = v.ViewPath };
            return success;
        }

        public SubmitResult UpdatePage(CreatePageDTO dto)
        {
            bool pathChanged = false;
            string oldPath = null;
            var page = Unit.PageRepository.FindSingle(dto.Id);
            page.AppendProperties(dto, true, new[] { "Resource", "Apps", "ViewParams" });

            page.Apps = GetApps(dto.Apps);
            if (IsDefaultAction(dto.ActionType))
                page.PrivilegeType = dto.ActionType;
            var dom = Unit.DomainRepository.GetOrCreatePath(dto.ComponentDomain);
            dom.Pages.Add(page);
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
            var result = Update(page);
            if (result.IsSuccess && pathChanged)
            {
                result.Data["ReferenceUpdateResult"] = UpdatePageRefeneces(dto.Id, dto.TenantId);
                result.Data["MoveRequest"] = new MovePageRequest
                {
                    DomainId = page.DomainId,
                    FromPath = oldPath,
                    ToPath = page.ViewPath,
                    PageId = page.Id,
                    TenantCode = dto.TenantCode
                };
            }
            return result;
        }


    }
}
