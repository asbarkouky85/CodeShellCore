using CodeShellCore.EntityFramework.Migrations;
using CodeShellCore.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Extensions
{
    public static class CodeShellEntityFrameworkExtensions
    {
        public static void AddDbMigrationsService<T>(this IServiceCollection coll) where T : class, IDbMigrationService
        {
            coll.AddTransient<IDbMigrationService, T>();
        }


        public static void AddMultiTenantDbMigrationsService<T>(this IServiceCollection coll) where T : class, IMultiTenantDbMigrationService
        {
            coll.AddTransient<IMultiTenantDbMigrationService, T>();
        }

        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> q, PagedListRequest<T> opts) where T : class
        {
            PagedResult<T> res = new PagedResult<T>();
            q = q.PageUsing(opts);

            res.TotalCount = await q.CountAsync(d => true);

            if (opts.Showing > 0)
                q = q.Skip(opts.Skip).Take(opts.Showing);

            res.List = await q.ToListAsync();
            return res;
        }

    }
}
