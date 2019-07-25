using CodeShellCore.Moldster.Db.Dto;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Razor
{
    public class TemplateDataCollector
    {
        public string EntityName { get; set; }
        public List<ControlDTO> Controls { get; set; } = new List<ControlDTO>();

    }
}
