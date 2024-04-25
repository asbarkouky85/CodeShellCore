using CodeShellCore.Data;
using CodeShellCore.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Controllers
{
    public interface IReadOnlyEntityController<T, TPrime> where T : class, IEntity<TPrime>
    {
        IActionResult Get([FromQuery] LoadOptions opt);
        IActionResult GetSingle([FromRoute] TPrime id);
        IActionResult Delete(TPrime id);
    }
}
