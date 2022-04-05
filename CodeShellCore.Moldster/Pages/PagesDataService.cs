using CodeShellCore.Cli;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Data.Services;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Services;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Pages
{
    public class PagesDataService : DataService<IConfigUnit>, IPagesDataService
    {
        private readonly IObjectMapper Mapper;
        private readonly IOutputWriter output;

        public PagesDataService(
            IConfigUnit unit,
            IObjectMapper mapper,
            IOutputWriter output) : base(unit)
        {
            Mapper = mapper;
            this.output = output;
        }

        public IEnumerable<PageParameterEditDto> GetViewParameters(long id)
        {
            var catId = Unit.PageRepository.GetValue(id, e => e.PageCategoryId);
            var categoryParameters = Unit.PageCategoryParameterRepository.FindAndMap<PageParameterEditDto>(e => e.PageCategoryId == catId);
            var pageParameters = Unit.PageParameterRepository.FindAndMap<PageParameter>(e => e.PageId == id);
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

        PageParameterEditDto CreateParameter(string key, string value, long pageId, long tenantId, long catId)
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

            return new PageParameterEditDto
            {
                Id = p.Id,
                DefaultValue = p.DefaultValue,
                Name = p.Name,
                Entity = Mapper.Map(param, new PageParameterDto()),
                Type = p.Type
            };
        }

        public void UpdateParametersFromJson(long id, long tenantId, ViewParams jsonParams, ref List<string> errors)
        {
            if (jsonParams.Other == null)
                return;
            long catId = Unit.PageRepository.GetValue(id, d => d.PageCategoryId ?? 0);
            List<PageParameterEditDto> pars = GetViewParameters(id).ToList();
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

                    var n = Mapper.Map(par.Entity, new PageParameter());

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

        public IEnumerable<long> GetPagesWithJsonParams(string modCode)
        {
            return Unit.PageRepository.GetValues(d => d.Id, d => d.Tenant.Code == modCode && d.ViewParams != null);
        }
    }
}
