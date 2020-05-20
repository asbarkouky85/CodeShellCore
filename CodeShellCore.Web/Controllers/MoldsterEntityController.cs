using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using CodeShellCore.Linq;
using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Data.Services;
using CodeShellCore.Data;

namespace CodeShellCore.Web.Controllers
{
    public abstract class MoldsterEntityController<T,TPrime> : EntityController<T, TPrime>, ILocalizableEntityController, ILookupLoaderController
        where T : class, IModel<TPrime>
    {
        ILocalizationDataService LocService { get { return GetService<ILocalizationDataService>(); } }
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
            if (!(EntityService is CollectionsEntityService<T>))
                throw new Exception("Unsupported function GetCollection, Service  should inherit from CollectionsEntityService");

            var res = ((CollectionsEntityService<T>)EntityService).LoadCollection(id, opts);
            return Respond(res);
        }

        public virtual IActionResult GetLocalizationData(object id)
        {
            return Respond(LocService.GetDataFor<T>(id));
        }

        public virtual IActionResult SetLocalizationData(object id, [FromBody]Dictionary<string, LocalizablesDTO> data)
        {
            return Respond(LocService.SetDataFor<T>(id, data));
        }

    }
}
