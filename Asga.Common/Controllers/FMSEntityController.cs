using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using CodeShellCore.Linq;
using CodeShellCore.Text.Localization;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Data.Services;

using Asga.Services;


namespace Asga.Controllers
{
    public abstract class AsgaEntityController<T> : EntityController<T, long> , ILocalizableEntityController
        where T : class, IAsgaModel
    {
        ILocalizationDataService LocService { get { return GetService<ILocalizationDataService>(); } }
        public AsgaEntityController(IEntityService<T> service) : base(service)
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
            if (!(EntityService is AsgaEntityHandler<T>))
                throw new Exception("Unsupported function GetCollection, Service should inherit from AsgaEntityService");

            var res = ((AsgaEntityHandler<T>)EntityService).LoadCollection(id, opts);
            return Respond(res);
        }

        public virtual IActionResult GetLocalizationData(long id)
        {
            return Respond(LocService.GetDataFor<T>(id));
        }

        public virtual IActionResult SetLocalizationData(long id,[FromBody]Dictionary<string,LocalizablesDTO> data)
        {
            return Respond(LocService.SetDataFor<T>(id, data));
        }

    }
}
