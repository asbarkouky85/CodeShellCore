using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth
{
    public static class Auth
    {
        [DbFunction(Schema = "Auth")]
        public static string GetLocalized(string EntityType, long EntityId, int LocaleId, string PropertyName, string def)
        {
            throw new Exception();
        }
    }
}
