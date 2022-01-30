using Asga.Dashboard;
using Asga.Dashboard.Business;
using CodeShellCore.Linq;
using CodeShellCore.Linq.Filtering;
using CodeShellCore.Text;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.DependencyInjection
{
    public static class AsgaDashboardModule
    {
        public static void AddDashboardService<T, TService>(this IServiceCollection coll)
            where T : class, IDashBoardQuery
            where TService : class, IDashboardItemService<T>
        {
            coll.AddTransient<IDashboardItemService<T>, TService>();
        }


        public static void Process(this IDashBoardQuery q)
        {
            if (q.CollectionId != null && q.CollectionId.Contains("__"))
                q.CollectionId = q.CollectionId.GetAfterLast("__");
            else if (q.CollectionId == "C0")
                q.CollectionId = null;
            q.LoadOptions = q.LoadOptions ?? new LoadOptions();
            q.Filters = q.Filters ?? new List<PropertyFilter>();
        }

    }
}
