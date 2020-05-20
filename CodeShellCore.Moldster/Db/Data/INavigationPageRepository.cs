using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Db.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface INavigationPageRepository : IRepository<NavigationPage>
    {
        LoadResult<NavigationPageListDTO> GetUnderNave(long navId, LoadOptions opt);
        void SetDisplayOrder(long naveId);
    }
}
