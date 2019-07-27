using CodeShellCore.Data;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.MQ;
using CodeShellCore.MQ.Events;
using Asga.Data;
using System;
using System.Collections.Generic;

namespace Asga.Services
{
    public class AsgaEntityService<T> : CollectionsEntityService<T>, IEntityHandler<T> where T : class, IAsgaModel
    {
        protected IRepository<T> AsgaRepository { get { return Repository as IRepository<T>; } }

        public AsgaEntityService(ICollectionUnitOfWork unit) : base(unit) { }
        
        public virtual SubmitResult SaveChanges(IEnumerable<T> lst)
        {
            Repository.ApplyChanges(lst);
            return UnitOfWork.SaveChanges();
        }

        public virtual EditingDTO<T> GetSingleEditingDTO(object id)
        {
            return Repository.FindSingleAs(EditingDTO.GetExpression<T>(), id);
        }
    }
}
