using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Localization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Localizables
{
    public class LocalizablesUnitOfWork<T> : UnitOfWork<T>, ILocalizablesUnitOfWork where T : DbContext, IGetLocalizedDbContext
    {
        public LocalizablesUnitOfWork(IServiceProvider provider) : base(provider)
        {
        }
    }
}
