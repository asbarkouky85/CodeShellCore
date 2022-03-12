using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Moldster.Builder.Services
{
    public class LegacyBundlingService : BundlingService
    {
        public LegacyBundlingService(IServiceProvider provider) : base(provider)
        {
        }

        public override bool IsBundled(string moduleName, string version)
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

        public override Result ProductionPack(string moduleName, string version = null, bool trace = false)
        {
            version = version ?? GetAppVersion(moduleName, true);
            if (IsBundled(moduleName, version))
            {
                return new Result { Code = 0, Message = "No Changes" };
            }

            string name = $"webpack.{moduleName}.js";

            string args = $"--max_old_space_size=8138 node_modules/webpack/bin/webpack.js --config {name} --env.prod --env.version={version} --debug --progress";
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

        public override void PrepEnvironment(bool prod = false)
        {
            string args = "install";
            RunCommand(Paths.UIRoot, "npm", args, true);
            args = "node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js --progress";
            RunCommand(Paths.UIRoot, "node", args);
            if (prod)
            {
                args = "node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js --env.prod --progress";
                RunCommand(Paths.UIRoot, "node", args);
            }
        }
    }
}
