using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Data;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.PageCategories
{
    public class PageCategoryParameterService : DataService<IConfigUnit>, IPageCategoryParameterService
    {
        public PageCategoryParameterService(IConfigUnit unit) : base(unit)
        {
        }

        public SubmitResult UpdateParameters(PageCategory cat, List<PageCategoryParameter> lst)
        {
            Unit.PageCategoryParameterRepository.UpdateParameters(cat.Id, lst);
            var s = Unit.SaveChanges(throwException: true);

            return s;
        }
    }
}
