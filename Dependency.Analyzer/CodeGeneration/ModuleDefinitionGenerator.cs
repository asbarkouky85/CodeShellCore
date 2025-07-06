using Dependency.Analyzer.Helpers;
using System.Text;
using System.Text.RegularExpressions;

namespace Dependency.Analyzer.CodeGeneration;
public class ModuleDefinitionGenerator
{
    IEnumerable<DependencyItem> items = new List<DependencyItem>();
    string folder;

    public ModuleDefinitionGenerator(IEnumerable<DependencyItem> items, string solutionFolder)
    {
        this.items = items;
        folder = solutionFolder;
    }

    private List<string> _addMethodOfNotExist(List<string> lines)
    {
        var exists = lines.Any(_isAddServices);
        if (exists)
        {
            return lines;
        }
        else
        {
            var result = new List<string>();
            bool classDeclared = false, insideClassBlock = false, isAdded = false;
            foreach (var line in lines)
            {
                result.Add(line);
                if (isAdded)
                    continue;

                if (line.Contains("static class"))
                {
                    classDeclared = true;
                }

                if (classDeclared && line.Contains("{"))
                {
                    insideClassBlock = true;
                }

                if (insideClassBlock)
                {
                    result.Add("\tprivate static void _addServices(IServiceCollection services)");
                    result.Add("\t{");
                    result.Add("\t");
                    result.Add("\t}");
                    result.Add("");
                    isAdded = true;
                }


            }

            return result;
        }
    }

    bool _isAddServices(string line)
    {
        return line.Contains("void _addServices(IServiceCollection services)");
    }

    void _appendToExisting(string filePath, IEnumerable<DependencyItem> list)
    {
        var sb = new StringBuilder();
        var namespaces = _getNamespaceListAndAddRegistrations(list, sb);
        var lines = File.ReadAllLines(filePath).ToList();

        lines = _addMethodOfNotExist(lines);
        var regx = new Regex(@"using ([a-zA-Z0-9\._]+);");
        var transformed = new List<string>();
        var ignore = false;


        foreach (var line in lines)
        {
            var matches = regx.Matches(line);
            if (matches.Any() && !ignore)
            {
                namespaces.Add(matches[0].Groups[1].Value);
            }
            else if (_isAddServices(line))
            {
                transformed.Add(line);
                transformed.Add("\t{");
                transformed.Add(sb.ToString());
                ignore = true;

            }
            else if (line.Trim() == "}" && ignore)
            {
                transformed.Add(line);
                ignore = false;
            }
            else if (!ignore)
            {
                transformed.Add(line);
            }

        }
        namespaces = namespaces.Distinct().OrderBy(x => x).ToList();
        var final = new List<string>();
        foreach (var ns in namespaces)
        {
            final.Add($"using {ns};");
        }
        final.AddRange(transformed);
        File.WriteAllLines(filePath, final.ToArray());
    }

    List<string> _getNamespaceListAndAddRegistrations(IEnumerable<DependencyItem> list, StringBuilder registrations)
    {
        List<string> nameSpaces = new();
        list = list.OrderBy(e => e.AbstractionName).ToList();
        foreach (var item in list)
        {
            registrations.AppendLine($"\t\tservices.AddScoped<{item.AbstractionName}, {item.Name}>();");

            if (!string.IsNullOrEmpty(item.AbstractionNamespace))
                nameSpaces.Add(item.AbstractionNamespace);
            if (!string.IsNullOrEmpty(item.Namespace))
                nameSpaces.Add(item.Namespace);
        }
        nameSpaces = nameSpaces.Distinct().OrderBy(e => e).ToList();
        return nameSpaces;
    }

    void _fillRegstrations(ModuleDefinitionModel model, IEnumerable<DependencyItem> list)
    {
        StringBuilder registrations = new();
        var nameSpaces = _getNamespaceListAndAddRegistrations(list, registrations);

        StringBuilder nsbuider = new();
        foreach (var ns in nameSpaces)
            nsbuider.AppendLine($"using {ns};");

        model.Usings = nsbuider.ToString();
        model.Registrations = registrations.ToString();
    }

    public void Generate()
    {
        var classifier = new DependencyClassifyService();
        var injectables = items.Where(e => classifier.IsInjectable(e));
        var assemblies = items.Select(e => e.Assembly).Distinct().ToList();
        var projs = Directory.GetFiles(folder, "*.csproj", SearchOption.AllDirectories);
        var template = GetType().Assembly.GetEmbeddedResourceAsString("module_definition_template");
        foreach (var assembly in assemblies)
        {
            var proj = projs.FirstOrDefault(e => e.EndsWith($"\\{assembly}.csproj"));
            var projectFolder = new FileInfo(proj).Directory?.FullName;
            var assemblyInjectables = injectables.Where(e => e.Assembly == assembly).ToList();
            var standardName = AnalyzerUtils.WordsToCamelCase(assembly, ".", "\\.");

            var moduleDefinitionFilePath = Directory.GetFiles(projectFolder, "*ModuleDefinition.cs").FirstOrDefault();
            if (moduleDefinitionFilePath == null)
            {
                var model = new ModuleDefinitionModel
                {
                    ClassName = standardName + "ModuleDefinition",
                    ModuleName = standardName + "Module",
                    Namespace = AnalyzerUtils.GetTagContent(File.ReadAllLines(proj), "RootNamespace") ?? assembly
                };

                _fillRegstrations(model, assemblyInjectables);
                moduleDefinitionFilePath = Path.Combine(projectFolder, model.ClassName + ".cs");

                var content = AnalyzerUtils.FillStringParameters(template, model);
                File.WriteAllText(moduleDefinitionFilePath, content);
                Console.WriteLine($"Added '{moduleDefinitionFilePath}'");
            }
            else
            {
                _appendToExisting(moduleDefinitionFilePath, assemblyInjectables);
                Console.WriteLine($"Modified '{moduleDefinitionFilePath}'");
            }

        }
    }
}
