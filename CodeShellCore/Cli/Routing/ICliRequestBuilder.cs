using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Cli.Routing
{
    public interface ICliRequestBuilder<T>
    {
        IEnumerable<ArgumentItem<T>> KeyList { get; }

        bool IsInvalid(out string[] res);
        void FillProperty<TVal>(Expression<Func<T, TVal>> t, char ch, string key = null, bool isRequired = false);
    }
}