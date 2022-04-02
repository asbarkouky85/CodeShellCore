using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapper
{
    public static class MappingExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreId<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
            where TSource : class
            where TDestination : class, IModel<long>
        {
            return expression.ForMember(e => e.Id, e => e.Ignore());
        }

        public static IMappingExpression<TSource, TDestination> MapChangeState<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
            where TSource : class
            where TDestination : class, IEditable<long>
        {
            return expression.ForMember(e => e.State, e => e.MapFrom(d => ChangeStates.Attached));
        }
    }
}
