using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Data
{
    public static class DbFunctions
    {
        [DbFunction]
        public static string GetLocalized(string EntityType, long EntityId, int LocaleId, string PropertyName, string def)
        {
            throw new NotImplementedException();
        }
    }
}
