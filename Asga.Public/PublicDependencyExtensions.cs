using Asga.Common.Data;
using Asga.Common.Services;
using Asga.Public.Data;
using Asga.Public.Services;
using CodeShellCore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public
{
    public static class PublicDependencyExtensions
    {
        public static void AddPublicData(this IServiceCollection coll, bool defaultModule = false)
        {
            if (defaultModule)
            {
                coll.AddUnitOfWork<PublicUnit>();
                coll.AddContext<PublicContext>();
            }
            else
            {
                coll.AddScoped<PublicUnit>();
                coll.AddScoped<PublicContext>();
            }
            coll.AddScoped<IPublicUnit>(d => d.GetService<PublicUnit>());

            coll.AddCollectionUnitOfWork<PublicUnit>();
            coll.AddTransient<AsgaCollectionService>();
            coll.AddTransient(typeof(AsgaRepository<,>));
        }
        public static void AddPublicModule(this IServiceCollection coll, bool defaultModule = false)
        {
            coll.AddPublicData(defaultModule);
            coll.AddTransient<IPublicDataService, PublicDataService>();
            coll.AddServiceFor<AboutSection, AboutSectionService>();
        }
    }
}
