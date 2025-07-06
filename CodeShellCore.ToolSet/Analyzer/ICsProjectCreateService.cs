using System.Collections.Generic;

namespace CodeShellCore.ToolSet.Analyzer
{
    public interface ICsProjectCreateService
    {
        string Create(
            string projectName,
            string targetFramework,
            List<string> projectDependencies,
            string outputDirectory = ".",
            bool centralizedVersions = false);
    }
}