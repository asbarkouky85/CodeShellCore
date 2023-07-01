using CodeShellCore.Text.Localization;
using CodeShellCore.Text.TextProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Text
{
    public class MvcTextProvider : ResxTextProvider, IRazorLocaleTextProvider
    {
        public MvcTextProvider(Language lang) : base(lang)
        {
        }
    }
}
