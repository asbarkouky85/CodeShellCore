using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Validation
{
    public interface IValidator
    {
        string Attribute { get; }
        string ValidationMessage { get; }
    }
}
