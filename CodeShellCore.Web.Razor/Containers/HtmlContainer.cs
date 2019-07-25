using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.Containers
{
    public class HtmlContainer : IDisposable
    {

        protected string Id;
        string _tag = "div";
        protected IHtmlHelper Helper;
        protected ViewContext Context { get { return Helper.ViewContext; } }
        public virtual string TagName { get { return _tag; } }
        protected virtual bool OpenTagInBaseConstructor { get { return true; } }
        protected bool Cancel;

        public HtmlContainer() { }

        public HtmlContainer(IHtmlHelper helper, string id, string tag = "div", object attributes = null)
        {
            _tag = tag;
            Helper = helper;

            Id = id;
            if (OpenTagInBaseConstructor)
                WriteOpeningTag(attributes);
        }

        public HtmlContainer(IHtmlHelper helper, bool cancelled)
        {
            Cancel = true;
        }

        public static void WriteContainerStart(ViewContext con, string tag, object attr, dynamic otherAttr = null)
        {
            if (tag != null)
            {
                string attrs = RazorUtils.ToAttributeString(attr);
                if (otherAttr != null)
                    attrs += RazorUtils.ToAttributeStringDynamic(otherAttr);
                con.Writer.Write(string.Format("<{0} {1}>\n", tag, attrs));
            }

        }

        protected virtual void WriteOpeningTag(object attr)
        {
            WriteContainerStart(Context, TagName, attr);
        }

        void IDisposable.Dispose()
        {
            WriteClosingTag();
        }

        protected virtual void WriteClosingTag()
        {
            if (!Cancel && TagName != null)
                Context.Writer.Write("</" + TagName + ">");
        }
    }
}
