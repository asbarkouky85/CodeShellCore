using CodeShellCore.Data.Services;

namespace Asga.Public.Business
{
    public interface IPublicContentService : IEntityService<PublicContent>
    {
        PublicContent GetByCode(string code, string lang);
        string GetContentPage(string id);
    }
}