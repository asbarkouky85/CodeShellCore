using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency.Analyzer
{
    public class DependencyClassifyService
    {
        public string? GetClassification(Type type)
        {
            return null;
        }

        public string? GetClassificationFinalStage(Type type)
        {
            return null;
        }

        public bool IsQualified(Type type)
        {
            return false;
        }

        public Dictionary<string, string[]> GetSuffixToClassificationDictionary()
        {
            return new Dictionary<string, string[]>();
        }

        public bool IsInjectable(DependencyItem dependencyItem)
        {
            return false;
        }
    }
}
