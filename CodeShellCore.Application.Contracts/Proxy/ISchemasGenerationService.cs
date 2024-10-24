using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Proxy
{
    public interface ISchemasGenerationService
    {
        void ClearProxyFolder(string targetFolder);
        Task GenerateProxyServices(Dictionary<string, GroupDto> modules, string targetFolder);
        Task GenerateSchemas(Dictionary<string, SchemaItemDto> schemas,string targetFolder);
    }
}