using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Data.Internal
{ 
    public class TemplateDataService : DataService<IConfigUnit>, ITemplateDataService
    {
        public TemplateDataService(IConfigUnit unit) : base(unit)
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
