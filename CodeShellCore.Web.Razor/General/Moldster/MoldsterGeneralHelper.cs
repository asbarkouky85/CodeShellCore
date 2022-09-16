using CodeShellCore.Moldster;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq.Expressions;

namespace CodeShellCore.Web.Razor.General.Moldster
{
    public class MoldsterGeneralHelper : DefaultGeneralHelper, IMoldsterGeneralHelper
    {
        private IdentifierProcessor proc = new IdentifierProcessor();
        private ParameterProcessor par = new ParameterProcessor();

        public override IHtmlContent TabTitle(IHtmlHelper helper, string containerId, string activationVariable, string textId, IHtmlContent content, object attr)
        {
            if (!helper.GetAccessibility(containerId).Read)
                return null;
            return base.TabTitle(helper, containerId, activationVariable, textId, content, attr);
        }

        public override void AddHeaderButton(IHtmlHelper helper, IHtmlContent content = null, string function = null, string url = null, BtnClass btn = BtnClass.Default, string icon = null, string identifier = null, string classes = null, string title = null, object attr = null)
        {
            if (identifier != null)
            {
                var acc = proc.Process(helper, identifier, "HeaderButton");
                if (!acc.Read)
                    return;
            }
            base.AddHeaderButton(helper, content, function, url, btn, icon, identifier, classes, title, attr);
        }

        public override IHtmlContent Button(IHtmlHelper helper, string text, string function, string url, BtnClass btn, string icon, string identifier, IHtmlContent content, string classes, string title, object attr)
        {
            if (identifier != null)
            {
                helper.AddToViewControls(new ControlRenderDto
                {
                    Identifier = identifier?.ToLower(),
                    ControlType = "Button"
                });
                if (!helper.GetAccessibility(identifier?.ToLower()).Read)
                    return null;
            }
            return base.Button(helper, text, function, url, btn, icon, identifier, content, classes, title, attr);
        }

        public virtual string GetLink<T, TValue>(IHtmlHelper<T> helper, string id, Expression<Func<T, TValue>> idProperty, string def)
        {
            string prop = RazorUtils.GetMemberName(idProperty);
            var link = new PageLink(id, def, null, prop);
            return GetLink(helper, link);
        }

        public virtual string GetLink(IHtmlHelper helper, PageLink link)
        {
            par.Process(helper, link);
            var val = helper.GetViewParams().GetFromOther(link.Name, link.DefaultValue);

            if (string.IsNullOrEmpty(val))
                return null;

            var nam = helper.GetService<INamingConventionService>();
            val = nam.ApplyConvension(val, AppParts.Route);
            string l = "'/" + val + "/'";
            string id = "";
            if (link.IdExpression != null)
                id = "+" + link.IdExpression;
            else if (link.IdProperty != null)
                id = "+" + helper.GetModelName() + "." + link.IdProperty;
            return l + id;
        }

        public virtual IHtmlContent ComponentSelectorFromOther(IHtmlHelper helper, string id, string def = null, object attr = null)
        {
            string res = "";

            var acc = proc.Process(helper, id, "ComponentSelector");
            par.Process(helper, id, PageParameterTypes.Embedded, def);
            if (!acc.Read)
                return null;
            string componentPath = helper.GetViewParams().GetFromOther(id, def);
            var nameService = helper.GetService<INamingConventionService>();
            id = id != "none" ? "#" + id : null;

            if (!string.IsNullOrEmpty(componentPath))
            {
                componentPath = nameService.GetComponentSelector(componentPath.GetAfterLast("/"));
                string attrString = attr == null ? "" : RazorUtils.ToAttributeString(attr);
                res = $"<{componentPath} {id} [IsEmbedded]='true'{attrString}></{componentPath}>";
            }
            return new HtmlString(res);
        }

        public void AddModal(IHtmlHelper helper, string id, string def)
        {
            par.Process(helper, id, PageParameterTypes.Modal, def);

        }

        public void AddParameter(IHtmlHelper helper, string id, string def)
        {
            par.Process(helper, id, PageParameterTypes.Text, def);
        }
    }
}
