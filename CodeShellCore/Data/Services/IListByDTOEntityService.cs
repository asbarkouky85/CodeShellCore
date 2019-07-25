using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.Services
{
    public interface IListByDTOEntityService<T,TDTO> : IServiceBase where T :class where TDTO : class,IDTO<T>
    {
        Expression<Func<T, TDTO>> ListDTOExpression { get; }
    }
}
