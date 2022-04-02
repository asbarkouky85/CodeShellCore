using CodeShellCore.Moldster.Localization;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Pages
{
    public class PageCustomizationDTO
    {
        public long Id { get; set; }
        public string ViewPath { get; set; }
        public string Layout { get; set; }
        public string Display { get { return ViewPath + " ( " + TenantCode + " )"; } }
        public bool? Presistant { get; set; }
        public IEnumerable<PageControlListDTO> Controls { get; set; }
        public PageRouteDTO Route { get; set; }
        public IEnumerable<PageParameterEditDto> Parameters { get; set; }
        public IEnumerable<CustomFieldDto> Fields { get; set; }
        public long TenantId { get; set; }
        public string TenantCode { get; set; }
    }
}
