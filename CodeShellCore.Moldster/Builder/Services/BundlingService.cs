﻿using CodeShellCore.Cli;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Builder.Services
{
    public class BundlingService : MoldsterFileHandlingService, IBundlingService
    {
        public BundlingService(IServiceProvider provider) : base(provider)
        {
        }

        public virtual bool AddVToVersion { get { return true; } }

        public IOutputWriter OutputWriter { get { return Out; } set { Out = value; } }

        public string GetAppVersion(string code, bool uiIfLager = false)
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
            string mainScript = Path.Combine(Paths.UIRoot, "wwwroot\\dist", moduleName + "-" + bundleVersion + ".js");
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

            string args = $"run build {moduleName} --config production";
            var p = GetCommandProcess(Paths.UIRoot, "npm", args);
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
            string args = "install";
            RunCommand(Paths.UIRoot, "npm", args, true);
        }


    }
}