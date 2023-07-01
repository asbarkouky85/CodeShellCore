using CodeShellCore.Cli;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Builder.Services
{
    public class BundlingService : MoldsterFileHandlingService, IBundlingService, ILegacyBundlingService
    {
        public BundlingService(IServiceProvider provider) : base(provider)
        {
        }

        public virtual bool AddVToVersion { get { return true; } }

        public IOutputWriter OutputWriter { get { return Out; } set { Out = value; } }

        public virtual string GetAppVersion(string code, bool uiIfLager = false)
        {
            string ver = Data.GetAppVersion(code);
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

        public virtual string GetNextVersionNumber(string ver)
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

        public virtual bool StartProductionPackIfNeeded(string tenantCode, out BundlingTask tt, string version = null)
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



        public virtual bool IsBundled(string moduleName, string version)
        {
            string bundleFolder = Names.GetOutputBundlePath(moduleName, version, true);
            if (File.Exists(bundleFolder))
            {
                Out.WriteLine($"Version {version} is already bundled for {moduleName}");
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
            var projectName = Names.ApplyConvension(moduleName, AppParts.Project);
            string args = $"node_modules/@angular/cli/bin/ng build {projectName} --configuration production --output-path {Names.GetOutputPath(moduleName, version)}";
            var p = GetCommandProcess(Paths.UIRoot, "node", args);
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
            CompressModuleBundle(moduleName, version);
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

        public virtual string CompressModuleBundle(string tenant, string version)
        {
            string bundleFolder = Names.GetOutputPath(tenant, version, true);
            string bundleFile = Names.GetOutputBundlePath(tenant, version, true);

            Out.Write("Compressing scripts [");
            WriteColored(tenant, ConsoleColor.Yellow);
            Out.Write("] for version [");
            WriteColored(version, ConsoleColor.Cyan);
            Out.Write("]...");

            if (!File.Exists(bundleFile))
            {
                FileUtils.CompressDirectory(bundleFolder, bundleFile, true);
                WriteSuccess();
            }
            else
            {
                GotoColumn(SuccessCol);
                WriteColored("EXISTS", ConsoleColor.DarkCyan);
            }

            Out.WriteLine();
            return bundleFolder + ".zip";
        }

        public virtual SubmitResult UpdateTenantVersionInDataSource(string code, string version)
        {

            Out.Write("Updating [" + code + "] to version [" + version + "]");
            var res = Data.SetAppVersion(code, version);
            WriteSuccess();
            Out.WriteLine();
            return res;
        }



        public virtual string GetUIVersion()
        {
            var files = Directory.GetFiles(Paths.UIRoot, "*.csproj");
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
            string args = "install --force";
            RunCommand(Paths.UIRoot, "npm", args, true);
        }

        public virtual void WriteWebpackConfigFiles()
        {

        }
    }
}