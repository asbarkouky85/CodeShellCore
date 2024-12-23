using CodeShellCore.Data.Events;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Localization;
using CodeShellCore.Linq;
using CodeShellCore.MQ.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Services
{

    public class DtoEntityService<T, TPrime, TOptionsDto, TListDto, TSingleDto, TCreateDto, TUpdateDto> :
        DtoReadOnlyEntityService<T, TPrime, TOptionsDto, TListDto, TSingleDto>,
        IDtoEntityService<TPrime, TOptionsDto, TListDto, TSingleDto, TCreateDto, TUpdateDto>,
        IEntityHandler<T>
        where T : class, IEntity<TPrime>
        where TSingleDto : class
        where TListDto : class
        where TCreateDto : class
        where TUpdateDto : class, IEntityDto<TPrime>
        where TOptionsDto : PagedListRequestDto
    {

        public virtual bool ProjectGetSingle => true;

        public DtoEntityService(IUnitOfWork unit) : base(unit)
        {

        }

        public virtual async Task<DeleteResult> Delete(TPrime id)
        {
            var can = await Repository.CanDeleteById(id);
            if (can.IsSuccess)
            {
                Repository.DeleteByKey(id);
            }
            else
            {
                return can;
            }

            return (await DefaultUnit.SaveChanges()).MapToResult<DeleteResult>();
        }



        protected virtual Task AfterUpdate(TUpdateDto dto, T entity)
        {
            return Task.CompletedTask;
        }

        protected virtual Task AfterCreate(TCreateDto dto, T entity)
        {
            return Task.CompletedTask;
        }

        public virtual async Task<EntitySubmitResult<TSingleDto>> Post(TCreateDto dto)
        {
            var entity = Mapper.Map<TCreateDto, T>(dto);
            Repository.Add(entity);
            var res = await SaveAndGetSingle(entity);
            if (res.IsSuccess)
            {
                await AfterCreate(dto, entity);
            }
            return res;
        }

        protected virtual async Task<EntitySubmitResult<TSingleDto>> SaveAndGetSingle(T entity)
        {
            var res = (await DefaultUnit.SaveChangesAsync()).ToSubmitResult<TSingleDto>();
            if (res.IsSuccess)
            {
                res.Result = await GetSingle(entity.Id);
            }
            return res;
        }

        public virtual async Task<EntitySubmitResult<TSingleDto>> Put(TUpdateDto dto)
        {
            var entity = await GetSingleById(dto.Id);
            Mapper.Map(dto, entity);
            Repository.Update(entity);
            var res = await SaveAndGetSingle(entity);
            if (res.IsSuccess)
            {
                await AfterUpdate(dto, entity);
                res.Result = await Repository.FindSingleAndMapById<TSingleDto>(entity.Id);
            }

            return res;
        }

        public virtual async Task<Dictionary<string, LocalizablesDto>> GetLocalizationData(long id)
        {
            var data = await LocalizationDataService.GetDataFor<T>(id);
            return Mapper.Map(data, new Dictionary<string, LocalizablesDto>());
        }

        public virtual Task<SubmitResult> SetLocalizationData(long id, Dictionary<string, LocalizablesDto> data)
        {
            var locData = Mapper.Map(data, new Dictionary<string, LocalizablesData>());
            return LocalizationDataService.SetDataFor<T>(id, locData);

        }


        public async Task<SubmitResult> Handle(CrudEvent<T> command)
        {
            switch (command.Type)
            {
                case ActionType.Add:
                    await Merge(command.Data);
                    break;
                case ActionType.Update:
                    await Merge(command.Data);
                    break;
                case ActionType.Delete:
                    await Delete(command.Data.Id);
                    break;
            }
            return await DefaultUnit.SaveChangesAsync();
        }

        public virtual async Task<SubmitResult> Merge(T obj)
        {
            IEntity<long> ent = obj as IEntity<long>;
            if (await Repository.IdExists(ent.Id))
            {
                Repository.Update(obj);
            }
            else
            {
                Repository.Add(obj);
            }
            return await DefaultUnit.SaveChangesAsync();
        }

    }
}
