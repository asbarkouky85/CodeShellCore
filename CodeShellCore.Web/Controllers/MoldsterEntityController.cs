using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using CodeShellCore.Linq;
using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Data.Services;
using CodeShellCore.Data;
using CodeShellCore.Data.ConfiguredCollections;

namespace CodeShellCore.Web.Controllers
{
    public abstract class MoldsterEntityController<T, TPrime> : EntityController<T, TPrime>, ILookupLoaderController
        where T : class, IModel<TPrime>
    {
        ILocalizationDataService LocService => GetService<ILocalizationDataService>();

        public MoldsterEntityController(IEntityService<T> service) : base(service)
        {
        }

        public virtual IActionResult GetEditLookups([FromQuery]Dictionary<string, string> data)
        {
            return Respond(new { });
        }

        public virtual IActionResult GetListLookups([FromQuery]Dictionary<string, string> data)
        {
            return Respond(new { });
        }

        public virtual IActionResult GetCollection(string id, [FromQuery]LoadOptions opts)
        {
            
            var res = EntityService.LoadCollection(id, opts);
            return Respond(res);
        }

        public virtual IActionResult GetLocalizationData(TPrime id)
        {
            return Respond(LocService.GetDataFor<T>(id));
        }

        public virtual IActionResult SetLocalizationData(TPrime id, [FromBody]Dictionary<string, LocalizablesDTO> data)
        {
            return Respond(LocService.SetDataFor<T>(id, data));
        }

    }
}
