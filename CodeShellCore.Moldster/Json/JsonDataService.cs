using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Services;
using CodeShellCore.Text;

namespace CodeShellCore.Moldster.Json
{
    public class JsonDataService : ServiceBase, IDataService, IJsonConfigProvider
    {
        readonly PathProvider Paths;
        private MoldsterData _data;
        private MoldsterData Data
        {
            get
            {
                if (_data == null)
                    _data = _loadData();

                return _data;
            }
        }
        public JsonDataService(PathProvider prov)
        {
            Paths = prov;
        }

        MoldsterData _loadData()
        {
            string configPath = Path.Combine(Paths.ConfigRoot, "codeGenConfig.json");
            return File.ReadAllText(configPath).FromJson<MoldsterData>();
        }

        public MoldsterData GetData()
        {
            return Data;
        }

        public string[] GetModuleCodes(bool? active = null)
        {
            return Data.Modules.Select(d => d.Name).ToArray();
        }

        public NgModule GetNgModule(string st)
        {

            var ngMod = Data.Modules.FirstOrDefault(d => d.Name.ToLower() == st.ToLower());

            if (ngMod == null)
                throw new ArgumentOutOfRangeException($"no such module '{st}'");

            return ngMod;
        }

        public PageConfig GetPageConfig(string viewPath)
        {
            var conf = GetData();

            var dom = conf.Modules.SelectMany(d => d.Domains).Where(d => d.Pages.Any(p => p.ViewPath == viewPath)).FirstOrDefault();
            if (dom == null)
                return null;

            var page = dom.Pages.Where(d => d.ViewPath == viewPath).FirstOrDefault();
            page.Name = dom.Name + "__" + page.Name;
            return page;
        }

        public PageTemplateConfig GetTemplate(string viewPath)
        {
            return Data.Templates.Where(d => d.Template.ToLower() == viewPath.ToLower()).FirstOrDefault();
        }

        public DomainWithPages GetDomainModuleWithPages(string moduleCode, string domain)
        {
            var mod = Data.Modules.SingleOrDefault(d => d.Name == moduleCode);
            if (mod != null)
            {
                var dom = mod.Domains.SingleOrDefault(d => d.Name == domain);
                foreach (var d in dom.Pages)
                    d.DomainName = dom.Name;
                return dom;
            }

            return null;
        }

        public PageConfig GetPageConfig(string modCode, string viewPath)
        {
            return GetNgModule(modCode).Domains.SelectMany(d => d.Pages).SingleOrDefault(d => d.ViewPath == viewPath);
        }

        public string[] GetDomainPages(string mod, string domain)
        {
            return GetNgModule(mod).GetDomain(domain).Pages.Select(d => d.ViewPath).ToArray();
        }

        public string[] GetTemplatePaths(string modCode, string domain = null)
        {
            return Data.Templates.Select(d => d.Template).ToArray();
        }

        public PageOptions GetPageOptions(string moduleCode, string viewPath)
        {
            var sin = GetNgModule(moduleCode).Domains.SelectMany(d => d.PagesFilled).SingleOrDefault(d => d.ViewPath == viewPath);
            if (sin == null)
                throw new ArgumentOutOfRangeException($"No such page {viewPath} in module {moduleCode}");
            return new PageOptions
            {
                Controls = new Dictionary<string, Db.Dto.ControlDTO>(),
                Layout = sin.Layout,
                Sources = new List<Lister>(),
                ViewParamsString = "",
                DefaultAccessibility = 2,
                PageIdentifier = sin.DomainName + "__" + sin.Name,
                ViewPath = sin.ViewPath
            };
        }

        public IEnumerable<DomainRecursive> GetModuleDomains(string modCode)
        {
            throw new NotImplementedException();
            //return GetNgModule(modCode).Domains.Select(d => d.Name).ToArray();
        }

        public TenantPageGuideDTO GetTenantGuide(long id)
        {
            throw new NotImplementedException();
        }
    }
}
