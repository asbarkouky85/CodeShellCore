using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

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
        public IUnitOfWork Unit { get; }
        public IKeyRepository<T, TPrime> Repository { get; private set; }
        public IObjectMapper Mapper { get; private set; }
        public DtoEntityService(IUnitOfWork unit)
        {
            Unit = unit;
            Repository = unit.GetRepositoryFor<T, TPrime>();
            Mapper = unit.ServiceProvider.GetService<IObjectMapper>();
        }

        public DeleteResult Delete(TPrime prime)
        {
            var can = Repository.CanDeleteById(prime);
            if (can.IsSuccess)
            {
                Repository.DeleteByKey(prime);
            }
            else
            {
                return can;
            }

            return Unit.SaveChanges().MapToResult<DeleteResult>();
        }

        public LoadResult<TListDto> Get(TOptionsDto opts)
        {
            var mapped = opts.GetOptionsFor<TListDto>();
            return Repository.FindAndMap(mapped);
        }

        public LoadResult<TListDto> GetCollection(string id, TOptionsDto opts)
        {
            var mapped = opts.GetOptionsFor<TListDto>();
            return Unit.GetCollectionRepositoryFor<T>().LoadCollectionAndMap(id, mapped);
        }

        public Dictionary<string, IEnumerable<Named<object>>> GetEditLookups(Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, IEnumerable<Named<object>>> GetListLookups(Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }

        public TSingleDto GetSingle(TPrime id)
        {
            return Repository.FindSingleAndMapById<TSingleDto>(id);
        }

        public bool IsUnique(PropertyUniqueDTO dto)
        {
            throw new NotImplementedException();
        }

        public SubmitResult<TSingleDto> Post(TCreateDto obj)
        {
            var entity = Mapper.Map<TCreateDto, T>(obj);
            Repository.Add(entity);
            var res = Unit.SaveChanges().MapToResult<SubmitResult<TSingleDto>>();
            if (res.IsSuccess)
            {
                res.Result = Repository.FindSingleAndMapById<TSingleDto>(entity.Id);
            }
            return res;
        }

        public SubmitResult<TSingleDto> Put(TUpdateDto obj)
        {
            var entity = Repository.FindSingleById(obj.Id);
            Mapper.Map(obj, entity);
            Repository.Update(entity);
            var res = Unit.SaveChanges().MapToResult<SubmitResult<TSingleDto>>();
            if (res.IsSuccess)
            {
                res.Result = Repository.FindSingleAndMapById<TSingleDto>(entity.Id);
            }
            
            return res;
        }
    }
}
