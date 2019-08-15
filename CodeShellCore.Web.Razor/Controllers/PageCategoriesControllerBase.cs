using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Services;
using CodeShellCore.Text;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Controllers
{

    public class PageCategoriesControllerBase : EntityController<PageCategory, long>, IEntityController<PageCategory, long>
    {
        DomainService Domains => GetService<DomainService>();
        public PageCategoriesControllerBase(PageCategoryService service) : base(service)
        {
        }

        public IActionResult Post([FromBody] PageCategory obj)
        {
            var domPath = Domains.CreatePathAndGetId(obj.ViewPath.GetBeforeLast("/"));
            obj.DomainId = (long)domPath.Data["LastId"];
            return DefaultPost(obj);
        }

        public IActionResult Put([FromBody] PageCategory obj)
        {
            return DefaultPut(obj);
        }
    }
}
