using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public interface IHasLocation
    {
        decimal? Longitude { get; set; }
        decimal? Latitude { get; set; }
    }
}
