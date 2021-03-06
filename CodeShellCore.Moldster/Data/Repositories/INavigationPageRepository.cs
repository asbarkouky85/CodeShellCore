﻿using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Data.Repositories
{
    public interface INavigationPageRepository : IRepository<NavigationPage>
    {
        LoadResult<NavigationPageListDTO> GetUnderNave(long navId, LoadOptions opt);
        void SetDisplayOrder(long naveId);
    }
}
