﻿using CodeShellCore.Data;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.MQ.Events;
using CodeShellCore.Services;
using CodeShellCore.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CodeShellCore.Data.Services
{
    public class EntityService<T> : ServiceBase, IEntityService<T> where T : class
    {
        protected virtual IUnitOfWork UnitOfWork { get; private set; }
        public virtual IRepository<T> Repository { get { return UnitOfWork.GetRepositoryFor<T>(); } }
        public virtual ICollectionRepository<T> CollectionRepository => UnitOfWork.GetCollectionRepositoryFor<T>();

        public EntityService(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }

        public LoadResult<T> Load(LoadOptions opts)
        {
            UnitOfWork.EnableJsonLoading();
            ListOptions<T> op = opts.GetOptionsFor<T>();
            return Repository.Find(op);
        }

        public virtual T GetSingle(object id)
        {
            UnitOfWork.EnableJsonLoading();
            return Repository.FindSingle(id);
        }


        public virtual SubmitResult Create(T obj)
        {
            Repository.Add(obj);
            var data = UnitOfWork.SaveChanges();
            data.Data["Id"] = obj.GetPropertyValue("Id");
            return data;
        }

        public virtual SubmitResult Update(T obj)
        {
            Repository.Update(obj);
            return UnitOfWork.SaveChanges();
        }

        public virtual SubmitResult Merge(T obj)
        {
            if (obj is IModel<long>)
            {
                IModel<long> ent = obj as IModel<long>;
                if (Repository.IdExists(ent.Id))
                {
                    return Update(obj);
                }
                else
                {
                    return Create(obj);
                }
            }
            else
            {
                Repository.Merge(obj);
                return UnitOfWork.SaveChanges();
            }

        }

        public object GetSingleObject(object id)
        {
            return GetSingle(id);
        }

        public SubmitResult Create(string obj)
        {
            T item = obj.FromJson<T>();
            return Create(item);
        }

        public SubmitResult Update(string obj)
        {
            T item = obj.FromJson<T>();
            return Update(item);
        }

        public virtual DeleteResult DeleteById(object prime)
        {
            Repository.DeleteById(prime);
            SubmitResult submitResult = UnitOfWork.SaveChanges();
            DeleteResult deleteResult = submitResult.MapTo<DeleteResult>();
            deleteResult.CanDelete = submitResult.Code == 0;
            return deleteResult;
        }

        public DeleteResult Delete(T entity)
        {
            Repository.Delete(entity);
            var x = UnitOfWork.SaveChanges().MapTo<DeleteResult>();
            x.CanDelete = x.Code == 0;
            return x;
        }

        public SubmitResult Handle(CrudEvent<T> command)
        {
            switch (command.Type)
            {
                case ActionType.Add:
                    Merge(command.Data);
                    break;
                case ActionType.Update:
                    Merge(command.Data);
                    break;
                case ActionType.Delete:
                    Delete(command.Data);
                    break;
            }
            return UnitOfWork.SaveChanges();

        }

        public bool IsUnique(PropertyUniqueDTO dto)
        {
            var exp = Expressions.Unique<T, long>(dto.Id, dto.Property, dto.Value);
            return !Repository.Exist(exp);
        }

        public virtual DeleteResult CanDelete(object id)
        {
            return Repository.CanDelete(id);
        }

        public virtual LoadResult LoadObjects(LoadOptions opts)
        {
            return Load(opts);
        }

        public LoadResult<TDTO> LoadDTO<TDTO>(System.Linq.Expressions.Expression<Func<T, TDTO>> ex, LoadOptions opts) where TDTO : class
        {
            var op = opts.GetOptionsFor<TDTO>();
            return Repository.FindAs(ex, op);
        }

        public virtual LoadResult<T> LoadCollection(string collectionId, LoadOptions opts)
        {
            if (string.IsNullOrEmpty(collectionId))
                return CollectionRepository.Find(opts.GetOptionsFor<T>());
            return CollectionRepository.LoadCollection(collectionId, opts.GetOptionsFor<T>());
        }

        public virtual LoadResult<TDto> LoadCollectionAs<TDto>(string collectionId, Expression<Func<T, TDto>> ex, LoadOptions opts) where TDto : class
        {
            if (string.IsNullOrEmpty(collectionId))
                return CollectionRepository.FindAs(ex, opts.GetOptionsFor<TDto>());
            return CollectionRepository.LoadCollectionAs(collectionId, ex, opts.GetOptionsFor<TDto>());
        }
    }
}
