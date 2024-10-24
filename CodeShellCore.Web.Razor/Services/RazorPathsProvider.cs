using System.Collections.Generic;
using System.IO;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Text;

namespace CodeShellCore.Web.Razor.Services
{
    public class RazorPathsProvider : ILayoutsService
    {
        IPathsService _pathsService;

        public RazorPathsProvider(IPathsService pathsService)
        {
            _pathsService = pathsService;
        }

        public List<LayoutFileDTO> GetLayouts(bool nameOnly = false)
        {
            var l = RazorConfig.Theme.BasePath.Replace("~/", "");
            var configPath = Path.Combine(_pathsService.ConfigRoot, l, "Layout");

            List<LayoutFileDTO> templateList = new List<LayoutFileDTO>();

            var templates = Directory.GetFiles(configPath);
            foreach (var temp in templates)
            {
                string name = Path.GetFileName(temp).GetBeforeLast(".");

                if (nameOnly)
                {
                    name = name.Replace("Layout", "");
                }
                else
                {
                    name = "Layout/" + name;
                }
                templateList.Add(new LayoutFileDTO
                {
                    Name = name
                });
            }
            return templateList;
        }
    }
}
