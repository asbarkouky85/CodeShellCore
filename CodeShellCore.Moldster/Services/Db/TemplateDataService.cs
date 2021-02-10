using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Db.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services.Db
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
