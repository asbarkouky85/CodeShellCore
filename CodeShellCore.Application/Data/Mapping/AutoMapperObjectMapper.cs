using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Data.Mapping
{
    public class AutoMapperObjectMapper : IObjectMapper, IQueryProjector
    {
        private IMapper _mapper;
        public AutoMapperObjectMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public virtual TTarget Map<TSource, TTarget>(TSource src)
        {
            return _mapper.Map<TSource, TTarget>(src);
        }

        public virtual TTarget Map<TSource, TTarget>(TSource src, TTarget tar)
        {
            return _mapper.Map(src, tar);
        }

        public virtual IQueryable<TDestination> Project<TSource, TDestination>(IQueryable<TSource> query)
        {
            return _mapper.ProjectTo<TDestination>(query);
        }
    }
}
