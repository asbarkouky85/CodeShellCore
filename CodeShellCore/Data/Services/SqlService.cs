using CodeShellCore.Cli;
using CodeShellCore.CLI;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CodeShellCore.Data.Services
{
    public class SqlService : ConsoleService
    {
        public virtual int CommandTimeout { get; set; }
        protected virtual DbConnectionParams ConnectionParams { get; }
        public override int SuccessCol => 10;

        public SqlService(IOutputWriter writer) : base(writer)
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

        //public Process SqlCmdQueryProcess(string query)
        //{

        //    if (ConnectionParams == null)
        //        throw new Exception("Environment is null");
        //    var param = ConnectionParams;

        //    var db = "";
        //    if (!string.IsNullOrEmpty(param.Database))
        //        db = "-d " + param.Database;

        //    var cmd = "sqlcmd";
        //    var args = $"-U {param.UserId} -P {param.Password} -S {param.Server} {db} -Q \"{query}\"";
        //    try
        //    {
        //        var p = new Process();
        //        p.StartInfo = new ProcessStartInfo
        //        {
        //            FileName = cmd,
        //            Arguments = args,
        //            UseShellExecute = false,
        //            RedirectStandardOutput = true
        //        };
        //        p.Start();
        //        return p;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        protected IEnumerable<DatabaseFile> GetDatabaseFiles(string dbName)
        {
            try
            {
                var q = "select name,physical_name from sys.database_files";
                var files = GetDataAs<DatabaseFile>(q, dbName);
                return files;
            }
            catch
            {
                return new List<DatabaseFile>();
            }

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
                    Out.WriteLine("Batch failed");
                    Out.WriteLine(sql);
                    using (Out.Set(ConsoleColor.Red))
                        Out.WriteLine(ex.GetMessageRecursive());
                    conn.Close();
                    throw;
                }
            }
        }

        protected SubmitResult ExecuteBatchNonQuery(string sql, string connectionString)
        {
            string sqlBatch = string.Empty;
            sql += "\nGO";   // make sure last batch is executed.

            SubmitResult res = new SubmitResult();
            foreach (string line in sql.Split(new string[2] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
            {

                if (line.ToUpperInvariant().Trim() == "GO" && !string.IsNullOrEmpty(sqlBatch))
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {

                            conn.Open();
                            SqlCommand cmd = new SqlCommand(sqlBatch, conn);
                            if (CommandTimeout != 0)
                                cmd.CommandTimeout = CommandTimeout;

                            res.AffectedRows = cmd.ExecuteNonQuery();

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

        public SubmitResult RunSql(string script, string db = null)
        {
            ConnectionParams.Database = db;
            return ExecuteBatchNonQuery(script, ConnectionParams.ConnectionString);
        }

        public SubmitResult RestoreDatabase(string dbName, string backupPath)
        {
            var files = GetDatabaseFiles(dbName);
            var isOffline = false;
            var sql = $"RESTORE DATABASE [{dbName}] FROM DISK = '{backupPath}' WITH REPLACE";
            foreach (var f in files)
            {
                sql += $", MOVE '{f.name}' TO '{f.physical_name}'";
            }
            WriteFileOperation("Restoring database " + dbName, backupPath);

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
            return res;
        }
    }
}
