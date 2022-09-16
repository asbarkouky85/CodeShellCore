namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryScriptGenerationService
    {
        void GenerateBaseComponent(string templatePath);
        void GeneratePageCategory(long id);
    }
}
