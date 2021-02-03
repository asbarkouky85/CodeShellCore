using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.MQ;
using System.Collections.Generic;

namespace Asga.Auth.Services
{
    public class AsgaEntityService<T> : EntityService<T>, IEntityHandler<T>
        where T : class, IAuthModel
    {

        public AsgaEntityService(IUnitOfWork unit) : base(unit) { }

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
