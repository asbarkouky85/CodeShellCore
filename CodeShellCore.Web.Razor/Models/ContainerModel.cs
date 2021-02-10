using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class ContainerModel
    {
        public string ContainerId { get; set; }
        public string ActivationProperty { get; set; }
        public string TitleTextId { get; set; }
        public object Attributes { get; set; }
        public string AttributeString { get { return Attributes == null ? "" : RazorUtils.ToAttributeString(Attributes); } }
    }
}
