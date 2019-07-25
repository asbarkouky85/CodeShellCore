using CodeShellCore.Data;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Services;
using CodeShellCore.Services.Http;
using CodeShellCore.Text;
using CodeShellCore.Types;
using Asga.Common.Data;
using Asga.Data;
using Asga.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace Asga.Services
{
    public class LookupsService<T> : ServiceBase where T : class, IUnitOfWork, IAsgaUnit
    {
        protected T Unit;
        public LookupsService(T unit)
        {
            Unit = unit;
            Unit.EnableJsonLoading();
        }

        public virtual object GetExternal(ExternalLookupQuery query) { return new { }; }

        public IEnumerable<Named<long>> GetLookupNamed<TObject>(string identifier) where TObject : class, IAsgaModel
        {
            IAsgaRepository<TObject> repo = Unit.GetAsgaRepositoryFor<TObject>();
            string collectionId = null;
            if (identifier.Contains("__"))
            {
                collectionId = identifier.GetAfterLast("__");
            }
            return repo.FindAsLookup(collectionId);
        }

        public IEnumerable<TObject> GetLookup<TObject>(string identifier) where TObject : class, IAsgaModel
        {
            IRepository<TObject> repo = Unit.GetRepositoryFor<TObject>();
            if (identifier.Contains("__"))
            {
                string collectionId = identifier.GetAfterLast("__");

                if (!(repo is IAsgaRepository<TObject>))
                    throw new CodeShellHttpException(HttpStatusCode.ServiceUnavailable, $"Repository must implement IAsgaRepository<{typeof(T).Name}> to use collections");

                var AsgaRepo = (IAsgaRepository<TObject>)repo;

                return AsgaRepo.GetCollectionList(collectionId);
            }
            else
            {
                return repo.Find(e => true);
            }
        }


        public IEnumerable<TResult> GetLookupAs<TObject, TResult>(string identifier, Expression<Func<TObject, TResult>> ex) where TObject : class where TResult : class
        {
            IRepository<TObject> repo = Unit.GetRepositoryFor<TObject>();
            if (identifier.Contains("__"))
            {
                string collectionId = identifier.GetAfterLast("__");

                if (!(repo is IAsgaRepository<TObject>))
                    throw new CodeShellHttpException(HttpStatusCode.ServiceUnavailable, $"Repository must implement IAsgaRepository<{typeof(T).Name}> to use collections");

                var AsgaRepo = (IAsgaRepository<TObject>)repo;

                return AsgaRepo.GetCollectionListAs(collectionId, ex);
            }
            else
            {
                return repo.FindAs(ex, d => true);
            }
        }

    }
}
