using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Razor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Data.Internal
{
    public class PageParameterDataService : DataService<IConfigUnit>, IPageParameterDataService
    {
        public PageParameterDataService(IConfigUnit unit) : base(unit)
        {

        }

        public LoadResult<PageReferenceDTO> GetReferences(ParameterRequestDTO req, LoadOptions opt)
        {
            var o = opt.GetOptionsFor<PageReferenceDTO>();
            return Unit.PageParameterRepository.FindReferences(req, o);
        }

        public SubmitResult UpdateJsonFromDataForTenant(long tar)
        {
            throw new NotImplementedException();
        }

        public SubmitResult UpdateTemplatePages(long id, long tenantId)
        {
            var textType = (long)PageParameterTypes.Text;
            var pages = Unit.PageRepository.GetValues(d => d.Id, d => d.TenantId == tenantId && d.PageCategoryId == id);
            IEnumerable<PageCategoryParameterWithPageId> categoryValues = Unit.PageCategoryParameterRepository.FindForPageParameterUpdate(id, tenantId);
            IEnumerable<PageParameter> all = Unit.PageParameterRepository.Find(d => d.Page.PageCategoryId == id && d.Page.TenantId == tenantId);

            foreach (var p in pages)
            {

                foreach (var categoryParameter in categoryValues)
                {
                    var ex = all.Where(d => d.PageCategoryParameterId == categoryParameter.PageCategoryParameterId && d.PageId == p).FirstOrDefault();
                    if (ex == null)
                    {
                        ex = new PageParameter
                        {
                            Id = Utils.GenerateID(),
                            PageCategoryParameterId = categoryParameter.PageCategoryParameterId,
                            PageId = p,
                            ParameterValue = categoryParameter.Type == textType ? categoryParameter.DefaultValue : null,
                            LinkedPageId = categoryParameter.LinkedPageId,
                            UseDefault = true
                        };
                        Unit.PageParameterRepository.Add(ex);
                    }
                    else if (ex.UseDefault)
                    {
                        ex.LinkedPageId = categoryParameter.LinkedPageId;
                        ex.ParameterValue = categoryParameter.Type == textType ? categoryParameter.DefaultValue : null;
                        Unit.PageParameterRepository.Update(ex);
                    }
                }
            }
            return Unit.SaveChanges();
        }



        public SubmitResult UpdateTemplatePagesViewParamsJson(long tenantId, long? pageCategoryId = null)
        {

            var pagesByCategoryAndTenant = Unit.PageRepository.Find(d => (d.PageCategoryId == pageCategoryId || pageCategoryId == null) && d.TenantId == tenantId);
            IEnumerable<PageParameterForJson> pageParametersByTenantAndCategory = Unit.PageParameterRepository.FindForJson(tenantId, pageCategoryId);
            IEnumerable<PageRouteDTO> pageRoutesByTenantAndCategory = Unit.PageRouteRepository.FindForJson(tenantId, pageCategoryId);
            IEnumerable<CustomField> fieldsByTenantAndCategory = Unit.CustomFieldRepository.Find(d => (d.Page.PageCategoryId == pageCategoryId || pageCategoryId == null) && d.Page.TenantId == tenantId);
            var func = FieldDefinition.Expression.Compile();
            foreach (var page in pagesByCategoryAndTenant)
            {
                //if (page.PageCategoryId == 1830435732000)
                //    Console.WriteLine("");
                var pageParameters = pageParametersByTenantAndCategory.Where(d => d.PageId == page.Id).ToArray();
                var pageRoutes = pageRoutesByTenantAndCategory.FirstOrDefault(d => d.PageId == page.Id);
                var pageFields = fieldsByTenantAndCategory.Where(d => d.PageId == page.Id).Select(func).ToArray();
                Unit.PageRepository.UpdatePageViewParamsJson(page, pageParameters, pageRoutes, pageFields);
            }
            return Unit.SaveChanges();
        }
    }
}
