using CodeShellCore.Helpers;

namespace CodeShellCore.Moldster.CodeGeneration.Services
{
    public interface IMigrationService
    {
        Result MigrateBaseModule(string tenant);
    }
}