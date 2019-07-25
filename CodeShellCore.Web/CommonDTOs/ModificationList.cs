using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.CommonDTOs
{
    public class ModificationList<T> where T : class,IEditable
    {
        public IEnumerable<T> items { get; set; }
    }
}
