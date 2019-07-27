using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Services
{
    public interface IMoldsterService : IServiceBase
    {
        void Init();
        void RenderDomainModule(string mod, string domain, bool lazy);
        void RenderModuleDefinition(string mod, bool lazy);
        void RenderGuid(string module);

        void ProcessTemplates(string module, string domain = null);

        void WriteWebpackConfigFiles(bool lazy);
        void DevelopmentPack();
        void VendorPack(bool production = false);

        string GetUIVersion();
        Result ProductionPack(string moduleName,string version);
    }
}
