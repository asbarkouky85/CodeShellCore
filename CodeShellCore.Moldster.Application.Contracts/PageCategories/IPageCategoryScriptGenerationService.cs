using System.Threading.Tasks;

namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryScriptGenerationService
    {
        Task GenerateBaseComponent(string templatePath);
        Task GeneratePageCategory(long id);
    }
}
