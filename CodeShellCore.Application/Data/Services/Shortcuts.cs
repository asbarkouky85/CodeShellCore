using CodeShellCore.Linq;

namespace CodeShellCore.Data.Services
{
    public class DtoEntityService<T, TPrime, TDto> :
       DtoEntityService<T, TPrime, LoadOptions, TDto, TDto, TDto, TDto>
        where T : class, IEntity<TPrime>
        where TDto : class, IEntityDto<TPrime>
    {
        public DtoEntityService(IUnitOfWork unit) : base(unit)
        {
        }
    }

    public class DtoEntityService<T, TPrime, TDto, TOptionsDto> :
        DtoEntityService<T, TPrime, TOptionsDto, TDto, TDto, TDto, TDto>
        where T : class, IEntity<TPrime>
        where TDto : class, IEntityDto<TPrime>
        where TOptionsDto : LoadOptions
    {
        public DtoEntityService(IUnitOfWork unit) : base(unit)
        {
        }
    }

    public class DtoEntityService<T, TPrime, TDto, TOptionsDto, TSingleDto> :
        DtoEntityService<T, TPrime, TOptionsDto, TDto, TSingleDto, TSingleDto, TSingleDto>
        where T : class, IEntity<TPrime>
        where TDto : class
        where TSingleDto : class, IEntityDto<TPrime>
        where TOptionsDto : LoadOptions
    {
        public DtoEntityService(IUnitOfWork unit) : base(unit)
        {
        }
    }

    public class DtoEntityService<T, TPrime, TDto, TOptionsDto, TSingleDto, TCreateUpdateDto> :
        DtoEntityService<T, TPrime, TOptionsDto, TDto, TSingleDto, TCreateUpdateDto, TCreateUpdateDto>
        where T : class, IEntity<TPrime>
        where TDto : class
        where TSingleDto : class
        where TCreateUpdateDto : class, IEntityDto<TPrime>
        where TOptionsDto : LoadOptions
    {
        public DtoEntityService(IUnitOfWork unit) : base(unit)
        {
        }
    }
}
