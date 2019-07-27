using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Db.Dto;
using System;
using System.Collections.Generic;
using CodeShellCore.Text;
using System.IO;
using System.Linq;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services.Internal;
using CodeShellCore.Services;
using CodeShellCore.Moldster.Definitions;

namespace CodeShellCore.Moldster.Db.Services
{
    public class DbTemplateProcessingService : TemplateProcessingService, ICustomizablePagesService
    {
        readonly IConfigUnit _unit;
        //readonly DbTsGeneratorService _writer;
        readonly DomainService _domains;
        readonly PageControlsService _controls;
        readonly IDbViewsService _dbViews;

        public DbTemplateProcessingService(
            IConfigUnit unit,
            //DbTsGeneratorService writer,
            IDbViewsService views,
            DomainService dom,
            WriterService wt,
            PathProvider paths,
            PageControlsService con) : base(views, paths, wt)
        {
            _unit = unit;
            // _writer = writer;
            _domains = dom;
            _controls = con;
            _dbViews = views;
        }
        public void ProcessAllTemplates(string modCode, IScriptGenerationService tsGen)
        {
            long tenantId = _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            var lst = _unit.PageCategoryRepository.GetValues(d => d.Id, d => d.Pages.Any(e => e.TenantDomain.TenantId == tenantId));
            foreach (long id in lst)
            {
                TemplateToDb(id, tenantId, tsGen);
            }
        }

        public void ProcessDomainTemplates(string domain, string modCode, IScriptGenerationService tsGen)
        {
            long tenantId = _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            var lst = _unit.PageCategoryRepository.GetValues(d => d.Id,
                d =>
                d.ViewPath.StartsWith(domain)
                );

            foreach (long id in lst)
            {
                TemplateToDb(id, tenantId, tsGen);
            }
        }

        public void WriteIdOnTemplate(PageCategory cat)
        {
            var dom = AppDomain.CurrentDomain;

            string viewPath = Path.Combine(_paths.ConfigRoot, "Views", cat.ViewPath + ".cshtml");
            string contents = File.ReadAllText(viewPath);

            string firstFive = contents.Length < 6 ? "" : contents.Substring(0, 5);
            if (firstFive != "@*ID:")
            {
                contents = "@*ID:" + cat.Id + "*@\n" + contents;
                File.WriteAllText(viewPath, contents);
            }
        }

        public void ProcessTemplate(string templatePath, string modCode, IScriptGenerationService tsGen)
        {
            long tempId = _unit.PageCategoryRepository.GetSingleValue(d => d.Id, d => d.ViewPath == templatePath);
            long tenantId = _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            TemplateToDb(tenantId, tempId, tsGen);
        }

        public bool TemplateToDb(long id, long tenantId, IScriptGenerationService tsGen)
        {
            string serviceName = null;
            string baseComponent = null;
            bool serviceCreated = false;
            using (var x = SW.Measure())
            {

                PageCategoryDTO p = _unit.PageCategoryRepository.FindSingleAs(PageCategoryDTO.Expression, d => d.Id == id);

                if (p == null)
                    throw new Exception("Not Found");

                Console.Write(p.Category.ViewPath);
                GotoColumn(6);
                Console.Write(" View Data: ");
                TemplateDataCollector dto = GetCollector(id);
                if (dto == null)
                {
                    WriteFailed();
                    return false;
                }
                WriteSuccess();
                WriteIdOnTemplate(p.Category);

                if (p.Category.ScriptPath == null)
                {
                    string folder = p.Category.ViewPath.GetBeforeLast("/");

                    p.Category.ScriptPath = _paths.CoreAppName + "/" + folder + "/" + p.Category.Name + "Base";
                    _unit.SaveChanges();
                }

                if (p.Resource != null)
                {
                    serviceName = p.Resource + "Service.ts";
                    serviceCreated = tsGen.GenerateDataService(p.Resource, p.Domain);
                }

                Console.Write(" Controls: ");
                _controls.UpdateTemplateControls(p.Category, dto.Controls);
                _controls.DeleteUnusedControls(p.Category, dto.Controls);
                WriteSuccess();

                Console.Write(" Pages: ");
                _controls.UpdateTemplatePages(id, tenantId);
                WriteSuccess();

                string baseComponentPath = Path.Combine(_paths.UIRoot, "Core", p.Category.ScriptPath + ".ts");
                Utils.CreateFolderForFile(baseComponentPath);

                if (!File.Exists(baseComponentPath))
                {
                    tsGen.GenerateBaseComponent(p.Category.ViewPath);
                    baseComponent = p.Category.Name + "Base";
                }

                using (ColorSetter.Set(ConsoleColor.Cyan))
                {
                    Console.Write(" " + x.Elapsed.TotalSeconds.ToString("F4"));
                }
            }

            if (serviceCreated || baseComponent != null)
            {
                Console.WriteLine();
                Console.Write("New files generated : ");
                GotoColumn(6);
                Console.Write("[ ");
                if (serviceCreated)
                {
                    using (ColorSetter.Set(ConsoleColor.Yellow))
                        Console.Write(serviceName);
                }
                
                if (baseComponent != null)
                {
                    if(serviceCreated)
                        Console.Write(" , ");
                    using (ColorSetter.Set(ConsoleColor.Cyan))
                        Console.Write(baseComponent + ".ts");
                }
                Console.Write(" ]");
            }

            Console.WriteLine();

            return true;
        }

        private string GetPage(string module, string viewPath)
        {
            try
            {
                return _dbViews.GetPage(new PageAcquisitorDTO { ModuleCode = module, ViewPath = viewPath });
            }
            catch (Exception ex)
            {
                WriteException(ex, false);
                return null;
            }
        }


        public bool RenderPage(long id, bool verbose = false)
        {
            using (var x = SW.Measure())
            {
                using (ColorSetter.Set(ConsoleColor.DarkYellow))
                    Console.Write(" Html: ");

                PageDTO p = _unit.PageRepository.FindSingleAs(PageDTO.ExpressionForRendering, id);

                string template = GetPage(p.TenantCode, p.Page.ViewPath);
                if (template == null)
                {
                    WriteFailed(x.Elapsed);
                    return false;
                }

                string path = Path.Combine(_paths.UIRoot, p.TenantCode, "app", p.Page.ViewPath);
                string templatePath = path + ".html";

                Utils.CreateFolderForFile(templatePath);
                File.WriteAllText(templatePath, template);

                WriteSuccess(x.Elapsed);
            }
            return true;
        }

        private TemplateDataCollector GetCollector(long id)
        {
            try
            {
                return _dbViews.GetTemplateData(id);
            }
            catch (Exception ex)
            {
                WriteException(ex, false);
                return null;
            }

        }

        public override void GenerateComponentTemplate(string moduleName, string viewPath)
        {
            long pageId = _unit.PageRepository.GetSingleValue(d => d.Id,
                d => d.TenantDomain.Tenant.Code == moduleName && d.ViewPath == viewPath);

            RenderPage(pageId);
        }

        public override void GenerateMainComponentTemplate(string moduleCode)
        {
            using (var m = SW.Measure())
            {
                using (ColorSetter.Set(ConsoleColor.DarkYellow))
                    Console.Write(" Html: ");
                string baseComponent = _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == moduleCode);

                string contents = _views.GetMainComponent(baseComponent);
                string filePath = Path.Combine(_paths.UIRoot, moduleCode, "app", "AppComponent.html");

                Utils.CreateFolderForFile(filePath);
                File.WriteAllText(filePath, contents);
                WriteSuccess(m.Elapsed);
            }

        }
    }
}
