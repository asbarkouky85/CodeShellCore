using CodeShellCore.Linq;

namespace Asga.Public.Services
{
    public interface IAboutSectionService
    {
        LoadResult<AboutSection> LoadLocalized(LoadOptions opts);
    }
}