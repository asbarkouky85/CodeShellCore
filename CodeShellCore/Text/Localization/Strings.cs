using CodeShellCore.Text;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Text.Localization
{
    public class Strings
    {
        private static ILocaleTextProvider provider;

        private static ILocaleTextProvider Provider
        {
            get
            {
                if (provider == null)
                    provider = Shell.RootInjector.GetService<ILocaleTextProvider>();
                return provider;
            }
        }

        public static string Word(string index, string cult = null)
        {
            if (Provider == null)
                return index;
            return Provider.Word(index, cult);
        }

        public static string Word(Enum index, string cult = null)
        {
            if (Provider == null)
                return index.GetString();
            return Provider.Word(index, cult);
        }

        public static string Column(string index, string cult = null)
        {
            if (Provider == null)
                return index;
            return Provider.Column(index, cult);
        }

        public static string Page(string index, string cult = null)
        {
            if (Provider == null)
                return index;
            return Provider.Page(index, cult);
        }

        public static string Message(string index, params string[] formatElements)
        {
            if (Provider == null)
                return index;
            return Provider.Message(index, formatElements);
        }

    }
}
