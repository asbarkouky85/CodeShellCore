using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Configurator.Config.Api.Controllers
{
    public class SqlCommandsController : SqlCommandsControllerBase
    {
        public SqlCommandsController(EnvironmentAccessor accessor) : base(accessor)
        {
        }
    }
}
