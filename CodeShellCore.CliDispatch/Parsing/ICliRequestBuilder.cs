using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.CliDispatch.Parsing
{
    public interface ICliRequestBuilder<T>
    {
        IEnumerable<ArgumentItem<T>> KeyList { get; }
        void Document();
        bool IsInvalid(out string[] res);
        ArgumentItem<T, TVal> Property<TVal>(Expression<Func<T, TVal>> t, string key, string ch = null, int? order = null, bool isRequired = false);
        void UseDefaultsForUnset(T subj);
    }
}