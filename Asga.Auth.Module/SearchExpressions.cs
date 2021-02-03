using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth
{
    public static class SearchExpressions
    {

        public static void RegisterExpressions()
        {
            ExpressionStore.RegisterSearchExpression<User>(term => e =>
                e.Name.Contains(term) || e.Email.Contains(term) || e.Mobile.Contains(term)
            );
        }
    }
}
