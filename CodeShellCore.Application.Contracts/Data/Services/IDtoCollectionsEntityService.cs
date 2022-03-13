using CodeShellCore.Linq;

namespace CodeShellCore.Data.Services
{
    public interface IDtoCollectionsEntityService<TPrime, TOptionsDto, TListDto>
        where TListDto : class
        where TOptionsDto : LoadOptions
    {
        LoadResult<TListDto> GetCollection(string id, TOptionsDto opts);
    }
}
