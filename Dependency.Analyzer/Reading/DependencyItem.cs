namespace Dependency.Analyzer;

public class DependencyItem
{
    public string Assembly { get; set; }
    public string? AbstractionAssembly { get; set; }
    public string Name { get; set; }
    public string? Namespace { get; set; }
    public string? AbstractionName { get; set; }
    public string? AbstractionNamespace { get; set; }
    public string Classification { get; set; }

    
}
