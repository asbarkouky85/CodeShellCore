using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Text.Localization;

namespace CodeShellCore.Data.Localization.Internal
{
    public class LocalizationDataService<T> : EntityService<T>, ILocalizationDataService where T : class, ILocalizable
    {
        Language _lang;
        static Dictionary<int, string> _allCulturelanguage;
        static Dictionary<int, string> AllCulturelanguage
        {
            get
            {
                if (_allCulturelanguage == null)
                {
                    _allCulturelanguage = CultureInfo.GetCultures(CultureTypes.AllCultures).Where(s => Shell.SupportedLanguages.Any(w => w == s.Name)).ToDictionary(s => s.LCID, s => s.Name);
                }
                return _allCulturelanguage;
            }
        }
        ILocalizablesRepository<T> _Loc { get { return UnitOfWork.GetRepository<ILocalizablesRepository<T>>(); } }
        public LocalizationDataService(ILocalizablesUnitOfWork unit, Language lang) : base(unit)
        {
            _lang = lang;
        }
        public Dictionary<string, LocalizablesDTO> GetDataFor<TEntity>(object Id) where TEntity : class
        {
            return GetDataFor(typeof(TEntity), Id);
        }

        string ConvertLangintToStr(int lang)
        {
            if (AllCulturelanguage.TryGetValue(lang, out string v))
                return v;
            return null;
        }

        int ConvertLangStrToInt(string lang)
        {
            return AllCulturelanguage.Where(s => s.Value.Equals(lang)).Select(s => s.Key).FirstOrDefault();
        }
        public SubmitResult SetDataFor<TEntity>(object id, Dictionary<string, LocalizablesDTO> dto) where TEntity : class
        {
            return SetDataFor(typeof(TEntity), id, dto);
        }

        public Dictionary<string, LocalizablesDTO> GetDataFor(Type t, object Id)
        {
            Dictionary<string, LocalizablesDTO> res = new Dictionary<string, LocalizablesDTO>();
            var dat = _Loc.Get(t.Name, Id, AllCulturelanguage.Keys);

            res = dat.ToDictionary(
                group => ConvertLangintToStr(group.LocaleId),
                group => new LocalizablesDTO
                {
                    LangId = group.LocaleId,
                    Data = group.Items.ToDictionary(f => f.ColumnName, f => f.Value)
                });

            foreach (var s in Shell.SupportedLanguages)
            {
                if (!res.ContainsKey(s))
                    res[s] = new LocalizablesDTO();
            }
            return res;
        }

        public virtual SubmitResult SetDataFor(Type type, object id, Dictionary<string, LocalizablesDTO> dto)
        {
            return SetDataFor(type.Name, id, dto);
        }

        public virtual SubmitResult SetDataFor(string type, object id, Dictionary<string, LocalizablesDTO> dto)
        {
            foreach (var item in dto)
            {
                List<T> sj = new List<T>();
                foreach (var n in item.Value.Data)
                {
                    if (string.IsNullOrWhiteSpace(n.Value))
                    {
                        continue;
                    }
                    var s = Activator.CreateInstance<T>();
                    s.ColumnName = n.Key;
                    s.Value = n.Value;
                    sj.Add(s);
                }

                _Loc.Apply(type, id, ConvertLangStrToInt(item.Key), sj);
            }
            return UnitOfWork.SaveChanges();
        }
    }
}
