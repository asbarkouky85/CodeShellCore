using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Data
{
    public interface ISqlCommandService : IServiceBase
    {
        int CommandTimeout { get; set; }
        bool CompareStructures(string db, out string updateScript);
        void SaveComparisonFile(string db, string addition, bool tables = false);
        SubmitResult UpdateDatabase(string db);
        SubmitResult RunUpdateScript(string db, string updateScript, bool showResult = true, string message = null, bool saveFile = true);
        void SyncSchemas(string db);
        void SyncBasicData(string db);
        SubmitResult CreateTenantDatabase(long id, string code, string dbName);
        void BeforeComparisonInitiation(string db);
        void AfterComparisonInitiation(long id, string code, string db);
        string[] GetDatabaseList();
        void AddMigrationTable();
    }
}
