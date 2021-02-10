using System;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;

namespace CodeShellCore.Text.Localization.Internal
{
    public class LocalizedDescribedRepository<T, TContext> : ILocalizedDescribedRepository<T>
        where T : class, IDescribedModel
        where TContext : DbContext, ILocalizableDbContext
    {
        TContext DbContext;
        Language _lang;
        public LocalizedDescribedRepository(TContext con, Language lang) 
        {
            DbContext = con;
            _lang = lang;
        }

        public LoadResult<Described<long>> FindAsDescribedLocalized(LoadOptions opts, Expression<Func<T, bool>> filter = null)
        {
            var op = opts.GetOptionsFor<Described<long>>();
            string name = typeof(T).Name;
            var q = DbContext.Set<T>().AsQueryable();
            if (filter != null)
                q = q.Where(filter);
            var id = _lang.Culture.LCID;
            return q.Select(d => new Described<long>
            {
                Id = d.Id,
                Name = DbContext.GetLocalized(name, d.Id, id, "Name", d.Name),
                Description = DbContext.GetLocalized(name, d.Id, id, "Description", d.Description),
            }).LoadWith(op);
        }
    }
}
