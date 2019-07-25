using CodeShellCore.Data;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Controllers
{
    public class EntityController<T, TPrime> : BaseApiController where T : class, IModel<TPrime>
    {

        protected IEntityService<T> EntityService;

        public EntityController(IEntityService<T> service)
        {
            EntityService = service;
        }

        [HttpGet]
        public virtual IActionResult Get([FromQuery]LoadOptions opt)
        {
            return Respond(EntityService.Load(opt));
        }

        [HttpGet]
        public virtual IActionResult GetSingle([FromRoute]TPrime id)
        {
            T obj = EntityService.GetSingle(id);
            return Respond(obj);
        }

        [HttpDelete]
        public IActionResult Delete(TPrime id)
        {
            SubmitResult = EntityService.DeleteById(id);
            return Respond();
        }

        protected IActionResult DefaultPost(T obj)
        {
            if (ModelIsValid())
                SubmitResult = EntityService.Create(obj);
       
            return Respond();
        }

        protected IActionResult DefaultPut(T obj)
        {
            if (ModelIsValid())
                SubmitResult = EntityService.Update(obj);

            return Respond();
        }

        [HttpGet]
        public bool IsUnique([FromQuery] PropertyUniqueDTO dto)
        {
            return EntityService.IsUnique(dto);
        }
    }
}
