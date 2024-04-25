using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public class NamedDto<TPrime> : INamed<TPrime>
    {
        public TPrime Id { get; set; }
        public string Name { get; set; }

        public static Expression<Func<T, NamedDto<TPrime>>> GetExpression<T>() where T : INamed<TPrime>
        {
            return e => new NamedDto<TPrime>
            {
                Id = e.Id,
                Name = e.Name
            };
        }
    }
}
