using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class ModalModel
    {
        public string ModalId { get; set; }
        public bool UseSearch { get; set; }
        public object Attrs { get; set; }
        public string AttrString { get { return RazorUtils.ToAttributeString(Attrs); } }
        public bool IsModal { get; set; }
        public string IsModalValue { get { return IsModal.ToString().ToLower(); } }
    }
}
