namespace CodeShellCore.ToolSet.Analyzer
{
    public class AnalyzerRequest
    {
        public string Folder { get; set; }
        public string TargetFramework { get; set; }
        public bool CentralVersions { get; set; }
    }
}
