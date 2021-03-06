﻿using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Angular.Models;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Services;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Moldster.Builder.Internal
{
    public class BuilderService : ConsoleService, IBuilderService
    {
        private readonly IPathsService paths;
        private readonly IMoldProvider molds;
        private readonly WriterService writer;
        public BuilderService(IOutputWriter output, IPathsService _paths, IMoldProvider _molds) : base(output)
        {
            paths = _paths;
            molds = _molds;
            writer = new WriterService();
        }

        public Result AddTenantToAngularJson(string tenant)
        {
            var angularJsonPath = Path.Combine(paths.UIRoot, "angular.json");
            tenant = tenant.LCFirst().Replace("-", "_");
            if (!File.Exists(angularJsonPath))
            {
                var tenantConfig = writer.FillStringParameters(molds.AngularJsonProject, new AppComponentModel { Name = tenant });
                var angularJsonConent = writer.FillStringParameters(molds.AngularJson, new AngularJsonModel { Projects = tenantConfig.Trim(), DefaultProject = tenant });
                Utils.CreateFolderForFile(angularJsonPath);
                var str = Encoding.UTF8.GetBytes(angularJsonConent);
                File.WriteAllBytes(angularJsonPath, str);
            }
            return new Result();
        }
    }
}
