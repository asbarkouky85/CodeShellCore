using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Linq;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Collections;
using CodeShellCore.Data.Localization;
using CodeShellCore.Data.Events;
using CodeShellCore.MQ.Events;
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
        where TOptionsDto : LoadOptions
    {

        public virtual bool ProjectGetSingle => true;

        public DtoEntityService(IUnitOfWork unit) : base(unit)
        {

        }

        public virtual DeleteResult Delete(TPrime id)
        {
            var can = Repository.CanDeleteById(id);
            if (can.IsSuccess)
            {
                Repository.DeleteByKey(id);
            }
            else
            {
                return can;
            }

            return DefaultUnit.SaveChanges().MapToResult<DeleteResult>();
        }



        protected virtual void AfterUpdate(TUpdateDto dto, T entity)
        {

        }

        protected virtual void AfterCreate(TCreateDto dto, T entity)
        {

        }

        public virtual SubmitResult<TSingleDto> Post(TCreateDto dto)
        {
            var entity = Mapper.Map<TCreateDto, T>(dto);
            Repository.Add(entity);
            var res = SaveAndGetSingle(entity);
            if (res.IsSuccess)
            {
                AfterCreate(dto, entity);
            }
            return res;
        }

        protected virtual SubmitResult<TSingleDto> SaveAndGetSingle(T entity)
        {
            var res = DefaultUnit.SaveChanges().ToSubmitResult<TSingleDto>();
            if (res.IsSuccess)
            {
                res.Result = GetSingle(entity.Id);
            }
            return res;
        }

        public virtual SubmitResult<TSingleDto> Put(TUpdateDto dto)
        {
            var entity = GetSingleById(dto.Id);
            Mapper.Map(dto, entity);
            Repository.Update(entity);
            var res = SaveAndGetSingle(entity);
            if (res.IsSuccess)
            {
                AfterUpdate(dto, entity);
                res.Result = Repository.FindSingleAndMapById<TSingleDto>(entity.Id);
            }

            return res;
        }

        public virtual Dictionary<string, LocalizablesDTO> GetLocalizationData(long id)
        {
            return LocalizationDataService.GetDataFor<T>(id);
        }

        public virtual SubmitResult SetLocalizationData(long id, Dictionary<string, LocalizablesDTO> data)
        {
            return LocalizationDataService.SetDataFor<T>(id, data);
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
                    Delete(command.Data.Id);
                    break;
            }
            return await DefaultUnit.SaveChangesAsync();
        }

        public virtual async Task<SubmitResult> Merge(T obj)
        {
            IEntity<long> ent = obj as IEntity<long>;
            if (Repository.IdExists(ent.Id))
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
