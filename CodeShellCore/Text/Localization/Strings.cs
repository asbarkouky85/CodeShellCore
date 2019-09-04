using CodeShellCore.Text;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Text.Localization
{
    public class Strings
    {
        private static readonly ILocaleTextProvider _provider;

        static Strings()
        {
            _provider = Shell.RootInjector.GetService<ILocaleTextProvider>();
        }


        public static string Word(string index, string cult = null)
        {
            if (_provider == null)
                return index;
            return _provider.Word(index, cult);
        }

        public static string Word(Enum index, string cult = null)
        {
            if (_provider == null)
                return index.GetString();
            return _provider.Word(index, cult);
        }

        public static string Column(string index, string cult = null)
        {
            if (_provider == null)
                return index;
            return _provider.Column(index, cult);
        }

        public static string Page(string index, string cult = null)
        {
            if (_provider == null)
                return index;
            return _provider.Page(index, cult);
        }

        public static string Message(string index, params string[] formatElements)
        {
            if (_provider == null)
                return index;
            return _provider.Message(index, formatElements);
        }

    }
}
