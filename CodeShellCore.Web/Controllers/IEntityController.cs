using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Security;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Controllers
{
    public interface IEntityController<T, TPrime> : IReadOnlyEntityController<T,TPrime> where T : class, IEntity<TPrime>
    {
        
        IActionResult Post([FromBody] T obj);
        IActionResult Put([FromBody] T obj);

        
    }
}
