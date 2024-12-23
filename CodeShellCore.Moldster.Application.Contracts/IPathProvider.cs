using System.Collections.Generic;
using CodeShellCore.Services;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.PageCategories;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster
{
    public interface ILayoutsService
    {
        List<LayoutFileDTO> GetLayouts(bool nameOnly = false);
    }

    public interface IPathsService : IServiceBase
    {
        string ConfigUrl { get; }
        string ConfigRoot { get; }
        string CoreAppName { get; }
        string LocalizationRoot { get; }
        string UIRoot { get; }
        string UILaunchProfile { get; }
        string UIUrl { get; }

        List<MoldsterEnvironment> GetEnvironments();
        MoldsterEnvironment GetEnvironmentByName(string name);
        Task<List<MoldsterEnvironment>> UpdateEnvironments(IEnumerable<MoldsterEnvironment> envs);


    }
}