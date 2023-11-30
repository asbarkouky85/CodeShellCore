using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Linq
{
    public static class ApplicationSharedLinqExtensions
    {
        public static void PresistState<T, TPrime>(this IEnumerable<EntityWrapperDto<T, TPrime>> lst)
            where T : class, IEditable
        {
            lst.ForEach(d => d.Entity.State = d.State);
        }

    }
}
