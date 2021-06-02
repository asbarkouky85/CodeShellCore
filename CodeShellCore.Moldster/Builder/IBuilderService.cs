using CodeShellCore.Helpers;

namespace CodeShellCore.Moldster.Builder
{
    public interface IBuilderService
    {
        Result AddTenantToAngularJson(string tenant);
        Result MigrateBaseModule(string tenant);
    }
}