using CodeShellCore.Proxy;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Proxy
{
    public interface IProxyDocumentationService
    {
        Task<DocumentDto> GetDocument();
    }
}