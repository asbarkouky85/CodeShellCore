using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public interface INameDescription<T> : INamed<T>
    {
        string Description { get; set; }
    }

    public interface IDescribedModel
    {
        long Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
