namespace CodeShellCore.Moldster.Resources.Services
{
    public interface IResourceScriptGenerationService
    {
        bool GenerateHttpService(string resource, string domain = null);
    }
}
