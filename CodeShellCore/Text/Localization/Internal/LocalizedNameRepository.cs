using System;
using System.Linq.Expressions;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Data;

namespace CodeShellCore.Text.Localization.Internal
{
    public class LocalizedNameRepository<T, TContext> : ILocalizedNameRepository<T>
        where T : class, INamedModel
        where TContext : DbContext, ILocalizableDbContext
    {
        protected readonly Language Lang;
        TContext _context;
        ILocalizableDbContext DbContext;
        public LocalizedNameRepository(TContext con, Language lang)
        {
            Lang = lang;
            DbContext = con;
            _context = con;
        }

        public LoadResult<NamedLong> FindAsNamedLocalized(LoadOptions opts, Expression<Func<T, bool>> filter = null)
        {
            string type = typeof(T).Name;
            var op = opts.GetOptionsFor<NamedLong>();
            var lId = Lang.Culture.LCID;
            var q = (IQueryable<INamedModel>)_context.Set<T>().AsQueryable();
            var q2 =from d in q
                    select new NamedLong
                    {
                        Id = d.Id,
                        Name = _context.GetLocalized(type, d.Id, lId, "Name", d.Name)
                    };
            var f = q2.ToList();
            return q2.LoadWith(op);
        }
    }
}
