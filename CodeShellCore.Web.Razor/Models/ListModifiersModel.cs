using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class ListModifiersModel
    {
        public string IdExpression { get; set; }
        public string EditFunction { get; set; }
        public string DetailsFunction { get; set; }
        public string DeleteFunction { get; set; }
        public IEnumerable<LinkModel> AdditionalButtons { get; set; }
        public string ModelExpression { get; set; }
        public string PermissionVariable { get; set; }
        public bool ReadOnly { get; set; }

        public bool ShowDelete { get { return Modifiers.Contains("R"); } }
        public bool ShowEdit { get { return Modifiers.Contains("E"); } }
        public bool ShowDetails { get { return Modifiers.Contains("D"); } }

        public string Classes { get; set; }

        public string Modifiers = "DER";

        public ListModifiersModel()
        {
            AdditionalButtons = new List<LinkModel>();
        }
    }
}
