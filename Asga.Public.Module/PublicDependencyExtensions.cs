using Asga.Public.Business;
using Asga.Public.Business.Internal;
using Asga.Public.Data;
using Asga.Public.Data.Internal;
using CodeShellCore.Data.Attachments;
using CodeShellCore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Asga.Public
{
    public static class PublicDependencyExtensions
    {
        public static void AddPublicData(this IServiceCollection coll, bool defaultModule = false)
        {
            if (defaultModule)
            {
                coll.AddUnitOfWork<PublicUnit>();
                coll.AddContext<AsgaPublicContext>();
            }
            else
            {
                coll.AddScoped<PublicUnit>();
                coll.AddScoped<AsgaPublicContext>();
            }
            coll.AddScoped<IPublicUnit>(d => d.GetService<PublicUnit>());
            coll.AddConfiguredCollections(typeof(AsgaPublicRepository<,>));
            coll.AddTransient(typeof(AsgaPublicRepository<,>));
            coll.AddTransient(typeof(DefaultAttachmentRepository<,>));
            coll.AddRepositoryFor<HomeSlide, HomeSlideRepository>();

        }
        public static void AddPublicModule(this IServiceCollection coll, bool defaultModule = false)
        {
            coll.AddPublicData(defaultModule);
            coll.AddServiceFor<HomeSlide, HomeSlideService, IHomeSlideService>();
            coll.AddServiceFor<PublicContent, PublicContentService, IPublicContentService>();
            coll.AddServiceFor<Message, MessageService>();
            coll.AddRepositoryFor<Message, MessageRepository, IMessageRepository>();
            coll.AddRepositoryFor<HomeSlide, HomeSlideRepository, IHomeSlideRepository>();
        }
    }
}
