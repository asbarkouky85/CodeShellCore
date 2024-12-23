using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Moldster.Sql
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
        public async Task<SubmitResult> CreateTenantDatabase([FromBody] DbCreationRequest req)
        {
            if (!string.IsNullOrEmpty(req.DbName))
            {
                accessor.CurrentEnvironment = paths.GetEnvironments().Where(d => d.Name == req.Environment).FirstOrDefault();
                return await sql.CreateTenantDatabase(req.Id ?? 0, req.TenantCode, req.DbName);
            }
            return SubmitResult;
        }

        public async Task<SubmitResult> UpdateTenantDatabse([FromBody] DbCreationRequest req)
        {
            accessor.CurrentEnvironment = paths.GetEnvironments().Where(d => d.Name == req.Environment).FirstOrDefault();
            SubmitResult = await sql.UpdateDatabase(req.DbName);
            return SubmitResult;
        }

        public Task<List<MoldsterEnvironmentDto>> GetEnvironments()
        {
            var invs = paths.GetEnvironments().Select(d => new MoldsterEnvironmentDto
            {
                Name = d.Name,
                SourceDatabase = d.SourceDatabase,
                Databases = d.Databases
            }).ToList();
            return Task.FromResult(invs);
        }
    }
}
