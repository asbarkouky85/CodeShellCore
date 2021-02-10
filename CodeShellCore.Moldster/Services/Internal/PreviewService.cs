using CodeShellCore.Cli;
using CodeShellCore.CLI;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class PreviewService : ConsoleService, IPreviewService
    {
        private readonly IPathsService paths;
        private readonly IDataService data;
        public static PreviewTask Current { get; protected set; }

        public virtual PreviewTask CurrentPreview => Current;

        public PreviewService(IOutputWriter writer, IPathsService paths, IDataService data) : base(writer)
        {
            this.paths = paths;
            this.data = data;
        }

        protected static string WebpackConfigTemplate => @"const merge = require('webpack-merge');
const WebpackSharedConfig = require(""./WebpackSharedConfig"");

module.exports = (env) => {

    var sharedConfig = WebpackSharedConfig({ prod: false });

    var config = merge(sharedConfig, {
        entry: {
            ""%TenantCode%"" : ""./%TenantCode%/boot.ts"",
        }
    })
    return config;
};";

        public virtual string BundleFolder => "wwwroot/dist";



        protected virtual void DeleteFolder(string path, string display = null)
        {
            WriteFileOperation("Deleting", display ?? path, false);
            var fls = Directory.GetFiles(path);
            foreach (var f in fls)
                File.Delete(f);
            Directory.Delete(path, true);
            WriteSuccess(null, SuccessCol);
            Out.WriteLine();
        }

        public virtual Result StartPreview(string tenantCode, string launchProfile = null)
        {
            var res = new Result();
            if (Current != null)
                StopPreview();
            try
            {
                var folder = Path.Combine(paths.UIRoot, tenantCode);
                if (!Directory.Exists(folder))
                    return new Result { Code = 1, Message = "render_tenant_first" };

                var otherApps = data.GetAppCodes().Where(d => d != tenantCode);
                Out.WriteLine("Deleting other tenant files");
                foreach (var app in otherApps)
                {
                    var p = Path.Combine(paths.UIRoot, app);
                    if (Directory.Exists(p))
                    {
                        DeleteFolder(p, app);
                    }
                }
                var dev = Path.Combine(paths.UIRoot, BundleFolder, "dev");
                if (Directory.Exists(dev))
                    DeleteFolder(dev, "Development bundle");

                var configString = WebpackConfigTemplate.Replace("%TenantCode%", tenantCode);
                var configPath = Path.Combine(paths.UIRoot, "webpack.config.js");
                Out.Write("Changing configuration");
                File.WriteAllText(configPath, configString);
                WriteSuccess(null, SuccessCol);

                Current = new PreviewTask { TenantCode = tenantCode };
                var t = RunPreviewAsync(Current, launchProfile);
                t.Start();
                Current.WaitForStartResult();

                if (Current.IsStarted)
                {
                    var url = Utils.CombineUrl(paths.UIUrl, Current.TenantCode);
                    Out.WriteLine("Preview started on :" + url);
                    res.Message = "tenant_preview_created";
                    res.Data["Url"] = url;
                }
                else
                {
                    res.Code = 1;
                    Out.WriteLine("Preview failed to start");
                    res.Message = "failed_to_start";
                    Current = null;
                }
            }
            catch (Exception ex)
            {
                res.SetException(ex);
            }
            return res;
        }

        protected virtual Task RunPreviewAsync(PreviewTask tsk, string launchProfile = null)
        {
            return new Task(() =>
            {
                RunPreview(tsk, launchProfile);
            });
        }

        protected virtual void RunPreview(PreviewTask tsk, string launchProfile = null)
        {
            try
            {
                var arg = launchProfile == null ? "" : $"--launch-profile {launchProfile}";
                var process = GetCommandProcess(paths.UIRoot, "dotnet", $"-d run {arg} --no-build");

                //process.EnableRaisingEvents = true;
                process.StartInfo.RedirectStandardOutput = true;
                // process.StartInfo.RedirectStandardError = true;

                process.Start();


                var outReader = process.StandardOutput;
                //var errReader = process.StandardError;

                while (!outReader.EndOfStream)//&& !errReader.EndOfStream)
                {
                    var outLine = outReader.ReadLine();
                    //var errLine = errReader.ReadLine();

                    if (outLine != null)
                    {
                        Out.WriteLine(outLine);
                        if (outLine.GetPatternContents("Process ID: (.*)", out string[] data))
                            tsk.Process = data[0];

                        if (outLine.Contains("Application started."))
                            tsk.IsStarted = true;
                    }

                    //if (errLine != null)
                    //{
                    //    using (Out.Set(ConsoleColor.Red))
                    //    {
                    //        Out.WriteLine(errLine);
                    //    }
                    //}

                    if (tsk.Process != null && tsk.IsStarted)
                        break;
                }
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    tsk.FailedToStart = true;
                }
                else
                {
                    tsk.IsStarted = true;
                }
            }
            catch (Exception ex)
            {
                using (Out.Set(ConsoleColor.Red))
                {
                    Out.WriteLine(ex.GetMessageRecursive());
                }
                tsk.FailedToStart = true;
                throw;
            }
        }

        protected virtual Task RunWebpackWatcher(PreviewTask tsk, string launchProfile = null)
        {
            return new Task(() =>
            {
                try
                {

                    var process = GetCommandProcess(paths.UIRoot, "node", $"node_modules/webpack/bin/webpack.js --watch");

                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.RedirectStandardOutput = true;

                    Out.WriteLine("starting");
                    process.Start();
                    tsk.Process = process.Id.ToString();
                    var re = process.StandardError;
                    var rd = process.StandardOutput;
                    Out.WriteLine("Reading output");
                    while (!rd.EndOfStream && !re.EndOfStream)
                    {
                        var e = re.ReadLine();
                        var l = rd.ReadLine();
                        if (e != null)
                        {
                            Out.WriteLine(e);
                            if (e.Contains("watching"))
                                tsk.IsStarted = true;
                        }

                        if (l != null)
                        {
                            Out.WriteLine(l);
                            if (l.Contains("watching"))
                                tsk.IsStarted = true;
                        }
                        if (tsk.IsStarted)
                            break;

                    }
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        Out.WriteLine("Exit code = " + process.ExitCode);
                        tsk.FailedToStart = true;
                    }
                }
                catch (Exception ex)
                {
                    Out.WriteLine(ex.Message);
                    tsk.FailedToStart = true;
                    WriteException(ex);
                }
            });
        }

        public virtual Result StopPreview()
        {
            if (Current != null)
            {
                if (int.TryParse(Current.Process, out int processId))
                {
                    WriteFileOperation("Stopping current preview with process", processId.ToString(), false);
                    Process p = Process.GetProcessById(processId);
                    p.Kill();
                    p.WaitForExit();
                    WriteSuccess();
                    Out.WriteLine();
                }
            }
            Current = null;
            return new Result { Message = "preview_stop" };
        }
    }
}
