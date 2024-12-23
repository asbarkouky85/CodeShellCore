using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;

namespace CodeShellCore.Text.TextProviders
{
    public class ResxTextProvider : ILocaleTextProvider
    {
        Language _lang;
        static object _locker = new { };

        protected static Dictionary<string, Dictionary<string, string>> WordsDictionary = new Dictionary<string, Dictionary<string, string>>();
        protected static Dictionary<string, Dictionary<string, string>> ColsDictionary = new Dictionary<string, Dictionary<string, string>>();
        protected static Dictionary<string, Dictionary<string, string>> MessDictionary = new Dictionary<string, Dictionary<string, string>>();
        protected static Dictionary<string, Dictionary<string, string>> PageDictionary = new Dictionary<string, Dictionary<string, string>>();

        public CultureInfo Culture => _lang.Culture;
        static bool _isDev = false;

        static Dictionary<string, DateTime> _lastRead = new Dictionary<string, DateTime>();

        public ResxTextProvider(Language lang)
        {
            _lang = lang;
            _isDev = Environment.GetEnvironmentVariable("FMS_HOME", EnvironmentVariableTarget.User) != null;
        }

        static void _initCulture(string cult)
        {
            lock (_locker)
            {
                if (Shell.UseLocalization)
                    _readCultureResources(cult);
                else
                {
                    WordsDictionary[cult] = new Dictionary<string, string>();
                    ColsDictionary[cult] = new Dictionary<string, string>();
                    MessDictionary[cult] = new Dictionary<string, string>();
                    PageDictionary[cult] = new Dictionary<string, string>();
                }
            }

        }

        void _checkCultureRead(string cult)
        {
            if (_isDev)
            {
                _readCultureResourcesDev(cult);
            }
            else
            {
                if (!WordsDictionary.ContainsKey(cult))
                    _initCulture(cult);
            }

        }

        static void _readCultureResourcesDev(string cult)
        {
            string[] types = new[] { "Words", "Pages", "Columns", "Messages" };

            var solutionFolder = Environment.GetEnvironmentVariable("FMS_HOME", EnvironmentVariableTarget.User);
            foreach (var type in types)
            {
                var fileName = Path.Combine(solutionFolder, $"FMS.Domain.Shared\\Localization\\{type}.{cult}.resx");
                if (File.Exists(fileName))
                {
                    var isRead = false;
                    var info = new FileInfo(fileName);
                    if (_lastRead.TryGetValue($"{type}_{cult}", out DateTime lastRead))
                    {
                        if (info.LastWriteTime > lastRead)
                            isRead = true;

                    }
                    if (!isRead)
                    {
                        _readFileDev(type, cult, fileName);
                    }
                    _lastRead[$"{type}_{cult}"] = info.LastWriteTime;
                }
            }
        }

        static void _readFileDev(string type, string cult, string fileName)
        {
            switch (type)
            {
                case "Words":
                    WordsDictionary[cult] = LangUtils.ResourceToDictionary(fileName);
                    break;
                case "Pages":
                    PageDictionary[cult] = LangUtils.ResourceToDictionary(fileName);
                    break;
                case "Messages":
                    MessDictionary[cult] = LangUtils.ResourceToDictionary(fileName);
                    break;
                case "Columns":
                    ColsDictionary[cult] = LangUtils.ResourceToDictionary(fileName);
                    break;
            }

        }

        static void _readCultureResources(string cult)
        {
            string assembly = Shell.LocalizationAssembly;
            string root = Shell.LocalizationAssembly.Replace(".Domain.Shared", "");

            string wordsType = root + ".Localization.Words";
            string colsType = root + ".Localization.Columns";
            string messType = root + ".Localization.Messages";
            string pageType = root + ".Localization.Pages";

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
            _checkCultureRead(cult);
            string word;
            if (WordsDictionary[cult].TryGetValue(index, out word))
                return word;

            return index;
        }

        public string Column(string index, string cult = null)
        {
            cult = cult ?? _lang.Culture.TwoLetterISOLanguageName;

            _checkCultureRead(cult);

            string col;
            if (ColsDictionary[cult].TryGetValue(index, out col))
                return col;

            return Word(index.GetAfterLast("__"));
        }

        public string Page(string index, string cult = null)
        {
            cult = cult ?? _lang.Culture.TwoLetterISOLanguageName;

            _checkCultureRead(cult);

            string word;
            if (PageDictionary[cult].TryGetValue(index, out word))
                return word;

            return Word(index.GetAfterLast("__"));
        }

        public string Message(string index, params string[] formatElements)
        {
            string cult = _lang.Culture.TwoLetterISOLanguageName;

            _checkCultureRead(cult);

            string mes;
            if (MessDictionary[cult].TryGetValue(index, out mes))
                return string.Format(MessDictionary[cult][index], formatElements);

            return index;
        }

        public string MessageWithCulture(string index, string cult, params string[] formatElements)
        {
            _checkCultureRead(cult);

            string mes;
            if (MessDictionary[cult].TryGetValue(index, out mes))
                return string.Format(MessDictionary[cult][index], formatElements);

            return index;
        }

        public string Word(string index, params string[] args)
        {
            string cult = _lang.Culture.TwoLetterISOLanguageName;

            _checkCultureRead(cult);

            string mes;
            if (WordsDictionary[cult].TryGetValue(index, out mes))
                return string.Format(MessDictionary[cult][index], args);

            return index;
        }

        public string WordWithCulture(string index, string cult, params string[] args)
        {
            _checkCultureRead(cult);

            string mes;
            if (WordsDictionary[cult].TryGetValue(index, out mes))
                return string.Format(MessDictionary[cult][index], args);

            return index;
        }

        public string Word(Enum en, string cult = null)
        {
            return Word(en.StringFormat(), cult);
        }

        public Dictionary<string, string> GetAllWords(string cult)
        {
            _checkCultureRead(cult);
            return WordsDictionary[cult];
        }

        public Dictionary<string, string> GetAllMessages(string cult)
        {
            _checkCultureRead(cult);
            return MessDictionary[cult];
        }

        public Dictionary<string, string> GetAllColumns(string cult)
        {
            _checkCultureRead(cult);
            return ColsDictionary[cult];
        }

        public Dictionary<string, string> GetAllPages(string cult)
        {
            _checkCultureRead(cult);
            return PageDictionary[cult];
        }
    }
}
