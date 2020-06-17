using CodeShellCore.Data;
using CodeShellCore.Helpers;
using Microsoft.EntityFrameworkCore;
using CodeShellCore.Data.ConfiguredCollections;
using Asga.Common.Services;
using CodeShellCore.Data.Lookups;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Asga.Common.Data
{
    public class AsgaRepository<T, TContext> : CollectionRepository<T, TContext,long>, INameableRepository<T> where T : class, IModel<long> where TContext : DbContext
    {
        public AsgaRepository(TContext con, AsgaCollectionService service) : base(con, service)
        {
        }

        public override void Add(T obj)
        {
            if (obj.Id == 0)
                obj.Id = Utils.GenerateID();
            base.Add(obj);
        }


        public IEnumerable<Named<long>> FindAsLookup(string collectionId = null)
        {
            var l = collectionId == null ? Loader : QueryCollection(collectionId);
            return QueryNamed(l).ToList();
        }
    }
}
