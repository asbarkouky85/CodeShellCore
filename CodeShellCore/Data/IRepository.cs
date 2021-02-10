using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using CodeShellCore.Data.Helpers;

namespace CodeShellCore.Data
{
    public interface IRepository
    {
        int Count();
        
        IEnumerable All();
        
        
    }

    
}
