using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Localization;
using CodeShellCore.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Localizables
{
    public class LocalizablesUnitOfWork<T> : UnitOfWork<T>, ILocalizablesUnitOfWork where T : CodeShellDbContext<T>, IGetLocalizedDbContext
    {
        public LocalizablesUnitOfWork(IServiceProvider provider) : base(provider)
        {
        }
    }
}
