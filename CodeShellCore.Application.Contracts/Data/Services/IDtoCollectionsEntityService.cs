using CodeShellCore.Linq;

namespace CodeShellCore.Data.Services
{
    public interface IDtoCollectionsEntityService<T, TPrime, TOptionsDto, TListDto>
        where T : class, IModel<TPrime>
        where TListDto : class
        where TOptionsDto : LoadOptions
    {
        LoadResult<TListDto> GetCollection(string id, TOptionsDto opts);
    }
}
