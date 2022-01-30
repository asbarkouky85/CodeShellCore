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
    public interface IEntityController<T, TPrime> where T : class, IModel<TPrime>
    {
        IActionResult Get([FromQuery]LoadOptions opt);
        IActionResult GetSingle([FromRoute]TPrime id);
        IActionResult Post([FromBody] T obj);
        IActionResult Put([FromBody] T obj);
        IActionResult Delete(TPrime id);
    }
}
