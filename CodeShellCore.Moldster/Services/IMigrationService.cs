using CodeShellCore.Helpers;

namespace CodeShellCore.Moldster.Services
{
    public interface IMigrationService
    {
        Result MigrateBaseModule(string tenant);
    }
}