using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster
{
    public enum PageParameterTypes
    {
        Text = 1,
        Embedded = 2,
        PageLink = 3,
        Modal = 4
    }

    public enum PageTypes
    {
        All,
        AnyRoutable,
        ParameterizedRoutable,
        UnParameterizedRoutable,
        Embedded
    }

    public enum ParameterRequestTypes
    {
        InvalidLinks,
        MissingLinks
    }

    public enum TextTypes
    {
        Words = 1,
        Columns = 2,
        Messages = 3,
        Pages = 4
    }
}
