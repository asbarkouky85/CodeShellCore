using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Cli.Routing.Internal
{
    internal class CliRequestBuilder<T> : ICliRequestBuilder<T>
    {
        private List<ArgumentItem<T>> _keys = new List<ArgumentItem<T>>();

        public IEnumerable<ArgumentItem<T>> KeyList => _keys;

        public ArgumentItem<T, TVal> FillProperty<TVal>(Expression<Func<T, TVal>> t, string key, char? ch = null, int? order = null, bool isRequired = false)
        {
            var item = new ArgumentItem<T, TVal>(t, key, ch, order, isRequired);
            _keys.Add(item);
            return item;
        }

        public void Document()
        {
            var keys = _keys.OrderBy(e => e.Order ?? 30).ToList();
            foreach (var item in keys)
            {
                var charSymb = item.CharacterSymbol.HasValue ? ("-" + item.CharacterSymbol) + ", " : "";
                var ord = item.Order.HasValue ? "[" + (item.Order).ToString() + "]" : "";
                var req = item.IsRequired ? " *" : "";
                var isBool = item.IsBool ? "" : " [value]";
                var def = item.GetDefault() == null ? "" : $"[def:({item.GetDefault()})]";
                Console.Write($"{ord}[{charSymb}--{item.Key}]{isBool}{req}{def}\t:\t");
                using (ColorSetter.Set(ConsoleColor.White))
                {
                    Console.WriteLine(item.Description);
                }
            }
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

        public void UseDefaultsForUnset(T subj)
        {
            var unset = KeyList.Where(e => !e.IsSet).ToList();
            foreach (var item in unset)
            {
                string val = item.GetDefault();
                if (val != null)
                {
                    item.SetMemberValue(subj, val);
                }
            }
        }
    }
}
