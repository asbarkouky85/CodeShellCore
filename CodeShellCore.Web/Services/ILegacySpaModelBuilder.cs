using CodeShellCore.Web.Moldster;
using Microsoft.AspNetCore.Http;

namespace CodeShellCore.Web.Services
{
    public interface ILegacySpaModelBuilder
    {
        IndexModel BuildModel(HttpRequest request, string defaultTenant, string defaultTitle);
    }
}