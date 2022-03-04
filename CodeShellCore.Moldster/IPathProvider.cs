using System.Collections.Generic;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster
{
    public interface IPathsService : IServiceBase
    {
        string ConfigRoot { get; }
        string ConfigUrl { get; }
        string CoreAppName { get; }
        string LocalizationRoot { get; }
        string UIRoot { get; }
        string UILaunchProfile { get; }
        string UIUrl { get; }

        List<MoldsterEnvironment> GetEnvironments();
        List<MoldsterEnvironment> UpdateEnvironments(IEnumerable<MoldsterEnvironment> envs);
        List<LayoutFileDTO> GetLayouts(bool nameOnly=false);
    }
}