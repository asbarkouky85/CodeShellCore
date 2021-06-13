using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Localizables
{
    public interface ILocalizableDbContext
    {
        DbSet<Localizable> Localizables { get; }

        [DbFunction]
        string GetLocalized(string EntityType, long? EntityId, int LocaleId, string PropertyName, string @default);
    }
}
