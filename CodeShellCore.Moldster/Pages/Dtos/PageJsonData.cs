using CodeShellCore.Moldster.Razor;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Pages.Dtos
{
    public class PageJsonData
    {
        public ViewParams ViewParams { get; set; }
        public Dictionary<string, string> Sources { get; set; }
    }
}
