using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Dependency.Analyzer.Helpers;
public static class AnalyzerUtils
{
    public static bool IsRecord(this Type type)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));

        return type.IsClass &&
               type.GetMethod("Equals", new Type[] { typeof(object) })?.DeclaringType == type &&
               type.GetProperty("EqualityContract", BindingFlags.NonPublic | BindingFlags.Instance)?.GetMethod?.IsCompilerGenerated() == true;
    }

    private static bool IsCompilerGenerated(this MethodInfo method)
    {
        return method.GetCustomAttribute<System.Runtime.CompilerServices.CompilerGeneratedAttribute>() != null;
    }

    public static bool Implements(this Type type, Type target)
    {
        if (target.IsGenericType)
        {
            return type.GetInterfaces().Any(e => e.GetGenericTypeDefinition() == target);
        }
        else
        {
            return type.GetInterfaces().Contains(target);
        }
    }

    public static string WordsToCamelCase(string id, string separtor, string separatorPattern = null)
    {
        id = new Regex("([a-z])([A-Z])").Replace(id, $"$1{separtor}$2");
        Regex separatorPatternRegex = separatorPattern != null ? new Regex(separatorPattern) : new Regex(separtor);
        MatchCollection col = separatorPatternRegex.Matches(id);

        id = id.Substring(0, 1).ToUpper() + id.Substring(1).ToLower();

        var collection = col.OrderBy(e => e.Index).ToList();
        foreach (Match d in collection)
        {
            if (d.Index != 0)
            {
                var ind = d.Index;
                id = id.Substring(0, ind + 1) + id.Substring(ind + 1, 1).ToUpper() + id.Substring(ind + 2).ToLower();
            }
        }
        id = separatorPatternRegex.Replace(id, "");

        return id;
    }

    public static Stream GetEmbeddedResourceStream(this Assembly assembly, string key)
    {
        var resourceNames = assembly.GetManifestResourceNames();
        var resourceWithName = resourceNames.FirstOrDefault(e => e.Contains(key));
        if (resourceWithName != null)
        {
            return assembly.GetManifestResourceStream(resourceWithName);
        }
        throw new Exception($"Could not find '{key}'");
    }

    public static byte[] GetEmbeddedResourceBytes(this Assembly assembly, string key)
    {
        using (var resStream = assembly.GetEmbeddedResourceStream(key))
        {
            var st = new MemoryStream();
            resStream.CopyTo(st);
            return st.ToArray();
        }
    }

    public static string GetEmbeddedResourceAsString(this Assembly assembly, string key)
    {
        var bytes = assembly.GetEmbeddedResourceBytes(key);
        if (bytes != null)
        {
            return Encoding.UTF8.GetString(bytes);
        }
        return null;
    }

    public static string GetTagContent(IEnumerable<string> subject, string tag, string usePattern = null)
    {
        var pat = usePattern ?? "<{0}>(.*?)</{0}>";
        string patternString = string.Format(pat, tag); //(isCore ?? IsCore) ? $"<{tag}>(.*?)</{tag}>" : @"\[assembly: " + tag + @"\((.*?)\)\]";
        Regex pattern = new Regex(patternString);
        foreach (var item in subject)
        {
            var s = pattern.Match(item);
            if (s.Success)
                return s.Groups[1].Value;
        }
        return null;
    }

    public static string FillStringParameters(string contents, object parameters)
    {
        PropertyInfo[] props = parameters.GetType().GetProperties();
        foreach (PropertyInfo inf in props)
        {
            contents = contents.Replace("%" + inf.Name + "%", inf.GetValue(parameters)?.ToString());
        }
        return contents;
    }
}
