using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Cli;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Models;
using CodeShellCore.Services;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.Data;

namespace CodeShellCore.Moldster.Builder.Internal
{
    public class BundlingService : ConsoleService, IBundlingService
    {
        private readonly IPathsService _paths;
        private readonly WriterService _writer;
        private readonly IMoldProvider _molds;
        private readonly IDataService _data;



        public BundlingService(

            IPathsService paths,
            IMoldProvider molds,
            IDataService data,
            IOutputWriter output) : base(output)
        {
            _paths = paths;
            _writer = new WriterService();
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

            File.WriteAllText(Path.Combine(folder, "update-v2.6.0.sql"), Properties.Resources.update_v2_6_0);
            File.WriteAllText(Path.Combine(folder, "update-v2.8.0.sql"), Properties.Resources.update_v2_8_0);
            File.WriteAllText(Path.Combine(folder, "update-v2.10.3.sql"), Properties.Resources.update_v2_10_3);
            File.WriteAllText(Path.Combine(folder, "update-v2.15.0.sql"), Properties.Resources.update_v2_15_0);

            Out.WriteLine();
        }

        void AddToBaseFolder(string name, string content, bool checkForFolder = false, bool replace = false)
        {
            var path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, name);
            if (checkForFolder)
                Utils.CreateFolderForFile(path);
            if (!File.Exists(path) || replace)
            {
                WriteFileOperation("Adding", name);
                File.WriteAllText(path, content);
            }
        }

        void AddToUI(string name, string content, bool replace = false)
        {
            var path = Path.Combine(_paths.UIRoot, name);
            if (!File.Exists(path) || replace)
            {
                WriteFileOperation("Adding", name);
                File.WriteAllText(path, content);
            }
        }

        void BaseModuleFiles(bool replace)
        {
            string content = _writer.FillStringParameters(_molds.BaseModuleMold, new DomainTsModel { Name = _paths.CoreAppName + "Base" });

            AddToBaseFolder(_paths.CoreAppName + "BaseModule.ts", content, true, replace);
            AddToBaseFolder("AppComponentBase.ts", Properties.Resources.AppComponentBase_ts, true, replace);
            AddToBaseFolder("ServerConfig.ts", Properties.Resources.ServerConfig_ts, replace);

            AddToBaseFolder("Main/Login.html", Properties.Resources.Login_html, true, replace);
            AddToBaseFolder("Main/Login.ts", Properties.Resources.Login_ts, false, replace);
            AddToBaseFolder("Main/topBar.html", Properties.Resources.topBar_html, false, replace);
            AddToBaseFolder("Main/topBar.ts", Properties.Resources.topBar_ts, false, replace);
            AddToBaseFolder("Main/navigationSideBar.html", Properties.Resources.navigationSideBar_html, false, replace);
            AddToBaseFolder("Main/navigationSideBar.ts", Properties.Resources.navigationSideBar_ts, false, replace);

            AddToBaseFolder("Http/AccountService.ts", Properties.Resources.AccountService_ts, true, replace);
        }

        public void AddShellComponents(bool replace)
        {
            UnZip(Properties.Resources.ShellComponents, _paths.ConfigRoot, "ShellComponents", replace);
        }

        public void GenerateEnvironment(bool replace)
        {
            string path = Path.Combine(_paths.UIRoot, "declarations.d.ts");

            AddSQLFiles();
            BaseModuleFiles(replace);

            AddToUI("package.json", Properties.Resources.package_json, replace);
            AddToUI("tsconfig.json", Properties.Resources.tsconfig_json, replace);
            AddToUI("declarations.d.ts", Properties.Resources.declarations_d, replace);
            AddToUI("WebPackSharedConfig.js", Properties.Resources.WebPackSharedConfig_js, replace);
            AddToUI("webpack.config.vendor.js", Properties.Resources.webpack_config_vendor_js, replace);
            AddToUI("appsettings.development.json", Properties.Resources.appsettings_json, replace);

            path = Path.Combine(_paths.UIRoot, "Pages/Index.cshtml");
            if (!File.Exists(path) || replace)
            {
                Utils.CreateFolderForFile(path);
                WriteFileOperation("Adding", "Index.cshtml", true);
                File.WriteAllText(path, Properties.Resources.Index_cshtml);
            }

            path = Path.Combine(_paths.ConfigRoot, "Views/AppComponent.cshtml");
            if (!File.Exists(path) || replace)
            {
                Utils.CreateFolderForFile(path);
                WriteFileOperation("Adding", "AppComponent.cshtml", true);
                File.WriteAllText(path, Properties.Resources.AppComponent_cshtml);
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

        public void AddCodeShell(bool replace)
        {
            string folder = Path.Combine(_paths.UIRoot, "Core");
            UnZip(Properties.Resources.codeshell, folder, "codeshell", replace);
        }

        public virtual void AddStaticFiles(bool replace)
        {
            string folder = Path.Combine(_paths.UIRoot, "wwwroot");
            UnZip(Properties.Resources.css, folder, "css", replace);
            UnZip(Properties.Resources.img, folder, "img", replace);
            UnZip(Properties.Resources.js, folder, "js", replace);
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
