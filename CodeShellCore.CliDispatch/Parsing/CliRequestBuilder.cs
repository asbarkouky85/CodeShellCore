using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CodeShellCore.Cli;

namespace CodeShellCore.CliDispatch.Parsing
{
    internal class CliRequestBuilder<T> : ICliRequestBuilder<T>
    {
        private List<ArgumentItem<T>> _keys = new List<ArgumentItem<T>>();

        public IEnumerable<ArgumentItem<T>> KeyList => _keys;

        public ArgumentItem<T, TVal> Property<TVal>(Expression<Func<T, TVal>> t, string key, string shortKey = null, int? order = null, bool isRequired = false)
        {
            var item = new ArgumentItem<T, TVal>(t, key, shortKey, order, isRequired);
            _keys.Add(item);
            return item;
        }

        public void Document()
        {
            var keys = _keys.OrderBy(e => e.Order ?? 30).ToList();
            foreach (var item in keys)
            {
                var charSymb = item.CharacterSymbol != null ? "-" + item.CharacterSymbol + ", " : "";
                var ord = item.Order.HasValue ? "[" + item.Order.ToString() + "]" : "";
                var req = item.IsRequired ? " *" : "";
                var isBool = item.IsBool ? "" : "\t[value]";
                var def = item.GetDefault() == null ? "\t\t" : $"[def:({item.GetDefault()})]\t";
                Console.Write($"{ord}[{charSymb}--{item.Key}]{isBool}{req}{def}\t:\t");

                using (ColorSetter.Set(ConsoleColor.White))
                {
                    Console.WriteLine(item.Description);
                }

                if (item.Options != null)
                {
                    Console.WriteLine($"\t Options : [{string.Join(" - ", item.Options)}]");
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
