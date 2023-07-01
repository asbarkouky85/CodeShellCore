using CodeShellCore.Data.Lookups;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.Helpers
{
    public class EditingDTO
    {
        public static Expression<Func<T, EditingDTO<T>>> GetExpression<T>() where T : class, IModel<long>
        {

            return d => new EditingDTO<T> { Entity = d, Id = d.Id };
        }
    }

    public interface IEditingDto<EntityDto>
    {
        EntityDto Entity { get; set; }
    }

    public class EditingDTO<T> : DTO<T, long> where T : class
    {

    }
}
