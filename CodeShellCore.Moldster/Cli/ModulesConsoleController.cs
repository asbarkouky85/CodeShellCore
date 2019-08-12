using CodeShellCore.Cli;
using CodeShellCore.Moldster.Services;
using System;
using System.Collections.Generic;
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
                    { 4, "WriteEnums"},
                    { 5, "Init"}
                };
            }
        }

        public ModulesConsoleController(bool lazy = true)
        {
            Lazy = lazy;
        }

        public void Render()
        {
            while (true)
            {
                var ten = Data.GetModuleNames();
                string modCode = GetSelectionFromUser("Select Tenant", ten);
                if (modCode == null)
                    break;
                while (true)
                {
                    var ls = Data.GetModuleDomains(modCode);
                    Dictionary<int, string> dic = new Dictionary<int, string>();
                    int i = 1;
                    foreach (var l in ls)
                    {
                        dic[i++] = l;
                    }
                    dic[i++] = "Localization";
                    dic[i] = "BuildTenantModule";
                    Console.WriteLine();
                    Console.WriteLine(GenerateChoices(dic));
                    int domainId = GetIntFromUser("Select Domain", 1, dic.Count);
                    if (domainId == 0)
                        break;
                    if (domainId == i - 1)
                        Localization.GenerateJsonFiles(modCode);
                    else if (domainId == i)
                        Moldster.RenderModuleDefinition(modCode, Lazy);
                    else
                        Moldster.RenderDomainModule(modCode, dic[domainId], Lazy);
                }
            }
        }

        public void ProccessTemplates()
        {
            while (true)
            {
                var ten = Data.GetModuleNames();
                string modCode = GetSelectionFromUser("Select Module", ten);
                if (modCode == null)
                    break;
                while (true)
                {

                    var ls = Data.GetModuleDomains(modCode);
                    Dictionary<int, string> dic = new Dictionary<int, string>();
                    int i = 1;
                    foreach (var l in ls)
                    {
                        dic[i++] = l;
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

        public void Init()
        {
            Scripts.GenerateEnvironment();
        }
    }
}
