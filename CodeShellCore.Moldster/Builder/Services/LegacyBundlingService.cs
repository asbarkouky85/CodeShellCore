using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            string args = "install --force";
            RunCommand(Paths.UIRoot, "npm", args, true);
            args = "node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js --progress";
            RunCommand(Paths.UIRoot, "node", args);
            if (prod)
            {
                args = "node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js --env.prod --progress";
                RunCommand(Paths.UIRoot, "node", args);
            }
        }

        public void GenerateDevWebPackFiles(IEnumerable<string> modules, IEnumerable<string> activeMods = null)
        {
            IEnumerable<string> apps = modules;
            activeMods = activeMods ?? apps;
            string packTemplate = Molds.GetResourceByNameAsString(MoldNames.WebPackConfig_js);
            Out.Write("Generating webpack.config.js\t");
            WebPackModel mod = new WebPackModel();
            string sep = "";
            foreach (string app in activeMods)
            {
                mod.Tenants += sep + $"\"{app}\" : \"./{app}/boot.ts\",\r";
                sep = "\t\t\t";
            }
            string contents = Writer.FillStringParameters(packTemplate, mod);
            string packPath = Path.Combine(Paths.UIRoot, "webpack.config.js");
            File.WriteAllText(packPath, contents);
            WriteSuccess(null);
            Out.WriteLine();

        }

        public void GenerateWebPackFiles(string code, IEnumerable<string> others, bool lazy)
        {
            string packTemplate = Molds.GetResourceByNameAsString(MoldNames.WebPackTenantConfig_js);
            string jsonTemplate = Molds.GetResourceByNameAsString(MoldNames.WebPackTenantJs_json);

            WebPackModel mod = new WebPackModel
            {
                Code = code,
                Tenants = "",
                Lazy = lazy ? "" : ""
            };

            foreach (var t in others)
                mod.Tenants += $"\"{t}\",\r";

            string packContents = Writer.FillStringParameters(packTemplate, mod);
            string jsonContents = Writer.FillStringParameters(jsonTemplate, mod);

            string packPath = $"webpack.{code}.js";
            string jsonPath = $"webpack.{code}.js.tsc";

            Out.Write("Generating " + packPath + "\t");
            File.WriteAllText(Path.Combine(Paths.UIRoot, packPath), packContents);
            WriteSuccess(null);
            Out.WriteLine();

            Out.Write("Generating " + jsonPath + "\t");
            File.WriteAllText(Path.Combine(Paths.UIRoot, jsonPath), jsonContents);
            WriteSuccess(null);
            Out.WriteLine();

        }

        public override void WriteWebpackConfigFiles()
        {
            using (var x = SW.Measure())
            {
                string[] modules = Data.GetAppCodes();
                string[] act = Data.GetAppCodes(true);
                GenerateDevWebPackFiles(modules, act);
                foreach (var t in modules)
                {
                    string[] others = modules.Where(d => d != t).ToArray();
                    GenerateWebPackFiles(t, others, true);
                }
            }

        }
    }
}
