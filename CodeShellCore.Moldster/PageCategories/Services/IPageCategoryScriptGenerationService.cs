using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.PageCategories.Services
{
    public interface IPageCategoryScriptGenerationService
    {
        void GenerateBaseComponent(string templatePath);
        void GeneratePageCategory(long id);
    }
}
