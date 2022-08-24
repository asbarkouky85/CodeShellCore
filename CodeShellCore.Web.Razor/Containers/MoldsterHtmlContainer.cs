using CodeShellCore.Moldster.PageCategories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.Containers
{
    public class MoldsterHtmlContainer : HtmlContainer
    {
        public ControlRenderDto Control { get; set; }

        public MoldsterHtmlContainer(IHtmlHelper cont, string tag, string identifier, object tableAttrs = null) : base(cont, identifier, tag, tableAttrs)
        {
            Control = new ControlRenderDto
            {
                Identifier = identifier?.ToLower(),
                ControlType = "Container"
            };
        }

        public MoldsterHtmlContainer(IHtmlHelper cont, string identifier, bool cancel) : base(cont, cancel)
        {
            Control = new ControlRenderDto
            {
                Identifier = identifier?.ToLower(),
                ControlType = "Container"
            };
        }
    }
}
