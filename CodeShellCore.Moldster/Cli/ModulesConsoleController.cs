using CodeShellCore.Cli;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
        public override Dictionary<int, string> Functions
        {
            get
            {
                return new Dictionary<int, string>
                {
                    { 1, "Render"},
                    { 2, "ProccessTemplates"},
                    { 3, "SyncLanguages"},
                    { 4, "WriteEnums"}
                };
            }
        }

        public ModulesConsoleController(bool lazy = true)
        {
            Lazy = lazy;
        }

        private void GenerateDomainChoices(string modCode, DomainRecursive rec, bool firstLevel = false)
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
                        Moldster.RenderModuleDefinition(modCode, Lazy);
                        continue;
                    }
                }
                
                if (domainId == 1)
                {
                    if (firstLevel)
                    {
                        foreach (var mod in rec.SubDomains)
                            Moldster.RenderDomainModule(modCode, mod.NameChain, Lazy);
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
                        GenerateDomainChoices(modCode, selectedDomain);
                }

            }
        }

        public void Render()
        {
            while (true)
            {
                var ten = Data.GetModuleCodes();
                string modCode = GetSelectionFromUser("Select Tenant", ten);
                if (modCode == null)
                    break;
                var domains = Data.GetModuleDomains(modCode);
                GenerateDomainChoices(modCode, new DomainRecursive { SubDomains = domains.ToList() }, true);
            }
        }

        public void ProccessTemplates()
        {
            while (true)
            {
                var ten = Data.GetModuleCodes();
                string modCode = GetSelectionFromUser("Select Module", ten);
                if (modCode == null)
                    break;
                while (true)
                {

                    var ls = Data.GetModuleDomains(modCode);
                    Dictionary<int, string> dic = new Dictionary<int, string>();
                    Dictionary<int, long> idsDic = new Dictionary<int, long>();
                    int i = 1;
                    foreach (var l in ls)
                    {
                        var index = i++;
                        dic[index] = l.Name;
                        idsDic[index] = l.Id;
                    }
                    dic[i++] = "Shared";

                    Console.WriteLine();
                    Console.WriteLine(GenerateChoices(dic));
                    int domainId = GetIntFromUser("Select Domain", 1, dic.Count);
                    if (domainId == 0)
                        break;

                    Moldster.ProcessTemplates(modCode, dic[domainId]);
                }
            }
        }


        public void WriteEnums()
        {
            Scripts.GenerateEnums();
        }


    }
}
