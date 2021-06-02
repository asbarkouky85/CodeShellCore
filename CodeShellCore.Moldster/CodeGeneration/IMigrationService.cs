using CodeShellCore.Helpers;

namespace CodeShellCore.Moldster.CodeGeneration
{
    public interface IMigrationService
    {
        Result MigrateBaseModule(string tenant);
    }
}