using System.Text.RegularExpressions;
using CodeShellCore.Text;
namespace System.Text
{
    public static class SystemTextExtensions
    {
        static Regex httpReg = new Regex("^http(.*)://");
        public static string FindXmlValue(this string subject, string elementName)
        {
            Regex sourcesPattern = new Regex("<" + elementName + ">((.|\n)*)</" + elementName + ">");
            var mat = sourcesPattern.Match(subject);
            if (mat.Success)
                return mat.Groups[1].Value;
            return null;
        }
    }

}