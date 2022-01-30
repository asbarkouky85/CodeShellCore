using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Web.Razor.Controllers
{
    public class SqlCommandsControllerBase : BaseApiController
    {
        private readonly EnvironmentAccessor accessor;

        ISqlCommandService sql => GetService<ISqlCommandService>();
        IPathsService paths => GetService<IPathsService>();
        public SqlCommandsControllerBase(EnvironmentAccessor accessor)
        {
            this.accessor = accessor;
        }
        public IActionResult CreateTenantDatabase([FromBody]DbCreationRequest req)
        {
            if (!string.IsNullOrEmpty(req.DbName))
            {
                accessor.CurrentEnvironment = paths.GetEnvironments().Where(d => d.Name == req.Environment).FirstOrDefault();
                SubmitResult = sql.CreateTenantDatabase(req.Id ?? 0, req.TenantCode, req.DbName);
            }
            
            return Respond();
        }

        public IActionResult UpdateTenantDatabse([FromBody]DbCreationRequest req)
        {
            accessor.CurrentEnvironment = paths.GetEnvironments().Where(d => d.Name == req.Environment).FirstOrDefault();
            SubmitResult = sql.UpdateDatabase(req.DbName);
            return Respond();
        }

        public IActionResult GetEnvironments()
        {
            var invs = paths.GetEnvironments().Select(d => new { d.Name, d.SourceDatabase, d.Databases });
            return Respond(invs);
        }
    }
}
