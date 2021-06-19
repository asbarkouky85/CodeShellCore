using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Localizables
{
    public interface IGetLocalizedDbContext
    {
        [DbFunction]
        string GetLocalized(string EntityType, long? EntityId, int LocaleId, string PropertyName, string @default);
    }
    public interface IHasLocalizablesDbContext : IGetLocalizedDbContext
    {
        DbSet<Localizable> Localizables { get; }


    }
}
