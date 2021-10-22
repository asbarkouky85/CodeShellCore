using CodeShellCore.Helpers;

namespace CodeShellCore.Moldster.Builder
{
    public interface ITenantScriptGenerationService
    {
        Result AddTenantToAngularJson(string tenant);
    }
}