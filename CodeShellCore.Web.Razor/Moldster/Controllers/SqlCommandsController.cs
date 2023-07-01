using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Sql;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
{
    public class SqlCommandsController : BaseApiController
    {
        private readonly EnvironmentAccessor accessor;

        ISqlCommandService sql => GetService<ISqlCommandService>();
        IPathsService paths => GetService<IPathsService>();
        public SqlCommandsController(EnvironmentAccessor accessor)
        {
            this.accessor = accessor;
        }
        public SubmitResult CreateTenantDatabase([FromBody] DbCreationRequest req)
        {
            if (!string.IsNullOrEmpty(req.DbName))
            {
                accessor.CurrentEnvironment = paths.GetEnvironments().Where(d => d.Name == req.Environment).FirstOrDefault();
                return sql.CreateTenantDatabase(req.Id ?? 0, req.TenantCode, req.DbName);
            }
            return SubmitResult;
        }

        public SubmitResult UpdateTenantDatabse([FromBody] DbCreationRequest req)
        {
            accessor.CurrentEnvironment = paths.GetEnvironments().Where(d => d.Name == req.Environment).FirstOrDefault();
            SubmitResult = sql.UpdateDatabase(req.DbName);
            return SubmitResult;
        }

        public IActionResult GetEnvironments()
        {
            var invs = paths.GetEnvironments().Select(d => new { d.Name, d.SourceDatabase, d.Databases });
            return Respond(invs);
        }
    }
}
