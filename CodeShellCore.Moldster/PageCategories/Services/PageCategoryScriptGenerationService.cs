using CodeShellCore.Cli;
using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Models;
using CodeShellCore.Moldster.Pages.Services;
using CodeShellCore.Text;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeShellCore.Moldster.PageCategories.Services
{
    public class PageCategoryScriptGenerationService : CodeGeneration.ScriptGenerationServiceBase, IPageCategoryScriptGenerationService
    {
        protected static string[] baseComponents => new[] { "Edit", "List", "Tree", "Select" };
        private IMoldProvider _molds => Store.GetInstance<IMoldProvider>();
        private INamingConventionService _fileNameService => Store.GetInstance<INamingConventionService>();
        private IConfigUnit _unit => Store.GetInstance<IConfigUnit>();

        public PageCategoryScriptGenerationService(
            IServiceProvider prov,
            IOptions<MoldsterModuleOptions> opt) : base(prov, opt)
        {
        }


        public virtual void GenerateBaseComponent(string viewPath)
        {
            PageCategoryDTO p = _unit.PageCategoryRepository.FindSingleAs(PageCategoryDTO.Expression, d => d.ViewPath == viewPath);
            if (p == null)
                throw new ArgumentOutOfRangeException($"PageCategory '{viewPath}' doesn't exist");

            string name = p.Category.ViewPath.GetAfterLast("/") + "Base";
            bool serviced = false;
            BaseComponentTsModel mod = new BaseComponentTsModel
            {
                Name = name,
                Domain = p.Domain ?? "",
                Resource = p.Resource ?? ""
            };

            if (!string.IsNullOrEmpty(p.Category.BaseComponent))
            {
                serviced = p.Resource != null;

                if (baseComponents.Contains(p.Category.BaseComponent))
                {
                    mod.Parent = p.Category.BaseComponent + "ComponentBase";
                    mod.ParentPath = "codeshell/base-components";
                    if (p.Resource == null)
                    {
                        mod.Resource = "DefaultHttp";
                        mod.ServicePath = "codeshell/http";
                    }
                }
                else if (p.Category.BaseComponent != null)
                {
                    mod.ParentPath = _fileNameService.ApplyConvension(p.Category.BaseComponent, AppParts.BaseComponent);
                    mod.Parent = mod.ParentPath.GetAfterLast("/");
                }


                if (p.Resource != null)
                {
                    string folder = _fileNameService.GetHttpServiceFolder(null, true);

                    if (p.ResourceDomain != null)
                    {
                        folder = _fileNameService.GetHttpServiceFolder(new Regex("^/").Replace(p.ResourceDomain, ""), true);
                    }

                    mod.ServicePath = Utils.CombineUrl(folder, _fileNameService.ApplyConvension(p.Resource, AppParts.Service));
                }
            }
            else
            {
                mod.Parent = "BaseComponent";
                mod.ParentPath = "codeshell/base-components";
            }

            string baseComponentTemplatePath = _molds.GetBaseComponentMold(serviced);

            string contents = Writer.FillStringParameters(baseComponentTemplatePath, mod);
            string path = _fileNameService.GetBaseComponentFilePath(viewPath) + ".ts"; // Path.Combine(_paths.UIRoot, "Core\\" + _paths.CoreAppName, viewPath + "Base.ts");

            Utils.CreateFolderForFile(path);
            File.WriteAllText(path, contents);

        }

        public virtual void GeneratePageCategory(long id)
        {
            string serviceName = null;
            string baseComponent = null;
            bool serviceCreated = false;
            var p = _unit.PageCategoryRepository.FindSingleAs(d => new CategoryBaseComponentDTO
            {
                ViewPath = d.ViewPath,
                Name = d.Name,
                Resource = d.ResourceId != null ? d.Resource.Name : null,
                ResourceDomain = d.ResourceId != null ? d.Resource.Domain.Name : null
            }, d => d.Id == id);

            if (p.Resource != null)
            {
                serviceName = p.Resource + "Service.ts";
                serviceCreated = GenerateHttpService(p.Resource, p.ResourceDomain);
            }

            string baseComponentPath = _fileNameService.GetBaseComponentFilePath(p.ViewPath) + ".ts";
            Utils.CreateFolderForFile(baseComponentPath);

            if (!File.Exists(baseComponentPath))
            {
                GenerateBaseComponent(p.ViewPath);
                baseComponent = p.Name + "Base";
            }

            if (serviceCreated || baseComponent != null)
            {
                Out.WriteLine();
                Out.Write("New files generated : ");
                GotoColumn(6);
                Out.Write("[ ");
                if (serviceCreated)
                {
                    using (Out.Set(ConsoleColor.Yellow))
                        Out.Write(serviceName);
                }

                if (baseComponent != null)
                {
                    if (serviceCreated)
                        Out.Write(" , ");
                    using (Out.Set(ConsoleColor.Cyan))
                        Out.Write(baseComponent + ".ts");
                }
                Out.Write(" ]");
            }

            Out.WriteLine();
        }
    }
}
