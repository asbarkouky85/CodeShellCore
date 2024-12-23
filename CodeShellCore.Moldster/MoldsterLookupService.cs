using CodeShellCore.Data.Lookups;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Moldster.Tenants;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster
{
    public class MoldsterLookupService : LookupsService<IMoldsterUnit>, IMoldsterLookupService
    {
        private readonly IMoldsterUnit _unit;
        private readonly IPathsService app;
        private readonly IModulesService mods;
        readonly ILayoutsService _layoutsService;

        protected override Dictionary<string, Type> ResourceToModel => new Dictionary<string, Type>
        {
            {"Tenants",typeof(Tenant) }
        };

        protected override string EntitiesAssembly => "CodeShellCore.Moldster.Domain";

        public MoldsterLookupService(IMoldsterUnit unit, IPathsService app, IModulesService mods) : base(unit)
        {
            _unit = unit;
            this.app = app;
            this.mods = mods;
            _layoutsService = _unit.ServiceProvider.GetRequiredService<ILayoutsService>();
        }

        public async Task<Dictionary<string, IEnumerable<Named<object>>>> Modules(Dictionary<string, string> data)
        {
            var mod = new Dictionary<string, IEnumerable<Named<object>>>();
            if (data.TryGetValue("modules", out string t))
                mod["modules"] = (await mods.GetRegisteredModules()).Select(e => new Named<object> { Name = e.Name });

            return mod;
        }

        public async Task<Dictionary<string, IEnumerable<Named<object>>>> PageEdit(Dictionary<string, string> data)
        {
            var mod = new Dictionary<string, IEnumerable<Named<object>>>();
            if (data.TryGetValue("TenantCode", out string t))
                mod["TenantCode"] = await _unit.TenantRepository.FindAs(e => new TenantLookupDto { Id = e.Code, Code = e.Code, Name = e.Name });
            if (data.TryGetValue("Resources", out string r))
                mod["Resources"] = Mapper.Map(await _unit.ResourceRepository.FindAsLookup(r), new List<Named<object>>());
            if (data.TryGetValue("Collection", out string c))
                mod["Collection"] = Mapper.Map(await _unit.ResourceCollectionRepository.FindAsLookup(c), new List<Named<object>>());
            if (data.TryGetValue("Apps", out string a))
                mod["Apps"] = Mapper.Map(await _unit.AppRepository.FindAsLookup(c), new List<Named<object>>());
            if (data.TryGetValue("NavigationGroup", out string n))
                mod["NavigationGroup"] = new List<Named<object>>();
            if (data.TryGetValue("TemplatePath", out string tP))
                mod["TemplatePath"] = Mapper.Map(await _unit.PageCategoryRepository.FindAs(s => new Named<object> { Id = s.Id, Name = s.ViewPath }), new List<Named<object>>());

            if (data.TryGetValue("Layout", out string l))
                mod["Layout"] = GetLayoutFiles();
            return mod;
        }

        public async Task<Dictionary<string, IEnumerable<Named<object>>>> PageCategoryEdit(Dictionary<string, string> data)
        {
            var mod = new Dictionary<string, IEnumerable<Named<object>>>();
            if (data.TryGetValue("Resources", out string r))
                mod["Resources"] = await GetLookupNamed<Resource>(r);
            if (data.TryGetValue("layouts", out string l))
                mod["layouts"] = GetLayoutFiles(true);
            return mod;
        }

        public async Task<Dictionary<string, IEnumerable<Named<object>>>> ResourceEdit(Dictionary<string, string> data)
        {
            var mod = new Dictionary<string, IEnumerable<Named<object>>>();
            if (data.TryGetValue("domains", out string l))
                mod["domains"] = Mapper.Map(await Unit.DomainRepository.FindAs(d => new Named<object> { Id = d.Id, Name = d.Name }, d => d.ParentId == null), new List<Named<object>>());
            return mod;
        }

        public async Task<Dictionary<string, IEnumerable<Named<object>>>> PageControlList(Dictionary<string, string> data)
        {
            var mod = new Dictionary<string, IEnumerable<Named<object>>>();

            if (data.TryGetValue("Collection", out string c))
                mod["Collection"] = await GetLookupNamed<ResourceCollection>(c);
            if (data.TryGetValue("Layout", out string l))
                mod["Layout"] = GetLayoutFiles();
            return mod;
        }

        protected List<LayoutFileDTO> GetLayoutFiles(bool nameOnly = false)
        {
            return _layoutsService.GetLayouts(nameOnly);
        }
    }
}
