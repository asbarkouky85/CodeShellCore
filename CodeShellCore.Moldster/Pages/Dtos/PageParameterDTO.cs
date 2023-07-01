using CodeShellCore.Data;
using CodeShellCore.Text;
using System;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Pages.Dtos
{
    public class PageParameterDTO : DTO<PageParameter, long>
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeString { get { return TypeEnum.GetString(); } }
        public PageParameterTypes TypeEnum => (PageParameterTypes)Type;
        public string DefaultValue { get; set; }
        public string ViewPath { get; set; }
    }
}
