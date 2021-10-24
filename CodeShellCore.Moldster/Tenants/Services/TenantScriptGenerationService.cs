using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Angular.Models;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Models;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Services;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Moldster.Builder.Internal
{
    public class TenantScriptGenerationService : ScriptGenerationServiceBase, ITenantScriptGenerationService
    {
        private IMoldProvider Molds => Store.GetInstance<IMoldProvider>();
        private IPathsService Paths => Store.GetInstance<IPathsService>();
        private INamingConventionService Naming => Store.GetInstance<INamingConventionService>();

        public TenantScriptGenerationService(IServiceProvider provider, IOptions<MoldsterModuleOptions> opts) : base(provider, opts)
        {

        }

        public Result AddTenantToAngularJson(string tenant)
        {
            var angularJsonPath = Path.Combine(Paths.UIRoot, "angular.json");
            
            if (!File.Exists(angularJsonPath))
            {
                var ten = Naming.ApplyConvension(tenant, AppParts.Project);
                var tenantConfig = Writer.FillStringParameters(Molds.AngularJsonProject, new AppComponentModel { Name = ten });
                var angularJsonConent = Writer.FillStringParameters(Molds.AngularJson, new AngularJsonModel { Projects = $"\"{ten}\":" + tenantConfig.Trim(), DefaultProject = ten });
                Utils.CreateFolderForFile(angularJsonPath);
                var str = Encoding.UTF8.GetBytes(angularJsonConent);
                File.WriteAllBytes(angularJsonPath, str);
            }
            return new Result();
        }


    }
}
