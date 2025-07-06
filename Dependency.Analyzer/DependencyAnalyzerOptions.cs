namespace Dependency.Analyzer
{
    public static class DependencyAnalyzerOptions
    {
        public static bool SaveToDb = false;
        public static bool Read = true;
        public static bool GenerateModuleClasses = true;
        public static string DbConnectionString = "Server=.;Integrated Security=True;Database=_test";
    }
}
