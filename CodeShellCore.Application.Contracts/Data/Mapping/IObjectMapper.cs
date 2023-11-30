using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Data.Mapping
{
    public interface IObjectMapper
    {
        TDestination Map<TSource, TDestination>(TSource src);
        object Map(object src, Type sourceType, Type destType);
        TDestination Map<TSource, TDestination>(TSource src, TDestination tar);

    }
}
