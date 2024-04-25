using AutoMapper;
using AutoMapper.QueryableExtensions;
using CodeShellCore.Data.MemberReplacement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
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

        public object Map(object src, Type sourceType, Type destType)
        {
            return _mapper.Map(src, sourceType, destType);
        }

        public virtual IQueryable<TDestination> Project<TSource, TDestination>(IQueryable<TSource> query)
        {
            return _mapper.ProjectTo<TDestination>(query);
        }

        public IQueryable<TEntity> ReplaceMembers<TEntity>(IQueryable<TEntity> query, Action<MemberReplacementConfiguration<TEntity>> replacementAction)
        {
            var c = new MemberReplacementConfiguration<TEntity>();
            replacementAction.Invoke(c);
            var conf = new MapperConfiguration(e =>
            {
                var m = e.CreateMap<TEntity, TEntity>();
                foreach (var replacer in c.GetReplaces())
                {
                    m = m.ForMember(replacer.Item1, e => e.MapFrom(replacer.Item2));
                }
            });
            return query.ProjectTo<TEntity>(conf);
        }
    }
}
