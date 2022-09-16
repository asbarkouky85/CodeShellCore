namespace CodeShellCore.Moldster.Resources
{
    public interface IResourceScriptGenerationService
    {
        bool GenerateHttpService(string resource, string domain = null);
    }
}
