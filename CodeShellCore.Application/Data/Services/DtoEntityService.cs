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

namespace CodeShellCore.Data.Services
{
    public class DtoEntityService<T, TPrime, TOptionsDto, TListDto, TSingleDto, TCreateDto, TUpdateDto> : IDtoEntityService<TPrime, TOptionsDto, TListDto, TSingleDto, TCreateDto, TUpdateDto>
        where T : class, IModel<TPrime>
        where TSingleDto : class
        where TListDto : class
        where TCreateDto : class
        where TUpdateDto : class, IEntityDto<TPrime>
        where TOptionsDto : LoadOptions
    {
        public IUnitOfWork DefaultUnit { get; }
        public IKeyRepository<T, TPrime> Repository { get; private set; }
        public IObjectMapper Mapper { get; private set; }
        public ILookupsService LookupsService { get; private set; }
        public virtual bool ProjectGetSingle => true;

        public DtoEntityService(IUnitOfWork unit)
        {
            DefaultUnit = unit;
            Repository = unit.GetRepositoryFor<T, TPrime>();
            Mapper = unit.ServiceProvider.GetService<IObjectMapper>();
            LookupsService = unit.ServiceProvider.GetService<ILookupsService>();
        }

        public virtual DeleteResult Delete(TPrime id)
        {
            Repository.DeleteByKey(id);
            return DefaultUnit.SaveChanges().MapToResult<DeleteResult>();
        }

        public virtual LoadResult<TListDto> Get(TOptionsDto options)
        {
            var mapped = options.GetOptionsFor<TListDto>();
            return Repository.FindAndMap(mapped);
        }

        public virtual LoadResult<TListDto> GetCollection(string id, TOptionsDto options)
        {
            var mapped = options.GetOptionsFor<TListDto>();
            return DefaultUnit.GetCollectionRepositoryFor<T>().LoadCollectionAndMap(id, mapped);
        }

        public virtual Dictionary<string, IEnumerable<Named<object>>> GetEditLookups(Dictionary<string, string> dto)
        {
            return LookupsService.GetRequestedLookups(dto);
        }

        public virtual Dictionary<string, IEnumerable<Named<object>>> GetListLookups(Dictionary<string, string> dto)
        {
            return LookupsService.GetRequestedLookups(dto);
        }

        public virtual TSingleDto GetSingle(TPrime id)
        {
            var item = GetSingleById(id);
            return Mapper.Map<T, TSingleDto>(item);
        }

        public virtual bool IsUnique(IsUniqueDto dto)
        {
            var id = dto.Id.ConvertTo<TPrime>();
            var exp = Expressions.Unique<T, TPrime>(id, dto.Property, dto.Value);
            return !Repository.Exist(exp);
        }

        protected virtual void AfterUpdate(TUpdateDto dto, T entity)
        {

        }

        protected virtual void AfterCreate(TCreateDto dto, T entity)
        {

        }

        protected virtual T GetSingleById(TPrime id)
        {
            return Repository.FindSingleById(id);
        }

        public virtual SubmitResult<TSingleDto> Post(TCreateDto dto)
        {
            var entity = Mapper.Map<TCreateDto, T>(dto);
            Repository.Add(entity);
            var res = DefaultUnit.SaveChanges().ToSubmitResult<TSingleDto>();
            if (res.IsSuccess)
            {
                AfterCreate(dto, entity);
                res.Result = Repository.FindSingleAndMapById<TSingleDto>(entity.Id);
            }
            return res;
        }

        public virtual SubmitResult<TSingleDto> Put(TUpdateDto dto)
        {
            var entity = GetSingleById(dto.Id);
            Mapper.Map(dto, entity);
            Repository.Update(entity);
            var res = DefaultUnit.SaveChanges().ToSubmitResult<TSingleDto>();
            if (res.IsSuccess)
            {
                AfterUpdate(dto, entity);
                res.Result = Repository.FindSingleAndMapById<TSingleDto>(entity.Id);
            }

            return res;
        }
    }
}
