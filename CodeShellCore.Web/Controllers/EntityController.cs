using CodeShellCore.Data;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Security;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Controllers
{
    public abstract class EntityController<T, TPrime> : BaseApiController, IReadOnlyEntityController<T, TPrime> where T : class, IEntity<TPrime>
    {

        protected IEntityService<T> EntityService;

        public EntityController(IEntityService<T> service)
        {
            EntityService = service;
        }

        [HttpGet]
        [ApiAuthorize(Actions = new[] { DefaultActions.View })]
        public virtual IActionResult Get([FromQuery] LoadOptions opt)
        {
            return Respond(EntityService.Load(opt));
        }

        [HttpGet]
        [ApiAuthorize(Actions = new[] { DefaultActions.Update, DefaultActions.ViewDetails })]
        public virtual IActionResult GetSingle([FromRoute] TPrime id)
        {
            T obj = EntityService.GetSingle(id);
            return Respond(obj);
        }

        [HttpDelete]
        [ApiAuthorize(Actions = new[] { DefaultActions.Delete })]
        public virtual IActionResult Delete(TPrime id)
        {
            SubmitResult = EntityService.DeleteById(id);
            return Respond();
        }

        protected virtual IActionResult DefaultPost(T obj)
        {
            if (ModelIsValid())
                SubmitResult = EntityService.Create(obj);

            return Respond();
        }

        protected virtual IActionResult DefaultPut(T obj)
        {
            if (ModelIsValid())
                SubmitResult = EntityService.Update(obj);

            return Respond();
        }

        [HttpGet]
        public virtual bool IsUnique([FromQuery] PropertyUniqueDTO dto)
        {
            return EntityService.IsUnique(dto);
        }

    }
}
