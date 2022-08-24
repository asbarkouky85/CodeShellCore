using CodeShellCore.Moldster;
using CodeShellCore.Types;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.Elements.Moldster
{
    public static class MoldsterElementHelperExtensions 
    {
        
        public static IHtmlContent CustomFieldGroup<T>(this IHtmlHelper<T> helper, FieldDefinition define, Accessibility acc, string modelName = null)
        {
            if (!acc.Read)
                return null;

            ControlGroupWriter mod = new ControlGroupWriter(helper);
            mod.UseProperty(define.Name, typeof(T).GetEntityName() + "__" + define.Name, define.Name, false);

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
