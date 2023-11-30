using CodeShellCore.Data;
using CodeShellCore.Data.Attachments;
using CodeShellCore.Data.Auditing;
using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static AutoMapper.Internal.ExpressionFactory;

namespace AutoMapper
{
    public static class MappingExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreId<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
            where TSource : class
            where TDestination : class, IEntity<long>
        {
            return expression.ForMember(e => e.Id, e => e.Ignore());
        }

        public static IMappingExpression<TSource, TDest> MapEntityWrapper<TSource, TDest, TDto>(
            this IMappingExpression<TSource, TDest> expression,
            Expression<Func<TDest, TDto>> exp)
            where TSource : class
            where TDto : class
            where TDest : class, IEntityWrapperDto<TDto>
        {
            return expression.ForMember(exp, e => e.MapFrom(d => d));
        }

        public static IMappingExpression<TSource, TDestination> MapChangeState<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
            where TSource : class
            where TDestination : class, IDetailObject<long>
        {
            return expression.ForMember(e => e.State, e => e.MapFrom(d => ChangeStates.Attached));
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAuditing<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
            where TSource : class
            where TDestination : class, IChangeColumns
        {
            return expression.ForMember(e => e.CreatedBy, e => e.Ignore())
                .ForMember(e => e.CreatedOn, e => e.Ignore());
        }

        public static IMappingExpression<TSource, TDestination> MapTempFile<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression,
            Expression<Func<TDestination, string>> tempFileExp)
            where TSource : class, IHasFileDto
        {
            return expression.ForMember(tempFileExp, d => d.MapFrom(e => e.File.Id));
        }

        public static IMappingExpression<TSource, TDestination> MapFilePath<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
            where TSource : class, IHasFileDto
            where TDestination : class, IAttachmentEntity
        {
            return expression.ForMember(e => e.FilePath, d => d.MapFrom(e => e.File.Id))
                .ForMember(e => e.FileName, e => e.MapFrom(d => d.File.FileName));
        }

        public static IMappingExpression<TSource, TDestination> MapTempFileDto<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
            where TSource : class, IAttachmentEntity
            where TDestination : class, IHasFileDto
        {
            return expression.ForMember(e => e.File, d => d.MapFrom(e => new TempFileDto(e.FilePath, e.FileName)));
        }

        public static void MapFromTempFile<TSource, TDestination, TMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TMember> memberOptions,
            Func<TSource, TempFileDto> tempFileExp)
        {
            memberOptions.Condition(e => tempFileExp.Invoke(e)?.FileTempPath != null && tempFileExp.Invoke(e)?.Id != null);
            memberOptions.MapFrom(e => tempFileExp.Invoke(e).Id.ToString());
        }

    }
}
