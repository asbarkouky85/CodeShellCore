using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Controllers.Configurator
{
    public class SqlCommandsController : SqlCommandsControllerBase
    {
        public SqlCommandsController(EnvironmentAccessor accessor) : base(accessor)
        {
        }
    }
}
