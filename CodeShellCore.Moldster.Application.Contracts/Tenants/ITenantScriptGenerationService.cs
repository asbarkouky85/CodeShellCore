using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Tenants
{
    public interface ITenantScriptGenerationService
    {
        Task<AngularJsonFile> ReadAngularJsonFile();
        Task<Result> AddAngularJson(string tenant);
        Task UpdateAngularJsonFromDatabase();
        Task GenerateMainFile(string tenantCode, bool addStyle = false);
        Task GenerateAppModule(string tenantCode);
    }
}