namespace CodeShellCore.ToolSet.Localization
{
    public class AbpExtractLocalizationKeysRequest
    {
        public string FrontEndFolder {  get; set; } 
        public string BackendFolder {  get; set; }
        public string DefaultResourceName { get; internal set; }
    }
}
