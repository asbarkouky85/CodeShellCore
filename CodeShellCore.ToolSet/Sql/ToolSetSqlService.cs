using CodeShellCore.Cli;
using CodeShellCore.Data.Sql;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodeShellCore.ToolSet.Sql
{
    public class ToolSetSqlService : ConsoleService
    {
        public virtual int CommandTimeout { get; set; }
        public virtual DbConnectionParams ConnectionParams { get; set; }
        public override int SuccessCol => 10;

        public ToolSetSqlService()
        {
        }

        public string GetQueryOutput(string query, string connectionString)
        {
            string data = "";
            using (var conn = new SqlConnection(connectionString))
            {
                conn.InfoMessage += (d, s) =>
                {
                    data += s.Message;
                };
                conn.Open();
                var cmd = new SqlCommand(query, conn);
                var res = cmd.ExecuteNonQuery();
            }
            return data;
        }

        protected IEnumerable<DatabaseFile> MakeNewDbFiles(string dbName, string backupPath)
        {
            IEnumerable<DatabaseFile> files = new List<DatabaseFile>();
            try
            {
                var q = $"RESTORE FILELISTONLY FROM DISK = '" + backupPath + "'";

                var fs = GetDataAs<DatabaseFile>(q);

                var paths = GetDataAs<SqlPaths>(@"SELECT SERVERPROPERTY('instancedefaultdatapath') AS [DefaultFile],SERVERPROPERTY('instancedefaultlogpath') AS [DefaultLog]").FirstOrDefault();
                foreach (var f in fs)
                {
                    var lg = f.PhysicalName.Contains(".ldf") ? "_log.ldf" : ".mdf";
                    (files as List<DatabaseFile>).Add(new DatabaseFile { LogicalName = f.LogicalName, PhysicalName = Path.Combine(paths.DefaultFile, dbName + lg) });
                }

            }
            catch
            {

            }
            return files;
        }

        protected IEnumerable<DatabaseFile> GetDatabaseFiles(string dbName)
        {
            IEnumerable<DatabaseFile> files = new List<DatabaseFile>();
            try
            {
                var q = $"select Count(*) as Count from master.dbo.sysdatabases where name='{dbName}'";
                var c = GetDataAs<CountModel>(q).FirstOrDefault();
                if (c.Count > 0)
                {
                    q = @"select name LogicalName,physical_name PhysicalName from [" + dbName + "].sys.database_files";
                    files = GetDataAs<DatabaseFile>(q, dbName);
                }
            }
            catch
            {

            }
            return files;
        }

        protected IEnumerable<T> GetDataAs<T>(string sql, string database = null)
        {
            ConnectionParams.Database = database;
            using (SqlConnection conn = new SqlConnection(ConnectionParams.ConnectionString))
            {
                try
                {
                    List<T> lst = new List<T>();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (CommandTimeout != 0)
                        cmd.CommandTimeout = CommandTimeout;
                    var red = cmd.ExecuteReader();
                    var props = typeof(T).GetProperties().ToDictionary(d => d.Name);
                    while (red.Read())
                    {
                        var inst = Activator.CreateInstance<T>();
                        for (var i = 0; i < red.FieldCount; i++)
                        {
                            var name = red.GetName(i);
                            if (props.TryGetValue(name, out PropertyInfo inf))
                            {
                                var s = red.GetValue(i);
                                if (s.GetType() != typeof(DBNull))
                                    inf.SetValue(inst, Convert.ChangeType(s, inf.PropertyType));
                            }
                        }
                        lst.Add(inst);
                    }
                    conn.Close();
                    conn.Dispose();
                    return lst;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Batch failed");
                    Console.WriteLine(sql);
                    using (ColorSetter.Set(ConsoleColor.Red))
                        Console.WriteLine(ex.GetMessageRecursive());
                    conn.Close();
                    throw;
                }
            }
        }

        protected Result ExecuteBatchNonQuery(string sql, string connectionString, bool logResponse = false)
        {
            string sqlBatch = string.Empty;
            sql += "\nGO";   // make sure last batch is executed.

            Result res = new Result();
            foreach (string line in sql.Split(new string[2] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
            {

                if (line.ToUpperInvariant().Trim() == "GO" && !string.IsNullOrEmpty(sqlBatch))
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            if (logResponse)
                            {

                                conn.InfoMessage += (s, a) =>
                                {
                                    Console.WriteLine(a.Message);
                                    foreach (var e in a.Errors)
                                    {
                                        Console.WriteLine(e);
                                    }
                                    Console.WriteLine("Source : " + a.Source);
                                };
                            }
                            conn.Open();
                            SqlCommand cmd = new SqlCommand(sqlBatch, conn);

                            if (CommandTimeout != 0)
                                cmd.CommandTimeout = CommandTimeout;


                            var rows = cmd.ExecuteNonQuery();
                            res.Message = "Affected rows " + rows;

                            sqlBatch = string.Empty;
                            conn.Close();

                        }
                        catch (Exception ex)
                        {
                            conn.Close();
                            res.SetException(ex);
                            res.Code = 1;

                            res.Data["SQL"] = sqlBatch;
                            break;
                        }
                    }

                }
                else
                {
                    sqlBatch += line + "\n";
                }

            }
            return res;
        }



        public Result RunSql(string script, string db = null, bool logResponse = false)
        {
            ConnectionParams.Database = db;
            return ExecuteBatchNonQuery(script, ConnectionParams.ConnectionString, logResponse);
        }

        public Result RestoreDatabase(string dbName, string backupPath)
        {
            var files = GetDatabaseFiles(dbName);
            string ext = "";
            if (!files.Any())
            {
                files = MakeNewDbFiles(dbName, backupPath);
                ext = "FILE = 1, NOUNLOAD, STATS = 10";
            }
            else
            {
                ext = "REPLACE";
            }
            var isOffline = false;
            var sql = $"RESTORE DATABASE [{dbName}] FROM DISK = '{backupPath}' WITH " + ext;
            foreach (var f in files)
            {
                sql += $", MOVE '{f.LogicalName}' TO '{f.PhysicalName}'";
            }
            WriteFileOperation("Restoring database " + dbName + " from : ", Path.GetFileName(backupPath), false);

            if (files.Any())
            {
                var off = RunSql($"alter database [{dbName}] set offline with rollback immediate");
                isOffline = off.IsSuccess;
            }

            var res = RunSql(sql);

            if (res.IsSuccess)
            {
                WriteSuccess();
            }
            else
            {
                WriteFailed(null, res);
            }

            if (isOffline)
                RunSql($"alter database [{dbName}] set online");
            Console.WriteLine();
            return res;
        }
    }
}
