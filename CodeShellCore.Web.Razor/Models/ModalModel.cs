using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class ModalModel
    {
        public string ModalId { get; set; }
        public bool UseSearch { get; set; }
        public int Width { get; set; }
        public bool IsModal { get; set; }
        public string IsModalValue { get { return IsModal.ToString().ToLower(); } }
    }
}
