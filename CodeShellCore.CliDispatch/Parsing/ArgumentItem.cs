using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using CodeShellCore.Types;
using CodeShellCore.Text.Localization;
using CodeShellCore.Helpers;

namespace CodeShellCore.CliDispatch.Parsing
{
    public abstract class ArgumentItem<T>
    {
        public string? CharacterSymbol { get; protected set; }
        public string? Key { get; protected set; }
        public int? Order { get; protected set; }
        public bool IsRequired { get; protected set; }
        public bool IsSet { get; protected set; }
        public string? MemberName { get; protected set; }
        public virtual bool IsBool { get; }
        public virtual bool IsEnum { get; }
        public string Description { get; set; }
        public string[] Options { get; protected set; }
        public T Item;

        protected ArgumentItem() { }

        public abstract void SetMemberValue(T obj, string v);
        public virtual string? GetDefault()
        {
            return null;
        }
    }

    public class ArgumentItem<T, TVal> : ArgumentItem<T>
    {
        protected Expression<Func<T, TVal>> _action;
        private bool _isBool;
        private bool _isEnum;
        public override bool IsBool => _isBool;
        public override bool IsEnum => _isEnum;
        protected TVal? Default;
        private bool _defaultIsSet = false;

        public ArgumentItem(
            Expression<Func<T, TVal>> t,
            string? key = null,
            string? shortKey = null,
            int? order = null,
            bool required = false) : base()
        {
            var memberExpression = t.Body as MemberExpression;
            CharacterSymbol = shortKey;
            _action = t;
            Key = key;
            Order = order;
            IsRequired = required;
            if (memberExpression != null)
            {
                MemberName = key ?? memberExpression.Member.Name;
                Description = LangUtils.CamelCaseToWords(memberExpression.Member.Name, " ");
            }
            _isBool = typeof(TVal).RealType() == typeof(bool);
            _isEnum = typeof(TVal).IsEnum;

            if (_isEnum)
            {
                List<string> opts = new List<string>();
                var baseType = typeof(TVal).BaseType;
                foreach (var item in Enum.GetValues(typeof(TVal)))
                {
                    if (baseType != null)
                        opts.Add($"{Convert.ChangeType(item, typeof(long))}:{Enum.GetName(typeof(TVal), item)}");
                }
                SetOptions(opts.ToArray());
            }
        }

        public virtual ArgumentItem<T, TVal> SetDefault(TVal val)
        {
            _defaultIsSet = true;
            Default = val;
            return this;
        }

        public ArgumentItem<T, TVal> SetOptions(string[] options)
        {
            Options = options;
            return this;
        }

        public override string? GetDefault()
        {
            if (_defaultIsSet)
                return Default?.ToString();
            return default(TVal)?.ToString();
        }

        public override void SetMemberValue(T obj, string v)
        {
            var b = _action.Body as MemberExpression;

            if (obj != null && b != null)
            {
                var mem = obj.GetType().GetProperty(b.Member.Name);
                var val = _getValue(v.Trim('"'));
                mem?.SetValue(obj, val);
            }
            IsSet = true;
        }

        private TVal _getValue(string value)
        {

            if (typeof(TVal).IsEnum)
            {
                if (int.TryParse(value, out int res))
                {
                    return (TVal)Enum.ToObject(typeof(TVal), res);
                }
                else
                {
                    try
                    {
                        var resEnum = Enum.Parse(typeof(TVal), value);
                        if (resEnum != null)
                            return (TVal)resEnum;
                    }
                    catch
                    {

                    }
                }
            }
            return value.ConvertTo<TVal>();
        }
    }
}
