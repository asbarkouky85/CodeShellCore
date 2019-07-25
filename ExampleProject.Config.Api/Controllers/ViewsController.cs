using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Web.Razor.Controllers;
using CodeShellCore.Moldster.Razor.Services;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Web.Razor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleProject.Config.Api.Controllers
{
    public class ViewsController : DbViewsControllerBase
    {
        public ViewsController(
            IRazorProcessorService ser,
            IDataService data,
            IConfigUnit unit) 
            : base(ser, data, unit)
        {
        }
    }
}
