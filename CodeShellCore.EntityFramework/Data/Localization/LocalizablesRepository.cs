using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using CodeShellCore.Helpers;
using CodeShellCore.Data.EntityFramework;

namespace CodeShellCore.Data.Localization
{
    public class LocalizableRepository<T, TContext> : 
        Repository_Int64<T, TContext>,
        ILocalizablesRepository<T> where T : class,
        ILocalizable where TContext : DbContext
    {
        public LocalizableRepository(TContext con) : base(con)
        {
        }

        public void Apply(string type, object id, int langId, IEnumerable<T> data)
        {
            var res = Loader.Where(d => d.EntityId.Equals(id) && d.EntityType == type && d.LocaleId == langId).ToList();
            foreach (var item in res)
            {
                Delete(item);
            }
            foreach (var ob in data)
            {
                ob.EntityId = (long)id;
                ob.LocaleId = langId;
                ob.EntityType = type;
                ob.Id= Utils.GenerateID();
                Add(ob);
            }
        }

        public IEnumerable<LocalizablesLoader> Get(string type, object id, IEnumerable<int> langs)
        {

            return Loader.Where(d => langs.Contains(d.LocaleId) && d.EntityId.Equals(id) && d.EntityType == type)
                     .GroupBy(s => s.LocaleId, (q, b) => new LocalizablesLoader
                     {
                         LocaleId = q,
                         Items = b.Select(l => new LocalizableItem { ColumnName = l.ColumnName, Value = l.Value })
                     }).ToList();

            //return Loader.Where(d => langs.Contains(d.LocaleId) && d.EntityId == id && d.EntityType == type).Select(d => new LocalizablesLoader
            //{
            //    LocaleId = d.LocaleId,
            //    Items = Loader.Where(e => e.EntityId == id && e.LocaleId == d.LocaleId && e.EntityType == type).Select(e => new LocalizableItem
            //    {
            //        ColumnName = e.ColumnName,
            //        Value = e.Value
            //    })
            //}).ToList();
        }
    }
}
