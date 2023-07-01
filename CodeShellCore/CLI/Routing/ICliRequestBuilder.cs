using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Cli.Routing
{
    public interface ICliRequestBuilder<T>
    {
        IEnumerable<ArgumentItem<T>> KeyList { get; }
        void Document();
        bool IsInvalid(out string[] res);
        void FillProperty<TVal>(Expression<Func<T, TVal>> t, string key, char? ch = null, int? order = null, bool isRequired = false);
    }
}