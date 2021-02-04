using CodeShellCore.Data.Localization;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CodeShellCore.Web.Controllers
{
    public abstract class LookupsControllerBase : BaseApiController
    {
        ILocalizationDataService LocService { get { return GetService<ILocalizationDataService>(); } }
        IEntityService Service;
        protected Type TypeFromString(string entity)
        {
            string type = Shell.ProjectAssembly.GetName().Name.Replace(".Api", "") + "." + entity.Singularize().UCFirst();
            return Assembly.Load("FMS").GetType(type);
        }

        private IEntityService GetServiceFor(string entity)
        {
            Type t = TypeFromString(entity);
            Type generic = typeof(IEntityService<>);
            Type enitityService = generic.MakeGenericType(t);

            return (IEntityService)Store.GetInstance(enitityService);
        }

        public IActionResult Get(string entity, [FromQuery]LoadOptions opts)
        {
            Service = GetServiceFor(entity);
            var x = Service.LoadObjects(opts);
            return Respond(x);
        }

        public IActionResult GetSingle(string entity, long id)
        {
            Service = GetServiceFor(entity);
            return Respond(Service.GetSingleObject(id));
        }

        public IActionResult Post(string entity)
        {
            Service = GetServiceFor(entity);
            SubmitResult = Service.Create(Request.ReadBodyAsString());
            return Respond();
        }

        public IActionResult Put(string entity)
        {
            Service = GetServiceFor(entity);
            SubmitResult = Service.Update(Request.ReadBodyAsString());
            return Respond();
        }

        public IActionResult Delete(string entity, long id)
        {
            Service = GetServiceFor(entity);
            return Respond(Service.DeleteById(id));
        }

        public IActionResult GetLocalizationDataGeneric(string entity, long id)
        {
            Type t = TypeFromString(entity);
            return Respond(LocService.GetDataFor(t, id));
        }
        
        public IActionResult SetLocalizationDataGeneric(string entity, long id, [FromBody] Dictionary<string, LocalizablesDTO> data)
        {
            Type t = TypeFromString(entity);
            return Respond(LocService.SetDataFor(t, id, data));
        }

    }
}
