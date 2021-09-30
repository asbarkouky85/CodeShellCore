using CodeShellCore.Cli;
using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Data.Sql;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Definitions;
using System;
using System.IO;
using System.Linq;

namespace CodeShellCore.Moldster.Data.Internal
{


    public class SqlCommandService : SqlService, ISqlCommandService
    {
        private readonly EnvironmentAccessor accessor;
        private readonly IConfigUnit unit;

        protected virtual string SyncSchemasScript { get; }
        protected virtual string UpdateBasicDataScript { get; }
        protected virtual string PrimaryKeyDefinition { get { return "Id bigint"; } }

        public SqlCommandService(
            EnvironmentAccessor accessor,
            IConfigUnit unit,
            IOutputWriter output) : base(output)
        {
            this.accessor = accessor;
            this.unit = unit;
        }

        private MoldsterEnvironment Environment { get { return accessor.CurrentEnvironment; } }
        protected override DbConnectionParams ConnectionParams { get { return Environment.ConnectionParams; } }
        public virtual long GetNewTenantId() { return unit.TenantRepository.GetMax(d => d.Id) + 1; }
        public virtual SubmitResult RunUpdateScript(string db, string updateScript, bool showResult = true, string message = null, bool saveFile = true)
        {
            message = message ?? $"Updating {db} Structure ... ";
            var res = new SubmitResult();
            using (var w = SW.Measure())
            {
                Out.Write(WriteLogMessage(message));
                Environment.ConnectionParams.Database = db;
                res = ExecuteBatchNonQuery(updateScript, Environment.ConnectionParams.ConnectionString);
                if (res.IsSuccess)
                {
                    WriteSuccess(w.Elapsed);
                }
                else
                {
                    WriteFailed(w.Elapsed, res);
                }

                Out.WriteLine();
            }

            return res;
        }

        public virtual void SyncBasicData(string db)
        {
            var scr = UpdateBasicDataScript;
            if (!string.IsNullOrEmpty(scr))
                RunUpdateScript(db, scr, false, $"Updating basic data for {db}", false);
        }

        public virtual void SyncSchemas(string db)
        {
            var scr = SyncSchemasScript;
            if (!string.IsNullOrEmpty(scr))
                RunUpdateScript(db, scr, false, $"Syncing schemas for {db}", false);
        }

        public virtual void SaveComparisonFile(string db, string addition, bool tables = false)
        {
            string added = tables ? "_ADDED_TABLES" : "";
            string path = $"./Results/COMPARISON_{Environment.Name}_{Environment.SourceDatabase}_TO_{db}{added}.sql";
            Utils.CreateFolderForFile(path);
            File.WriteAllText(path, addition);

            Out.Write(WriteLogMessage("Script saved ["));
            using (Out.Set(ConsoleColor.Yellow))
            {
                Out.Write(path);
            }
            Out.Write("]");
            Out.WriteLine();
        }

        protected virtual string WriteLogMessage(string st)
        {
            return $"[{Environment.ConnectionParams.Server} : {Environment.SourceDatabase}] {st}";
        }

        public virtual bool CompareStructures(string db, out string script)
        {
            using (var w = SW.Measure())
            {
                Out.Write(WriteLogMessage($"Comparing {db} to source"));

                script = GetQueryOutput($"exec master.dbo.CompareStructures '[{Environment?.SourceDatabase}]', '[{db}]', '{PrimaryKeyDefinition}'", Environment.ConnectionParams.ConnectionString);
                var lines = script.Split('\n');
                if (lines.Length > 0 && !lines[0].Contains("-- NO ADDED TABLES"))
                {
                    return true;
                }
                return false;
            }
        }

