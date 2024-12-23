using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Sql
{
    public interface ISqlCommandService : IServiceBase
    {
        int CommandTimeout { get; set; }
        bool CompareStructures(string db, out string updateScript);
        Task SaveComparisonFile(string db, string addition, bool tables = false);
        Task<SubmitResult> UpdateDatabase(string db);
        Task<SubmitResult> RunUpdateScript(string db, string updateScript, bool showResult = true, string message = null, bool saveFile = true);
        Task SyncSchemas(string db);
        Task SyncBasicData(string db);
        Task<SubmitResult> CreateTenantDatabase(long id, string code, string dbName);
        Task BeforeComparisonInitiation(string db);
        Task AfterComparisonInitiation(long id, string code, string db);
        Task<string[]> GetDatabaseList();
        Task AddMigrationTable();
    }
}
