using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Cli.Routing
{
    public abstract class ArgumentItem<T>
    {
        public char CharacterSymbol { get; protected set; }
        public string Key { get; protected set; }
        public bool IsRequired { get; protected set; }
        public bool IsSet { get; protected set; }
        public string MemberName { get; protected set; }

        public T Item;

        protected ArgumentItem() { }

        public abstract void WriteValue(T item, string v);
    }

    public class ArgumentItem<T, TVal> : ArgumentItem<T>
    {
        protected Expression<Func<T, TVal>> _action;
        public ArgumentItem(char ch, Expression<Func<T, TVal>> t, string key = null, bool required = false) : base()
        {
            CharacterSymbol = ch;
            _action = t;
            Key = key;
            IsRequired = required;
            MemberName = key ?? (t.Body as MemberExpression).Member.Name;
        }

        public override void WriteValue(T item, string v)
        {
            var b = _action.Body as MemberExpression;
            var mem = item.GetType().GetProperty(b.Member.Name);
            MemberName = Key ?? mem.Name;
            var val = v.ConvertTo(mem.PropertyType);
            mem.SetValue(item, val);
            IsSet = true;
        }
    }
}
