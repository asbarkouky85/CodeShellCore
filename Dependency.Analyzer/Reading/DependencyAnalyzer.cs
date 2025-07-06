using Dependency.Analyzer.CodeGeneration;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;

namespace Dependency.Analyzer;

public static class DependencyAnalyzer
{
    static DependencyClassifyService ClassifierService = new DependencyClassifyService();
    public static async Task Generate(string solutionFolder, List<DependencyItem> lst)
    {
        IEnumerable<DependencyItem> data = new List<DependencyItem>();
        if (DependencyAnalyzerOptions.SaveToDb)
        {
            data = await SqlGenerator.GetData<DependencyItem>("Select * from DependencyItems");
        }
        else
        {
            data = lst;
        }

        var defGen = new ModuleDefinitionGenerator(data, solutionFolder);
        defGen.Generate();
    }

    public static async Task<List<DependencyItem>> Read(List<string> assemblies)
    {
        var items = new List<DependencyItem>();
        foreach (var check in assemblies)
        {
            try
            {
                Console.Write(check);
                var a = Assembly.Load(new AssemblyName(check));

                foreach (var type in a.GetTypes())
                {
                    if (_isQualified(type))
                    {
                        items.Add(_present(type, check));
                    }
                }
            }
            catch
            {
                Console.Write("\t\tFailed");
            }
            Console.WriteLine();
        }

        if (DependencyAnalyzerOptions.SaveToDb)
            await SqlGenerator.InsertData(items);
        return items;
    }

    static DependencyItem _present(Type type, string assembly)
    {

        var result = new DependencyItem();

        result.Namespace = type.Namespace;
        result.Name = type.Name;
        result.Assembly = assembly;

        var classification = ClassifierService.GetClassification(type);

        if (classification == null)
        {
            var dic = ClassifierService.GetSuffixToClassificationDictionary();
            foreach (var kvp in dic)
            {
                foreach (var v in kvp.Value)
                {
                    if (type.Name.EndsWith(v))
                        classification = kvp.Key;
                }
            }

            if (classification == null)
            {
                classification = ClassifierService.GetClassificationFinalStage(type);
            }
        }
        result.Classification = classification ?? "Unknown";

        _setAbstraction(result, type);
        return result;
    }

    private static void _setAbstraction(DependencyItem item, Type type)
    {
        if (ClassifierService.IsInjectable(item))
        {
            var intrface = type.GetInterfaces().Where(e => e.Name == "I" + item.Name).FirstOrDefault();

            if (intrface == null)
            {
                intrface = type.GetInterfaces().Where(e => !e.IsGenericType).FirstOrDefault();
            }

            if (intrface != null)
            {
                item.AbstractionNamespace = intrface.Namespace;
                item.AbstractionName = intrface.Name;
                item.AbstractionAssembly = intrface.Assembly.GetName()?.Name;
            }
            else
            {
                item.Classification = "POCO";
            }
        }

    }

    static bool _isQualified(Type type)
    {
        if (type.Name == "Program" || type.Name == "Startup")
            return false;
        if (type.CustomAttributes.Any(e => e.AttributeType == typeof(GeneratedCodeAttribute)))
            return false;
        if (type.FullName != null && (type.FullName.StartsWith("System") || type.FullName.Contains("+")))
            return false;
        if (type.IsClass && type.IsAbstract && type.IsSealed)
            return false;
        if (!ClassifierService.IsQualified(type))
            return false;
        if (type.IsInterface || type.IsAbstract || type.IsGenericType)
            return false;
        return true;
    }
}
