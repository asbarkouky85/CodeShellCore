using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Cli.Routing.Internal
{
    internal class CliRequestBuilder<T> : ICliRequestBuilder<T>
    {
        private List<ArgumentItem<T>> _keys = new List<ArgumentItem<T>>();

        public IEnumerable<ArgumentItem<T>> KeyList => _keys;

        public void FillProperty<TVal>(Expression<Func<T, TVal>> t, string key, char? ch = null, int? order = null, bool isRequired = false)
        {
            var item = new ArgumentItem<T, TVal>(t, key, ch, order, isRequired);
            _keys.Add(item);
        }

        public bool IsInvalid(out string[] res)
        {
            var lst = new List<string>();
            var invalid = false;
            foreach (var n in _keys)
            {
                if (n.IsRequired && !n.IsSet)
                {
                    lst.Add("[" + n.MemberName + "] is required (ex: .. -" + n.CharacterSymbol + " [" + n.MemberName + "])");
                    invalid = true;
                }
            }
            res = lst.ToArray();
            return invalid;
        }
    }
}
