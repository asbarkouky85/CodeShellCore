using CodeShellCore.Data.Lookups;
using System.Collections.Generic;
using System.Dynamic;
using CodeShellCore.Moldster.Resources;
using System;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.PageCategories;
using System.Collections;
using CodeShellCore.Moldster.Data;
using System.Linq;

namespace CodeShellCore.Moldster
{
    public class MoldsterLookupService : LookupsService<IConfigUnit>, IMoldsterLookupService
    {
        private readonly IConfigUnit _unit;
        private readonly IPathsService app;
        private readonly IModulesService mods;

        protected override Dictionary<string, Type> ResourceToModel => new Dictionary<string, Type>
        {
            {"Tenants",typeof(Tenant) }
        };

        protected override string EntitiesAssembly => "CodeShellCore.Moldster";

        public MoldsterLookupService(IConfigUnit unit, IPathsService app, IModulesService mods) : base(unit)
        {
            _unit = unit;
            this.app = app;
            this.mods = mods;
        }

        public Dictionary<string, IEnumerable<Named<object>>> Modules(Dictionary<string, string> data)
        {
            var mod = new Dictionary<string, IEnumerable<Named<object>>>();
            if (data.TryGetValue("modules", out string t))
                mod["modules"] = mods.GetRegisteredModules().Select(e => new Named<object> { Name = e.Name });

            return mod;
        }

        public Dictionary<string, IEnumerable<Named<object>>> PageEdit(Dictionary<string, string> data)
        {
            var mod = new Dictionary<string, IEnumerable<Named<object>>>();
            if (data.TryGetValue("TenantCode", out string t))
                mod["TenantCode"] = _unit.TenantRepository.FindAs(e => new TenantLookupDto { Id = e.Code, Code = e.Code, Name = e.Name });
            if (data.TryGetValue("Resources", out string r))
                mod["Resources"] = _unit.ResourceRepository.FindAsLookup(r);
            if (data.TryGetValue("Collection", out string c))
                mod["Collection"] = _unit.ResourceCollectionRepository.FindAsLookup(c);
            if (data.TryGetValue("Apps", out string a))
                mod["Apps"] = _unit.AppRepository.FindAsLookup(c);
            if (data.TryGetValue("NavigationGroup", out string n))
                mod["NavigationGroup"] = new List<Named<object>>();
            if (data.TryGetValue("TemplatePath", out string tP))
                mod["TemplatePath"] = _unit.PageCategoryRepository.FindAs(s => new Named<object> { Id = s.Id, Name = s.ViewPath });

            if (data.TryGetValue("Layout", out string l))
                mod["Layout"] = GetLayoutFiles();
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
            var mod = new Dictionary<string, IEnumerable<Named<object>>>();
            if (data.TryGetValue("domains", out string l))
                mod["domains"] = Unit.DomainRepository.FindAs(d => new Named<object> { Id = d.Id, Name = d.Name }, d => d.ParentId == null);
            return mod;
        }

        public Dictionary<string, IEnumerable<Named<object>>> PageControlList(Dictionary<string, string> data)
        {
            var mod = new Dictionary<string, IEnumerable<Named<object>>>();

            if (data.TryGetValue("Collection", out string c))
                mod["Collection"] = GetLookupNamed<ResourceCollection>(c);
            if (data.TryGetValue("Layout", out string l))
                mod["Layout"] = GetLayoutFiles();
            return mod;
        }

        protected List<LayoutFileDTO> GetLayoutFiles(bool nameOnly = false)
        {
            return app.GetLayouts(nameOnly);
        }
    }
}
