using CodeShellCore.Data;
using CodeShellCore.Linq;

namespace Asga.Public.Data
{
    public interface IHomeSlideRepository : IRepository<HomeSlide>
    {
        LoadResult<HomeSlide> GetLocalized(LoadOptions opts, string collectionId = null);
    }
}