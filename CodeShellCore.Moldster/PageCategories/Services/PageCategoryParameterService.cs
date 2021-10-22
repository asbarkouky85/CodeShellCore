using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.PageCategories.Services
{
    public class PageCategoryParameterService : DataService<IConfigUnit>, IPageCategoryParameterService
    {
        public PageCategoryParameterService(IConfigUnit unit) : base(unit)
        {
        }

        public SubmitResult UpdateParameters(PageCategory cat, List<PageCategoryParameterDTO> lst)
        {
            Unit.PageCategoryParameterRepository.UpdateParameters(cat.Id, lst);
            var s = Unit.SaveChanges();

            return s;
        }
    }
}
