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

namespace CodeShellCore.Moldster.Builder
{
    public class BundlingService : MoldsterFileHandlingService, IBundlingService, ILegacyBundlingService
    {
        public BundlingService(IServiceProvider provider) : base(provider)
        {
        }

        public virtual bool AddVToVersion { get { return true; } }

        public IOutputWriter OutputWriter { get { return Out; } set { Out = value; } }

        public virtual async Task<string> GetAppVersion(string code, bool uiIfLager = false)
        {
            string ver = await Data.GetAppVersion(code);
            if (string.IsNullOrEmpty(ver))
            {
                return await GetUIVersion();
            }
            else if (uiIfLager)
            {
                return await GetNextVersionNumber(ver);
            }
            return ver;
        }

        public virtual async Task<string> GetNextVersionNumber(string ver)
        {
            string ui = await GetUIVersion();
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
            var t = GetAppVersion(tenantCode, true);
            t.Wait();
            var v = version ?? t.Result;

            var t2 = IsBundled(tenantCode, v);
            t2.Wait();
            if (t2.Result)
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

            tsk.Task = Task.Run(() =>
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
                var t = tsk.OnComplete?.Invoke(res);
                t.Wait();
            });
            tsk.Task.Start();
            tt = tsk;
            return true;
        }



        public virtual async Task<bool> IsBundled(string moduleName, string version)
        {
            string bundleFolder = Names.GetOutputBundlePath(moduleName, version, true);
            if (File.Exists(bundleFolder))
            {
                Out.WriteLine($"Version {version} is already bundled for {moduleName}");
                await UpdateTenantVersionInDataSource(moduleName, version);
                return true;
            }
            return false;
        }

        public virtual async Task<Result> ProductionPack(string moduleName, string version = null, bool trace = false)
        {
            version = version ?? await GetAppVersion(moduleName, true);
            if (await IsBundled(moduleName, version))
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
            await CompressModuleBundle(moduleName, version);
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

        public virtual Task<string> CompressModuleBundle(string tenant, string version)
        {
            return Task.Run(() =>
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
            });
        }

        public virtual async Task<SubmitResult> UpdateTenantVersionInDataSource(string code, string version)
        {

            Out.Write("Updating [" + code + "] to version [" + version + "]");
            var res = await Data.SetAppVersion(code, version);
            WriteSuccess();
            Out.WriteLine();
            return res;
        }



        public virtual Task<string> GetUIVersion()
        {
            return Task.Run(() =>
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
            });
        }

        public virtual async Task PrepEnvironment(bool prod = false)
        {
            string args = "install --force";
            await RunCommand(Paths.UIRoot, "npm", args, true);
        }

        public virtual Task WriteWebpackConfigFiles()
        {
            return Task.CompletedTask;
        }
    }
}