        protected virtual SubmitResult UpdateDbStructure(string db)
        {
            SubmitResult res = new SubmitResult();
            SubmitResult tableAdd = null;
            string updateScript = null;
            if (CompareStructures(db, out updateScript))
            {
                Out.WriteLine();
                Out.WriteLine("Discovered tables needs to be added first");
                SaveComparisonFile(db, updateScript, true);
                tableAdd = RunUpdateScript(db, updateScript);
                if (CompareStructures(db, out updateScript))
                {
                    Out.WriteLine("Tables still need to be added");
                    Out.WriteLine(updateScript);
                    return res;
                }
            }
            SaveComparisonFile(db, updateScript);
            res = RunUpdateScript(db, updateScript);
            if (tableAdd != null)
                res.Data["TableAddResult"] = tableAdd;
            SyncSchemas(db);
            SyncBasicData(db);
            Out.WriteLine();

            return res;
        }

        public virtual void BeforeComparisonInitiation(string db) { }
        public virtual void AfterComparisonInitiation(long id, string code, string db) { }

        public SubmitResult CreateTenantDatabase(long id, string code, string dbName)
        {
            var env = Environment;
            WriteFileOperation("Creating database", dbName, false);
            var res = RunSql("CREATE DATABASE [" + dbName + "]");


            if (res.IsSuccess)
            {
                WriteSuccess();
                Out.WriteLine();
                BeforeComparisonInitiation(dbName);

                if (!string.IsNullOrEmpty(env.SourceDatabase))
                {
                    var r2 = UpdateDbStructure(dbName);
                    if (!r2.IsSuccess)
                        return r2;
                    else
                        res.Data["UpdateStructureRes"] = r2;
                }

                if (id == 0)
                    id = GetNewTenantId();
                AfterComparisonInitiation(id, code, dbName);
                env.ConnectionParams.Database = dbName;
                res.Data["ConnectionString"] = env.ConnectionParams.ConnectionString;
                res.Data["TenantId"] = id;
                env.ConnectionParams.Database = null;
            }
            else
            {

                WriteFailed(null, res);
            }
            return res;
        }

        //public SubmitResult RestoreSourceDatabase()
        //{
        //    var env = Environment;
        //    SubmitResult res = new SubmitResult();
        //    if (!string.IsNullOrEmpty(env.SourceBackupPath) && !string.IsNullOrEmpty(env.SourceDatabase))
        //    {
        //        res = RestoreDatabase(env.SourceDatabase, env.SourceBackupPath);
        //    }
        //    else
        //    {
        //        Out.WriteLine("'SourceDataBase' and 'SourceBackupPath' are both required to be present in the environment to use this function");
        //    }
        //    Out.WriteLine();
        //    return res;
        //}

        //public SubmitResult RestoreConfigDatabase()
        //{
        //    var env = Environment;
        //    SubmitResult res = new SubmitResult();
        //    if (!string.IsNullOrEmpty(env.ConfigBackupPath) && !string.IsNullOrEmpty(env.ConfigDatabase))
        //    {
        //        res = RestoreDatabase(env.ConfigDatabase, env.ConfigBackupPath);
        //    }
        //    else
        //    {
        //        Out.WriteLine("'ConfigBackupPath' and 'ConfigDatabase' are both required to be present in the environment to use this function");
        //    }
        //    Out.WriteLine();
        //    return res;
        //}

        public SubmitResult UpdateDatabase(string db)
        {
            string updateScript = null;
            CommandTimeout = 90;
            SubmitResult res = new SubmitResult();
            SubmitResult tableAdd = new SubmitResult();
            if (CompareStructures(db, out updateScript))
            {
                Out.WriteLine();
                Out.WriteLine("Discovered tables needs to be added first");
                SaveComparisonFile(db, updateScript, true);
                tableAdd = RunUpdateScript(db, updateScript);
                if (CompareStructures(db, out updateScript))
                {
                    Out.WriteLine("Tables still need to be added");
                    Out.WriteLine(updateScript);
                    return new SubmitResult(1, "something_went_wrong");
                }
            }

            SaveComparisonFile(db, updateScript);
            res = RunUpdateScript(db, updateScript);

            if (res.IsSuccess)
            {
                SyncSchemas(db);
                SyncBasicData(db);
            }

            Out.WriteLine();
            return res;
        }

        public string[] GetDatabaseList()
        {
            return GetDataAs<string>("select name from master.sys.databases where owner_sid !=0x01").ToArray();
        }
    }
}
