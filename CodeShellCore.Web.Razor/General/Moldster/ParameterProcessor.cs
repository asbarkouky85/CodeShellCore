using CodeShellCore.Moldster;
using CodeShellCore.Moldster.PageCategories.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.General.Moldster
{
    public class ParameterProcessor
    {
        public void Process(IHtmlHelper helper, string key, PageParameterTypes type, string value)
        {
            if (helper.CollectingData())
            {
                helper.GetCollector().Parameters.Add(new PageCategoryParameterDTO { Name = key, Type = (int)type, DefaultValue = value });
            }
        }



        public void Process(IHtmlHelper helper, PageLink link)
        {
            if (helper.CollectingData())
            {
                helper.GetCollector().Parameters.Add(new PageCategoryParameterDTO
                {
                    Name = link.Name,
                    Type = (int)PageParameterTypes.PageLink,
                    DefaultValue = link.DefaultValue
                });
            }
        }
    }

}
