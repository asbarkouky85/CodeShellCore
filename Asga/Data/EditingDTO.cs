using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Asga.Data
{
    public static class EditingDTO
    {
        public static Expression<Func<T,EditingDTO<T>>> GetExpression<T>() where T:class,IAsgaModel
        {

            return d=>new EditingDTO<T> { Entity=d,Id=d.Id};
        }
    }
    public class EditingDTO<T> : DTO<T, long> where T : class
    {

    }
}
