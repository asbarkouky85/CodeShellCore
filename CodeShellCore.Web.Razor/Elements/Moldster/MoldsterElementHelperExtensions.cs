using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Db.Razor;
using CodeShellCore.Types;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.Elements.Moldster
{
    public static class MoldsterElementHelperExtensions 
    {
        
        public static IHtmlContent CustomFieldGroup<T>(this IHtmlHelper<T> helper, FieldDefinition define, CodeShellCore.Moldster.Razor.Accessibility acc, string modelName = null)
        {
            if (!acc.Read)
                return null;

            ControlGroupWriter mod = new ControlGroupWriter(helper);
            mod.UseProperty(define.Name, typeof(T).RealModelType().Name + "__" + define.Name, define.Name, false);

            if (!acc.Write)
            {
                mod.InputModel.NgModelName = (modelName ?? helper.GetModelName()) + ".extra.data";
                return mod.WriteLabel();
            }

            if (modelName != null)
                mod.InputModel.NgModelName = modelName;
            return mod.Write("CustomField");
        }

        

        
    }
}
