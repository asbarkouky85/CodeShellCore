using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public interface ICollectionEntityService<T> where T : class
    {
        LoadResult<T> LoadCollection(string collectionId, LoadOptions opts);
        LoadResult<TDto> LoadCollectionAs<TDto>(string collectionId, Expression<Func<T, TDto>> ex, LoadOptions opts) where TDto : class;
    }
}
