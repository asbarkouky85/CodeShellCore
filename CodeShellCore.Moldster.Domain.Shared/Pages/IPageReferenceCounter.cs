using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageReferenceCounter : IModel<long>
    {
        int ReferencedBy { get; set; }
        int References { get; set; }
    }
}
