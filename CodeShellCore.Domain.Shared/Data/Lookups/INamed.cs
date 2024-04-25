using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public interface INamedModel
    {
        long Id { get; set; }
        string Name { get; set; }
    }
    public interface INamed
    {
         string Name { get; set; }
    }
    public interface INamed<T> : IEntity<T>, INamed
    {

    }
}
