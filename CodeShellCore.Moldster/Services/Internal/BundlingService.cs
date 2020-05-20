using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Cli;
using CodeShellCore.CLI;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Models;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class BundlingService : ConsoleService, IBundlingService
    {
        private readonly IPathsService _paths;
        private readonly WriterService _writer;
        private readonly IMoldProvider _molds;
        private readonly IDataService _data;



        public BundlingService(

            IPathsService paths,
            WriterService writer,
            IMoldProvider molds,
            IDataService data,
            IOutputWriter output) : base(output)
        {
            _paths = paths;
            _writer = writer;
            _molds = molds;
            _data = data;

        }

        public virtual bool AddVToVersion { get { return true; } }

        public IOutputWriter OutputWriter { get { return Out; } set { Out = value; } }

        #region environment

        public void AddSQLFiles()
        {
            var sql = Properties.Resources.Creation;
            var folder = Path.Combine(_paths.ConfigRoot, "SQL");
            var createPath = Path.Combine(folder, "Creation.sql");
            Utils.CreateFolderForFile(createPath);

            WriteFileOperation("Adding SQL Update files", folder);
            File.WriteAllText(createPath, sql);
            File.WriteAllText(Path.Combine(folder, "update-v2.1.0.sql"), Properties.Resources.update_v2_1_0);
            File.WriteAllText(Path.Combine(folder, "update-v2.2.0.sql"), Properties.Resources.update_v2_2_0);
            File.WriteAllText(Path.Combine(folder, "update-v2.5.0.sql"), Properties.Resources.update_v2_5_0);
            File.WriteAllText(Path.Combine(folder, "update-v2.6.0.sql"), Properties.Resources.update_v2_6_0);
            File.WriteAllText(Path.Combine(folder, "update-v2.8.0.sql"), Properties.Resources.update_v2_8_0);
            File.WriteAllText(Path.Combine(folder, "update-v2.10.3.sql"), Properties.Resources.update_v2_10_3);

            Out.WriteLine();
        }

        void BaseModuleFiles()
        {
            string path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "AppComponentBase.ts");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [AppComponentBase.ts]");
                File.WriteAllText(path, Properties.Resources.AppComponentBase_ts);
            }

            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "ServerConfig.ts");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [ServerConfig.ts]");
                File.WriteAllText(path, Properties.Resources.ServerConfig_ts);
            }
            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "AppComponentBase.ts");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [AppComponentBase.ts]");
                File.WriteAllText(path, Properties.Resources.AppComponentBase_ts);
            }

            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "Main/Login.ts");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [Login.ts]");
                File.WriteAllText(path, Properties.Resources.Login_ts);
            }

            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "Main/Login.html");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [Login.html]");
                File.WriteAllText(path, Properties.Resources.Login_html);
            }
            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "Main/topBar.html");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [topBar.html]");
                File.WriteAllText(path, Properties.Resources.topBar_html);
            }
            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "Main/topBar.ts");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [topBar.ts]");
                File.WriteAllText(path, Properties.Resources.topBar_ts);
            }
            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "Main/navigationSideBar.html");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [navigationSideBar.html]");
                File.WriteAllText(path, Properties.Resources.navigationSideBar_html);
            }
            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "Http/AccountService.ts");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [AccountService.ts]");
                File.WriteAllText(path, Properties.Resources.AccountService_ts);
            }
            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, _paths.CoreAppName + "BaseModule.ts");
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [" + _paths.CoreAppName + "BaseModule.ts]");
                string content = _writer.FillStringParameters(_molds.BaseModuleMold, new DomainTsModel { Name = _paths.CoreAppName + "Base" });
                File.WriteAllText(path, content);
            }
        }

        public void GenerateEnvironment()
        {
            string path = Path.Combine(_paths.UIRoot, "declarations.d.ts");

            AddSQLFiles();
            BaseModuleFiles();
            UnZip(Properties.Resources.ShellComponents, _paths.ConfigRoot, "ShellComponents");

            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [declarations.d.ts]");
                File.WriteAllText(path, Properties.Resources.declarations_d);
            }

            path = Path.Combine(_paths.UIRoot, "package.json");
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [package.json]");
                File.WriteAllText(path, Properties.Resources.package_json);
            }

            path = Path.Combine(_paths.UIRoot, "tsconfig.json");
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [tsconfig.json]");
                File.WriteAllText(path, Properties.Resources.tsconfig_json);
            }

            path = Path.Combine(_paths.UIRoot, "WebPackSharedConfig.js");
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [WebPackSharedConfig.js]");
                File.WriteAllText(path, Properties.Resources.WebPackSharedConfig_js);
            }

            path = Path.Combine(_paths.UIRoot, "webpack.config.vendor.js");
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [webpack.config.vendor.js]");
                File.WriteAllText(path, Properties.Resources.webpack_config_vendor_js);
            }

            

            path = Path.Combine(_paths.UIRoot, "appsettings.development.json");
            if (!File.Exists(path))
            {
                Out.WriteLine("Adding file [appsettings.development.json]");
                File.WriteAllText(path, Properties.Resources.appsettings_json);
            }

            path = Path.Combine(_paths.ConfigRoot, "Views/AppComponent.cshtml");
            if (!File.Exists(path))
            {
                Utils.CreateFolderForFile(path);
                Out.WriteLine("Adding file [AppComponent.cshtml]");
                File.WriteAllText(path, Properties.Resources.AppComponent_cshtml);

            }

            path = Path.Combine(_paths.UIRoot, "Pages/Index.cshtml");
            if (!File.Exists(path))
            {
                Utils.CreateFolderForFile(path);
                Out.WriteLine("Adding file [Index.cshtml]");
                File.WriteAllText(path, Properties.Resources.Index_cshtml);
            }

        }
        #endregion

        public void GenerateDevWebPackFiles(IEnumerable<string> modules, IEnumerable<string> activeMods = null)
        {
            IEnumerable<string> apps = modules;
            activeMods = activeMods ?? apps;
            string packTemplate = _molds.DevWebpackConfigMold;
            Out.Write("Generating webpack.config.js\t");
            WebPackModel mod = new WebPackModel();
            string sep = "";
            foreach (string app in activeMods)
            {
                mod.Tenants += sep + $"\"{app}\" : \"./{app}/boot.ts\",\r";
                sep = "\t\t\t";
            }
            string contents = _writer.FillStringParameters(packTemplate, mod);
            string packPath = Path.Combine(_paths.UIRoot, "webpack.config.js");
            File.WriteAllText(packPath, contents);
            WriteSuccess(null);
            Out.WriteLine();

        }

        public void GenerateWebPackFiles(string code, IEnumerable<string> others, bool lazy)
        {
            string packTemplate = _molds.ProWebpackConfigMold;
            string jsonTemplate = _molds.ModuleTsConfigMold;

            WebPackModel mod = new WebPackModel
            {
                Code = code,
                Tenants = "",
                Lazy = lazy ? "" : ""
            };

            foreach (var t in others)
                mod.Tenants += $"\"{t}\",\r";

            string packContents = _writer.FillStringParameters(packTemplate, mod);
            string jsonContents = _writer.FillStringParameters(jsonTemplate, mod);

            string packPath = $"webpack.{code}.js";
            string jsonPath = $"webpack.{code}.js.tsc";

            Out.Write("Generating " + packPath + "\t");
            File.WriteAllText(Path.Combine(_paths.UIRoot, packPath), packContents);
            WriteSuccess(null);
            Out.WriteLine();

            Out.Write("Generating " + jsonPath + "\t");
            File.WriteAllText(Path.Combine(_paths.UIRoot, jsonPath), jsonContents);
            WriteSuccess(null);
            Out.WriteLine();

        }

        public void AddCodeShell()
        {
            string folder = Path.Combine(_paths.UIRoot, "Core");
            UnZip(Properties.Resources.codeshell, folder, "codeshell");
        }

        public virtual void AddStaticFiles()
        {
            string folder = Path.Combine(_paths.UIRoot, "wwwroot");
            UnZip(Properties.Resources.css, folder, "css");
            UnZip(Properties.Resources.img, folder, "img");
            UnZip(Properties.Resources.js, folder, "js");
        }

        void UnZip(byte[] bytes, string folder, string name, bool overwrite = false)
        {
            using (var t = SW.Measure())
            {
                string file = Path.Combine(folder, $"{name}.zip");
                string folderPath = Path.Combine(folder, name);
                bool write = true;

                if (Directory.Exists(folderPath))
                {
                    if (overwrite && Utils.DeleteDirectory(folderPath))
                    {
                        write = true;
                    }
                    else
                    {
                        Out.WriteLine(folderPath + " already exists");
                        return;
                    }
                }

                if (write)
                {
                    WriteFileOperation($"Extracting {name} to", folder, false);
                    Utils.CreateFolderForFile(file);
                    File.WriteAllBytes(file, bytes);
                    ZipFile.ExtractToDirectory(file, folderPath);
                    File.Delete(file);
                    WriteSuccess(t.Elapsed, SuccessCol);
                }
                Out.WriteLine();
            }
        }


        public virtual void WriteWebpackConfigFiles(bool lazy)
        {
            using (var x = SW.Measure())
            {
                string[] modules = _data.GetAppCodes();
                string[] act = _data.GetAppCodes(true);
                GenerateDevWebPackFiles(modules, act);
                foreach (var t in modules)
                {
                    string[] others = modules.Where(d => d != t).ToArray();
                    GenerateWebPackFiles(t, others, lazy);
                }
            }

        }

        public virtual void DevelopmentPack()
        {
            string args = $"--max_old_space_size=8138 node_modules/webpack/bin/webpack.js --progress";
            RunCommand(_paths.UIRoot, "node", args);
        }

        public string GetAppVersion(string code, bool uiIfLager = false)
        {
            string ver = _data.GetAppVersion(code);
            if (string.IsNullOrEmpty(ver))
            {
                return GetUIVersion();
            }
            else if (uiIfLager)
            {
                return GetNextVersionNumber(ver);
            }
            return ver;
        }



        public string GetNextVersionNumber(string ver)
        {
            string ui = GetUIVersion();
            if (Utils.CompareVersions(ui, ver) == 1)
            {
                return ui;
            }
            else
            {
                return ver;
            }
        }

        public bool StartProductionPackIfNeeded(string tenantCode, out BundlingTask tt, string version = null)
        {
            var v = version ?? GetAppVersion(tenantCode, true);

            if (IsBundled(tenantCode, v))
            {
                tt = null;
                return false;
            }
            var tsk = new BundlingTask()
            {
                TenantCode = tenantCode,
                Version = v,
                StartedOn = DateTime.Now,
                Status = "Active"
            };

            tsk.Task = new Task<Result>(() =>
            {
                using (var sc = Shell.GetScope())
                {
                    var ser = sc.ServiceProvider.GetService<IBundlingService>();
                    ser.OutputWriter = Out;
                    return ser.ProductionPack(tenantCode, v, true);
                }
            });

            tsk.Task.GetAwaiter().OnCompleted(() =>
            {
                var res = tsk.Task.Result;
                tsk.Status = res.IsSuccess ? "Successfull" : "Failed";
                tsk.CompletedOn = DateTime.Now;
                tsk.Message = res.Message;
                tsk.OnComplete?.Invoke(tsk, res);
            });
            tsk.Task.Start();
            tt = tsk;
            return true;
        }

        public bool IsBundled(string moduleName, string version)
        {
            string bundleVersion = AddVToVersion ? "v" + version : version;
            string mainScript = Path.Combine(_paths.UIRoot, "wwwroot\\dist", moduleName + "-" + bundleVersion + ".js");
            if (File.Exists(mainScript))
            {
                Out.WriteLine($"Version {bundleVersion} is already bundled for {moduleName}");
                UpdateTenantVersionInDataSource(moduleName, version);
                return true;
            }
            return false;
        }

        public virtual Result ProductionPack(string moduleName, string version = null, bool trace = false)
        {
            version = version ?? GetAppVersion(moduleName, true);
            if (IsBundled(moduleName, version))
            {
                return new Result { Code = 0, Message = "No Changes" };
            }

            string name = $"webpack.{moduleName}.js";

            string args = $"--max_old_space_size=8138 node_modules/webpack/bin/webpack.js --config {name} --env.prod --env.version={version} --debug --progress";
            var p = GetCommandProcess(_paths.UIRoot, "node", args);
            p.StartInfo.RedirectStandardOutput = trace;
            p.Start();
            if (trace)
            {

                while (!p.StandardOutput.EndOfStream)
                {
                    Out.WriteLine(p.StandardOutput.ReadLine());
                }
            }
            p.WaitForExit();

            var code = p.ExitCode;
            var res = new Result { Code = code, Message = code == 0 ? "bundling_successful" : "bundling_failed" };
            if (res.IsSuccess)
            {
                res.Data["UpdateVersion"] = UpdateTenantVersionInDataSource(moduleName, version);
            }
            else
            {
                res.Message = "Bundling failed";
                WriteFailed(null, res);
            }
            return res;

        }

        public SubmitResult UpdateTenantVersionInDataSource(string code, string version)
        {

            Out.Write("Updating [" + code + "] to version [" + version + "]");
            var res = _data.SetAppVersion(code, version);
            WriteSuccess();
            Out.WriteLine();
            return res;
        }



        public virtual string GetUIVersion()
        {
            var files = Directory.GetFiles(_paths.UIRoot, "*.csproj");
            if (files.Length > 0)
            {
                string contents = File.ReadAllText(files[0]);
                var ver = contents.FindXmlValue("AssemblyVersion");
                if (ver != null)
                    return ver;
            }
            return "1.0.0.0";
        }

        public virtual void PrepEnvironment(bool prod = false)
        {
            string args = "install";
            RunCommand(_paths.UIRoot, "npm", args, true);
            args = "node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js --progress";
            RunCommand(_paths.UIRoot, "node", args);
            if (prod)
            {
                args = "node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js --env.prod --progress";
                RunCommand(_paths.UIRoot, "node", args);
            }

        }


    }
}
