using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using CodeShellCore.Linq;
using CodeShellCore.Data.Services;
using CodeShellCore.Data;
using CodeShellCore.Data.Localization;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Web.Controllers;

namespace CodeShellCore.Web.Moldster.Controllers
{
    public abstract class MoldsterEntityController<T, TPrime> : EntityController<T, TPrime>, ILookupLoaderController
        where T : class, IModel<TPrime>
    {
        ILocalizationDataService LocService => GetService<ILocalizationDataService>();
        ILookupsService lookupsService => GetService<ILookupsService>();
        public MoldsterEntityController(IEntityService<T> service) : base(service)
        {
        }

        public virtual IActionResult GetEditLookups([FromQuery] Dictionary<string, string> data)
        {
            return Respond(lookupsService.GetRequestedLookups(data));
        }

        public virtual IActionResult GetListLookups([FromQuery] Dictionary<string, string> data)
        {
            return Respond(lookupsService.GetRequestedLookups(data));
        }

        public virtual IActionResult GetCollection(string id, [FromQuery] LoadOptions opts)
        {

            var res = EntityService.LoadCollection(id, opts);
            return Respond(res);
        }

        public virtual IActionResult GetLocalizationData(TPrime id)
        {
            return Respond(LocService.GetDataFor<T>(id));
        }

        public virtual IActionResult SetLocalizationData(TPrime id, [FromBody] Dictionary<string, LocalizablesDTO> data)
        {
            return Respond(LocService.SetDataFor<T>(id, data));
        }

    }
}
