using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Text.Localization
{
    public interface ILocalizableDbContext: IDisposable, IInfrastructure<IServiceProvider>, IDbContextDependencies, IDbSetCache, IDbQueryCache, IDbContextPoolable
    {
        //[DbFunction]
        string GetLocalized(string EntityType, long EntityId, int LocaleId, string PropertyName, string def);


        [DbFunction]
        string GetNameChain(string EntityType, int LocaleId, string chain);

    }
}
