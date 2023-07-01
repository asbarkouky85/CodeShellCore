using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Data.Mapping
{
    public interface IObjectMapper
    {
        TDestination Map<TSource, TDestination>(TSource src);
        TDestination Map<TSource, TDestination>(TSource src, TDestination tar);

    }
}
