using CodeShellCore.Moldster.Localization;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.PageCategories
{
    public class TemplateDataCollector
    {
        public string EntityName { get; set; }
        public List<ControlRenderDto> Controls { get; set; } = new List<ControlRenderDto>();
        public List<PageCategoryParameterDto> Parameters { get; set; } = new List<PageCategoryParameterDto>();
        public LocalizationDataCollector Localization { get; set; }
    }
}
