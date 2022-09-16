using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Services;
using CodeShellCore.Types;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CodeShellCore.Moldster.Services
{
    public abstract class ScriptGenerationServiceBase : StandaloneConsoleService
    {
        protected MoldsterModuleOptions Options;
        protected WriterService Writer;

        private IMoldProvider _molds => Store.GetInstance<IMoldProvider>();
        private INamingConventionService _fileNameService => Store.GetInstance<INamingConventionService>();
        private IPathsService _paths => Store.GetInstance<IPathsService>();

        public ScriptGenerationServiceBase(
            IServiceProvider prov,
            IOptions<MoldsterModuleOptions> opt) : base(prov)
        {
            Store = new InstanceStore<object>(prov);
            Options = opt.Value;
            Writer = new WriterService();
        }
        public bool GenerateHttpService(string resource, string domain = null)
        {
            string folder = _fileNameService.GetHttpServiceFolder();

            if (domain != null)
            {
                domain = new Regex("^/").Replace(domain, "").Replace("/", "\\");
                folder = _fileNameService.GetHttpServiceFolder(domain);
            }

            string servicePath = Path.Combine(_paths.UIRoot, folder + "/" + _fileNameService.ApplyConvension(resource, AppParts.Service) + ".ts");
            Utils.CreateFolderForFile(servicePath);
            if (!File.Exists(servicePath))
            {
                string serviceTemplate = _molds.GetResourceByNameAsString(MoldNames.Service_ts);
                string service = Writer.FillStringParameters(serviceTemplate, new ServiceTsModel { Resource = resource, Domain = domain });
                File.WriteAllText(servicePath, service);

                string httpPath = Path.Combine(_paths.UIRoot, folder, "index.ts"); ;
                List<string> lst = new List<string>();
                if (File.Exists(httpPath))
                {
                    string[] lines = File.ReadAllLines(httpPath);
                    lst.AddRange(lines);
                }
                lst.Add("export * from \"./" + _fileNameService.ApplyConvension(resource, AppParts.Service) + "\";");
                File.WriteAllLines(httpPath, lst);
                return true;
            }
            return false;

        }



    }
}
