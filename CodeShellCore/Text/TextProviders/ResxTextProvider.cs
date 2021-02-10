using CodeShellCore.Text.Localization;
using CodeShellCore.Text;
using CodeShellCore.Helpers;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace CodeShellCore.Text.TextProviders
{
    public class ResxTextProvider : ILocaleTextProvider
    {
        Language _lang;

        protected static Dictionary<string, Dictionary<string, string>> WordsDictionary = new Dictionary<string, Dictionary<string, string>>();
        protected static Dictionary<string, Dictionary<string, string>> ColsDictionary = new Dictionary<string, Dictionary<string, string>>();
        protected static Dictionary<string, Dictionary<string, string>> MessDictionary = new Dictionary<string, Dictionary<string, string>>();
        protected static Dictionary<string, Dictionary<string, string>> PageDictionary = new Dictionary<string, Dictionary<string, string>>();

        public ResxTextProvider()
        {
            _lang = Shell.ScopedInjector != null ? Shell.ScopedInjector.GetService<Language>() : Language.Default;
        }

        static void InitializeCulutre(string cult)
        {
            if (Shell.UseLocalization)
                UseLocalization(cult);
            else
            {
                WordsDictionary[cult] = new Dictionary<string, string>();
                ColsDictionary[cult] = new Dictionary<string, string>();
                MessDictionary[cult] = new Dictionary<string, string>();
                PageDictionary[cult] = new Dictionary<string, string>();
            }
        }

        static void UseLocalization(string cult)
        {
            string assembly = Shell.LocalizationAssembly;

            string wordsType = assembly + ".Localization.Words";
            string colsType = assembly + ".Localization.Columns";
            string messType = assembly + ".Localization.Messages";
            string pageType = assembly + ".Localization.Pages";

            Assembly ass = Assembly.Load(assembly);

            ResourceManager wordRes = new ResourceManager(wordsType, ass);
            ResourceManager colRes = new ResourceManager(colsType, ass);
            ResourceManager messRes = new ResourceManager(messType, ass);
            ResourceManager pageRes = new ResourceManager(pageType, ass);

            WordsDictionary[cult] = LangUtils.ResourceToDictionary(wordRes, new CultureInfo(cult));
            ColsDictionary[cult] = LangUtils.ResourceToDictionary(colRes, new CultureInfo(cult));
            MessDictionary[cult] = LangUtils.ResourceToDictionary(messRes, new CultureInfo(cult));
            PageDictionary[cult] = LangUtils.ResourceToDictionary(pageRes, new CultureInfo(cult));
        }

        public string Word(string index, string cult = null)
        {
            cult = cult ?? _lang.Culture.TwoLetterISOLanguageName;

            if (!WordsDictionary.ContainsKey(cult))
                InitializeCulutre(cult);

            string word;
            if (WordsDictionary[cult].TryGetValue(index, out word))
                return word;

            return index;
        }

        public string Column(string index, string cult = null)
        {
            cult = cult ?? _lang.Culture.TwoLetterISOLanguageName;
            if (!ColsDictionary.ContainsKey(cult))
                InitializeCulutre(cult);

            string col;
            if (ColsDictionary[cult].TryGetValue(index, out col))
                return col;

            return Word(index.GetAfterLast("__"));
        }

        public string Page(string index, string cult = null)
        {
            cult = cult ?? _lang.Culture.TwoLetterISOLanguageName;

            if (!PageDictionary.ContainsKey(cult))
                InitializeCulutre(cult);

            string word;
            if (PageDictionary[cult].TryGetValue(index, out word))
                return word;

            return Word(index.GetAfterLast("__"));
        }

        public string Message(string index, params string[] formatElements)
        {
            string cult = _lang.Culture.TwoLetterISOLanguageName;

            if (!MessDictionary.ContainsKey(cult))
                InitializeCulutre(cult);

            string mes;
            if (MessDictionary[cult].TryGetValue(index, out mes))
                return string.Format(MessDictionary[cult][index], formatElements);

            return index;
        }

        public string MessageWithCulture(string index, string cult, params string[] formatElements)
        {
            if (!MessDictionary.ContainsKey(cult))
                InitializeCulutre(cult);

            string mes;
            if (MessDictionary[cult].TryGetValue(index, out mes))
                return string.Format(MessDictionary[cult][index], formatElements);

            return index;
        }

        public string Word(string index, params string[] args)
        {
            string cult = _lang.Culture.TwoLetterISOLanguageName;

            if (!WordsDictionary.ContainsKey(cult))
                InitializeCulutre(cult);

            string mes;
            if (WordsDictionary[cult].TryGetValue(index, out mes))
                return string.Format(MessDictionary[cult][index], args);

            return index;
        }

        public string WordWithCulture(string index, string cult, params string[] args)
        {
            if (!WordsDictionary.ContainsKey(cult))
                InitializeCulutre(cult);

            string mes;
            if (WordsDictionary[cult].TryGetValue(index, out mes))
                return string.Format(MessDictionary[cult][index], args);

            return index;
        }

        public string Word(Enum en, string cult = null)
        {
            return Word(en.StringFormat(), cult);
        }
    }
}
