using CodeShellCore.Data.Lookups;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.Helpers
{
    public class EditingDTO<T> : EntityWrapperDto<T, long>, IEntityWrapperDto<T> where T : class
    {

    }
}
