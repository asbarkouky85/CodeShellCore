using CodeShellCore.Cli;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Moldster.Cli
{
    public class ModulesConsoleController : ConsoleController
    {
        protected virtual bool Lazy { get; private set; }
        IDataService Data { get { return GetService<IDataService>(); } }
        ILocalizationService Localization { get { return GetService<ILocalizationService>(); } }
        IMoldsterService Moldster { get { return GetService<IMoldsterService>(); } }
        ITemplateProcessingService Templates { get { return GetService<ITemplateProcessingService>(); } }
        IScriptGenerationService Scripts { get { return GetService<IScriptGenerationService>(); } }
        IScriptModelMappingService Mapping { get { return GetService<IScriptModelMappingService>(); } }
        IPagesDataService pages => GetService<IPagesDataService>();

        public override Dictionary<int, string> Functions
        {
            get
            {
                return new Dictionary<int, string>
                {
                    //{ 1, "Render"},
                   // { 2, "ProccessTemplates"},
                    { 1, "MigrateViewParams"},
                    { 2, "ScriptMapping"},
                    { 3, "SyncTenants"},
                };
            }
        }

        public ModulesConsoleController(bool lazy = true)
        {
            Lazy = lazy;
        }

        public void ScriptMapping()
        {
            Mapping.ScriptMapping();
        }

        private void GenerateDomainChoicesProcess(string modCode, DomainRecursive rec, bool firstLevel = false)
        {
            while (true)
            {
                Dictionary<int, string> dic = new Dictionary<int, string>();
                Dictionary<int, DomainRecursive> idDic = new Dictionary<int, DomainRecursive>();
                int i = 1;
                dic[i++] = "All";
                foreach (var l in rec.SubDomains)
                {
                    var index = i++;
                    dic[index] = l.Name;
                    idDic[index] = l;
                }

                Console.WriteLine();
                Console.WriteLine(GenerateChoices(dic));
                int domainId = GetIntFromUser("Select Domain", 1, dic.Count);
                if (domainId == 0)
                    break;

                if (domainId == 1)
                {
                    if (firstLevel)
                        Moldster.ProcessTemplates(modCode);
                    else
                        Moldster.ProcessTemplates(modCode, rec.NameChain);
                }
                else
                {
                    var selectedDomain = idDic[domainId];
                    if (selectedDomain.SubDomains.Count == 0)
                        Moldster.ProcessTemplates(modCode, selectedDomain.NameChain);
                    else
                        GenerateDomainChoicesProcess(modCode, selectedDomain);
                }

            }
        }

        private void GenerateDomainChoicesRender(string modCode, DomainRecursive rec, bool firstLevel = false)
        {
            while (true)
            {
                Dictionary<int, string> dic = new Dictionary<int, string>();
                Dictionary<int, DomainRecursive> idDic = new Dictionary<int, DomainRecursive>();
                int i = 1;
                dic[i++] = "All";
                foreach (var l in rec.SubDomains)
                {
                    var index = i++;
                    dic[index] = l.Name;
                    idDic[index] = l;
                }

                if (firstLevel)
                {
                    dic[i++] = "Localization";
                    dic[i] = "BuildTenantModule";
                }

                Console.WriteLine();
                Console.WriteLine(GenerateChoices(dic));
                int domainId = GetIntFromUser("Select Domain", 1, dic.Count);
                if (domainId == 0)
                    break;

                if (firstLevel)
                {
                    if (domainId == i - 1)
                    {
                        Localization.GenerateJsonFiles(modCode);
                        continue;
                    }
                    else if (domainId == i)
                    {
                        //TODO:Module definition
                        Moldster.RenderModuleDefinition(modCode, Lazy);
                        continue;
                    }
                }

                if (domainId == 1)
                {
                    if (firstLevel)
                    {
                        //TODO:Render All
                        foreach (var mod in rec.SubDomains)
                            Moldster.RenderDomainModule(modCode, mod.NameChain, Lazy);
                        Moldster.RenderModuleDefinition(modCode, Lazy);
                    }
                    else
                    {
                        Moldster.RenderDomainModule(modCode, rec.NameChain, Lazy);
                    }

                }
                else
                {
                    var selectedDomain = idDic[domainId];
                    if (selectedDomain.SubDomains.Count == 0)
                        Moldster.RenderDomainModule(modCode, selectedDomain.NameChain, Lazy);
                    else
                        GenerateDomainChoicesRender(modCode, selectedDomain);
                }

            }
        }

        public void MigrateViewParams()
        {
            while (true)
            {
                var ten = Data.GetAppCodes();
                string modCode = GetSelectionFromUser("Select Tenant", ten);
                if (modCode == null)
                    break;
                var ps = pages.GetPagesWithJsonParams(modCode);
                foreach (var p in ps)
                {
                    var res = pages.ViewParamsToData(p);
                    if (!res.IsSuccess)
                        break;
                }
                    
            }
        }

        public void Render()
        {
            while (true)
            {
                var ten = Data.GetAppCodes();
                string modCode = GetSelectionFromUser("Select Tenant", ten);
                if (modCode == null)
                    break;
                var domains = Data.GetModuleDomains(modCode);
                GenerateDomainChoicesRender(modCode, new DomainRecursive { SubDomains = domains.ToList() }, true);
            }
        }

        public void ProccessTemplates()
        {
            while (true)
            {
                var ten = Data.GetAppCodes();
                string modCode = GetSelectionFromUser("Select Tenant", ten);
                if (modCode == null)
                    break;
                var domains = Data.GetModuleDomains(null);
                GenerateDomainChoicesProcess(modCode, new DomainRecursive { SubDomains = domains.ToList() }, true);
            }
        }

        public void SyncTenants()
        {
            int src = GetIntFromUser("Enter Source Tenant Id");
            int tar = GetIntFromUser("Enter Target Tenant Id");
            Moldster.SyncTenants(src, tar);
        }


    }
}
