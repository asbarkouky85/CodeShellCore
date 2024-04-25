using System.Threading.Tasks;

namespace CodeShellCore.EntityFramework.Migrations
{
    public interface IDbMigrationService
    {
        Task MigrateAsync();
    }
}
