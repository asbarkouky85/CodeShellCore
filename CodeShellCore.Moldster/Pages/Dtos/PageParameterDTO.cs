using CodeShellCore.Data;
using CodeShellCore.Localization;
using CodeShellCore.Text;
using System;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Pages.Dtos
{
    [EntityName("PageParameter")]
    public class PageParameterDTO : EntityDto<long>
    {
        public PageParameter Entity { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeString { get { return TypeEnum.GetString(); } }
        public PageParameterTypes TypeEnum => (PageParameterTypes)Type;
        public string DefaultValue { get; set; }
        public string ViewPath { get; set; }
    }
}
