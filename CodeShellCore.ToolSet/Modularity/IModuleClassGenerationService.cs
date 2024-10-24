using CodeShellCore.Helpers;
using CodeShellCore.ToolSet.Modularity;
using System.Threading.Tasks;

namespace CodeShellCore.Modularity
{
    public interface IModuleClassGenerationService
    {
        Task<Result> Generate(GenerateModuleClassesRequest request);
    }
}