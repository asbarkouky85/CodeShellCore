using CodeShellCore.Cli;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Cli
{
    public class SqlConsoleController : ConsoleController
    {
        ISqlCommandService service => GetService<ISqlCommandService>();
        IPathsService paths => GetService<IPathsService>();
        EnvironmentAccessor accessor => GetService<EnvironmentAccessor>();
        IConfigUnit unit => GetService<IConfigUnit>();
        ITenantService tenants => GetService<ITenantService>();

        public override Dictionary<int, string> Functions => new Dictionary<int, string>
        {
            { 1, "CreateTenantDatabase" },
            { 2, "UpdateDatabase" },
            { 3, "RunInitiationScripts"},

        };


        MoldsterEnvironment SelectEnvironment()
        {
            while (true)
            {
                var envs = paths.GetEnvironments();

                var selected = GetSelectionFromUser("Environment ", envs.Select(d => d.Name).ToArray());
                if (selected == null)
                    return null;
                accessor.CurrentEnvironment = envs.Where(d => d.Name == selected).FirstOrDefault();
                return accessor.CurrentEnvironment;
            }

        }

        public void CreateTenantDatabase()
        {
            while (true)
            {

                var selected = SelectEnvironment();
                if (selected == null)
                    break;

                var env = selected;
                while (true)
                {
                    var id = GetIntFromUser("Enter id");
                    if (id == 0)
                        break;
                    if (unit.TenantRepository.Exist(d => d.Id.Equals(id)))
                    {
                        writer.WriteExceptionShort("Id exists already", "Dubplicate");
                        continue;
                    }

                    var code = GetStringFromUser("Enter Code");
                    if (string.IsNullOrEmpty(code))
                        break;

                    if (unit.TenantRepository.Exist(d => d.Code.Equals(code)))
                    {
                        writer.WriteExceptionShort("code already exists", "Dubplicate");
                        continue;
                    }
                    var def = $"{ paths.CoreAppName }.{ code}";
                    var dbName = GetStringFromUser($"Enter db name [{def}]");
                    if (string.IsNullOrEmpty(dbName))
                        dbName = def;

                    service.CommandTimeout = 90;
                    service.CreateTenantDatabase(id, code, dbName);

                    Console.Write("Adding tenant to config database...");

                    var res = tenants.Create(new Tenant
                    {
                        Id = id,
                        Code = code,
                        Name = code,
                        MainComponentBase = "AppComponent",
                        ConnectionString = $"Server={{DEFAULT}};User Id={env.ConnectionParams.UserId};Password={env.ConnectionParams.Password};Database={dbName}"
                    });
                    if (res.IsSuccess)
                    {
                        writer.WriteSuccess();
                    }
                    else
                    {
                        writer.WriteException(res.ExceptionMessage, res.Message, res.StackTrace);
                    }
                    Console.WriteLine();
                }
            }

        }

        public void RunInitiationScripts()
        {
            while (true)
            {
                var envs = paths.GetEnvironments();

                var selected = GetSelectionFromUser("Environment ", envs.Select(d => d.Name).ToArray());
                if (selected == null)
                    break;
                var env = envs.Where(d => d.Name == selected).FirstOrDefault();
                accessor.CurrentEnvironment = env;

                var dbs = new List<string>();
                dbs.AddRange(env.Databases);

                while (true)
                {
                    var db = GetSelectionFromUser("Database", dbs.ToArray());
                    if (db == null)
                        break;
                    service.AfterComparisonInitiation(0, null, db);
                }
            }
        }

        public void UpdateDatabase()
        {
            while (true)
            {
                var envs = paths.GetEnvironments();

                var selected = GetSelectionFromUser("Environment ", envs.Select(d => d.Name).ToArray());
                if (selected == null)
                    break;
                var env = envs.Where(d => d.Name == selected).FirstOrDefault();
                accessor.CurrentEnvironment = env;

                var dbs = new List<string>();
                dbs.Add("All");
                dbs.AddRange(env.Databases);

                while (true)
                {
                    var db = GetSelectionFromUser("Database", dbs.ToArray());
                    if (db == null)
                        break;
                    if (db == "All")
                    {
                        foreach (var dbb in env.Databases)
                            service.UpdateDatabase(dbb);
                    }
                    else
                        service.UpdateDatabase(db);
                }

            }

        }
    }
}
