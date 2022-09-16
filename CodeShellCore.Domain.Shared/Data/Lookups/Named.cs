using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public class Named<TPrime> : INamed<TPrime>
    {
        public TPrime Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<T, Named<TPrime>>> GetExpression<T>() where T : INamed<TPrime>
        {
            return e => new Named<TPrime>
            {
                Id = e.Id,
                Name = e.Name
            };
        }
    }
}
