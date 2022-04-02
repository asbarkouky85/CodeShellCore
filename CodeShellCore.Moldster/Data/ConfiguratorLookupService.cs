using CodeShellCore.Data.Lookups;
using System.Collections.Generic;
using System.Dynamic;
using CodeShellCore.Moldster.Resources;
using System;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.PageCategories;
using System.Collections;

namespace CodeShellCore.Moldster.Data
{
    public class ConfiguratorLookupService : LookupsService<IConfigUnit>
    {
        private readonly IConfigUnit _unit;
        private readonly IPathsService app;
        private readonly IModulesService mods;

        protected override Dictionary<string, Type> ResourceToModel => new Dictionary<string, Type>
        {
            {"Tenants",typeof(Tenant) }
        };

        protected override string EntitiesAssembly => "CodeShellCore.Moldster";

        public ConfiguratorLookupService(IConfigUnit unit, IPathsService app, IModulesService mods) : base(unit)
        {
            _unit = unit;
            this.app = app;
            this.mods = mods;
        }

        public object Modules(Dictionary<string, string> data)
        {
            dynamic mod = new ExpandoObject();
            if (data.TryGetValue("modules", out string t))
                mod.modules = mods.GetRegisteredModules();

            return mod;
        }

        public object PageEdit(Dictionary<string, string> data)
        {
            dynamic mod = new ExpandoObject();
            if (data.TryGetValue("TenantCode", out string t))
                mod.TenantCode = _unit.TenantRepository.FindAs(s => new { s.Id, s.Name, s.Code });
            if (data.TryGetValue("Resources", out string r))
                mod.Resources = _unit.ResourceRepository.FindAs(s => new Named<long> { Id = s.Id, Name = s.Name });
            if (data.TryGetValue("Collection", out string c))
                mod.Collection = _unit.ResourceCollectionRepository.FindAs(s => new Named<long> { Id = s.Id, Name = s.Name });
            if (data.TryGetValue("Apps", out string a))
                mod.Apps = _unit.AppRepository.FindAs(s => new Named<long> { Id = s.Id, Name = s.Name });
            if (data.TryGetValue("NavigationGroup", out string n))
                mod.NavigationGroup = _unit.NavigationGroupRepository.FindAs(s => s.Name);
            if (data.TryGetValue("TemplatePath", out string tP))
                mod.TemplatePath = _unit.PageCategoryRepository.FindAs(s => new Named<long> { Id = s.Id, Name = s.ViewPath });

            if (data.TryGetValue("Layout", out string l))
                mod.Layout = GetLayoutFiles();
            return mod;
        }

        public Dictionary<string, IEnumerable<Named<object>>> PageCategoryEdit(Dictionary<string, string> data)
        {
            var mod = new Dictionary<string, IEnumerable<Named<object>>>();
            if (data.TryGetValue("Resources", out string r))
                mod["Resources"] = GetLookupNamed<Resource>(r);
            if (data.TryGetValue("layouts", out string l))
                mod["layouts"] = GetLayoutFiles(true);
            return mod;
        }

        public Dictionary<string, IEnumerable<Named<object>>> ResourceEdit(Dictionary<string, string> data)
        {
            dynamic mod = new ExpandoObject();
            if (data.TryGetValue("domains", out string l))
                mod.domains = Unit.DomainRepository.FindAs(d => new Named<long> { Id = d.Id, Name = d.Name }, d => d.ParentId == null);
            return mod;
        }

        public object PageControlList(Dictionary<string, string> data)
        {
            dynamic mod = new ExpandoObject();

            if (data.TryGetValue("Collection", out string c))
                mod.Collection = GetLookupNamed<ResourceCollection>(c);
            if (data.TryGetValue("Layout", out string l))
                mod.Layout = GetLayoutFiles();
            return mod;
        }

        protected List<LayoutFileDTO> GetLayoutFiles(bool nameOnly = false)
        {
            return app.GetLayouts(nameOnly);
        }
    }
}
