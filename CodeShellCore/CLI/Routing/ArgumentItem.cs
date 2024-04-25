using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using CodeShellCore.Types;
using CodeShellCore.Text.Localization;

namespace CodeShellCore.Cli.Routing
{
    public abstract class ArgumentItem<T>
    {
        public char? CharacterSymbol { get; protected set; }
        public string Key { get; protected set; }
        public int? Order { get; protected set; }
        public bool IsRequired { get; protected set; }
        public bool IsSet { get; protected set; }
        public string MemberName { get; protected set; }
        public virtual bool IsBool { get; }
        public string Description { get; set; }
        public T Item;

        protected ArgumentItem() { }

        public abstract void SetMemberValue(T obj, string v);
        public abstract void UseDefaultValue(T requestInstance);

    }

    public class ArgumentItem<T, TVal> : ArgumentItem<T>
    {
        protected Expression<Func<T, TVal>> _action;
        private bool _isBool;
        public override bool IsBool => _isBool;
        protected TVal Default;
        

        public ArgumentItem(Expression<Func<T, TVal>> t, string key = null, char? ch = null, int? order = null, bool required = false) : base()
        {
            var memberExpression = t.Body as MemberExpression;
            CharacterSymbol = ch;
            _action = t;
            Key = key;
            Order = order;
            IsRequired = required;
            MemberName = key ?? memberExpression.Member.Name;
            Description = LangUtils.CamelCaseToWords(memberExpression.Member.Name, " ");
            _isBool = typeof(TVal).RealType() == typeof(bool);
        }

        public virtual ArgumentItem<T, TVal> SetDefault(TVal val)
        {
            Default = val;
            return this;
        }

        public override void SetMemberValue(T obj, string v)
        {
            var b = _action.Body as MemberExpression;
            var mem = obj.GetType().GetProperty(b.Member.Name);
            MemberName = Key ?? mem.Name;
            var val = v.ConvertTo(mem.PropertyType);
            mem.SetValue(obj, val);
            IsSet = true;
        }

        public void SetMemberValue(T obj, TVal val)
        {
            var b = _action.Body as MemberExpression;
            var mem = obj.GetType().GetProperty(b.Member.Name);
            MemberName = Key ?? mem.Name;
            mem.SetValue(obj, val);
            IsSet = true;
        }

        public override void UseDefaultValue(T requestInstance)
        {
            SetMemberValue(requestInstance, Default);
        }
    }
}
