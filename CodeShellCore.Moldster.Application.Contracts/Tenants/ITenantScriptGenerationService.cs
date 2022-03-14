using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;

namespace CodeShellCore.Moldster.Tenants
{
    public interface ITenantScriptGenerationService
    {
        AngularJsonFile ReadAngularJsonFile();
        void UpdateAngularJsonFromDatabase();
        Result AddAngularJson(string tenant);
        void GenerateMainFile(string tenantCode, bool addStyle = false);
        void GenerateAppModule(string tenantCode);
    }
}